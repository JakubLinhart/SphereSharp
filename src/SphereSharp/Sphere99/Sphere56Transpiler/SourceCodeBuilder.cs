using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public sealed class SourceCodeBuilder
    {
        private enum Scope
        {
            None, Numeric, Eval, ArgumentRequiringEval,
            Macro, MemberAccess, VariablesRestriced
        }

        private class Scopes
        {
            private Stack<Scope> scopes = new Stack<Scope>();

            public Scope Current => scopes.Count > 0 ? scopes.Peek() : Scope.None;
            public Scope Parent => scopes.Count > 1 ? scopes.Skip(1).First() : Scope.None;

            public void Enter(Scope scope) => scopes.Push(scope);
            public void Leave() => scopes.Pop();
        }

        private TextBuilder builder = new TextBuilder();
        public string Output => builder.Output;

        public void Append(ITerminalNode node)
            => builder.Append(node);
        public void Append(ITerminalNode[] nodes)
            => builder.Append(nodes);

        public void Append(string str) => builder.Append(str);
        public void Append(char ch) => builder.Append(ch);

        public void AppendLine(string str) => builder.AppendLine(str);
        public void AppendLine() => builder.AppendLine();
        public void AppendLine(ITerminalNode[] nodes)
            => builder.AppendLine(nodes);

        private Scopes scopes = new Scopes();

        public void AppendTag()
        {
            Append("tag");
            if (scopes.Current == Scope.Numeric)
                Append('0');
        }

        public void AppendLocalVariable(string name)
        {
            AppendVariable("local", name);
        }

        public void AppendGlobalVariable(string name)
        {
            AppendVariable("var", name);
        }

        private void AppendVariable(string variableType, string name)
        {
            bool requiresMacro = scopes.Parent != Scope.VariablesRestriced 
                && scopes.Current != Scope.Macro && scopes.Parent != Scope.Macro
                && scopes.Parent != Scope.None;

            if (requiresMacro)
                builder.Append('<');

            if (scopes.Parent != Scope.VariablesRestriced)
            {
                builder.Append(variableType);
                builder.Append('.');
            }

            builder.Append(name);

            if (requiresMacro)
                builder.Append('>');
        }

        public void RestrictVariables() => scopes.Enter(Scope.VariablesRestriced);
        public void AllowVariables() => scopes.Leave();

        public void StartNumericExpression() => scopes.Enter(Scope.Numeric);
        public void EndNumericExpression() => scopes.Leave();

        public void StartRequireMacro()
        {
            if (scopes.Current != Scope.Macro)
                scopes.Enter(Scope.ArgumentRequiringEval);
        }

        public void EndRequireMacro()
        {
            if (scopes.Current != Scope.Macro)
                scopes.Leave();
        }

        public void StartEvalCall() => scopes.Enter(Scope.Eval);
        public void EndEvalCall() => scopes.Leave();
        public void EnsureEvalCall(string evalFuncName, Action evalAction)
        {
            var currentScope = scopes.Current;

            if (currentScope != Scope.Macro && currentScope != Scope.Numeric)
            {
                Append('<');
                StartMacro();
            }

            if (currentScope != Scope.Eval && currentScope != Scope.Numeric)
            {
                Append(evalFuncName);
                Append(' ');
                StartEvalCall();
            }

            evalAction();

            if (currentScope != Scope.Eval && currentScope != Scope.Numeric)
            {
                EndEvalCall();
            }

            if (currentScope != Scope.Macro && currentScope != Scope.Numeric)
            {
                Append('>');
                EndMacro();
            }
        }

        public void StartMemberAccess()
        {
            if (scopes.Current == Scope.Numeric || scopes.Current == Scope.ArgumentRequiringEval)
            {
                builder.Append('<');
                StartMacro();
            }
            else
                scopes.Enter(Scope.MemberAccess);
        }

        public void EndMemberAccess()
        {
            if (scopes.Current == Scope.Macro)
                builder.Append('>');
            scopes.Leave();
        }

        public void StartMacro() => scopes.Enter(Scope.Macro);
        public void EndMacro() => scopes.Leave();

        private int callStartIndex = -1;
        private int lastSharpSubstitutionStartIndex = -1;
        private int lastSharpSubstitutionEndIndex = -1;

        public void StartCall()
        {
            callStartIndex = builder.Length;
        }

        public void EndCall()
        {
            callStartIndex = -1;
        }

        public void CaptureLastSharpSubstitution()
        {
            if (callStartIndex >= 0)
            {
                lastSharpSubstitutionStartIndex = callStartIndex;
                lastSharpSubstitutionEndIndex = builder.Length - 1;
            }
        }

        public void AppendLastSharpSubstitution()
        {
            if (lastSharpSubstitutionStartIndex < 0 || lastSharpSubstitutionEndIndex < 0 || lastSharpSubstitutionStartIndex > lastSharpSubstitutionEndIndex)
                throw new InvalidOperationException();

            builder.Append('<');
            builder.AppendSubstring(lastSharpSubstitutionStartIndex, lastSharpSubstitutionEndIndex);
            builder.Append('>');
        }
    }
}
