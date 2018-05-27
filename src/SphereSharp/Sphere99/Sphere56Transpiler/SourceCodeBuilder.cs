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
            None, Numeric, Eval, Argument,
            Macro
        }

        private class Scopes
        {
            private Stack<Scope> scopes = new Stack<Scope>();

            public Scope Current => scopes.Count > 0 ? scopes.Peek() : Scope.None;

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

        public void StartNumericExpression() => scopes.Enter(Scope.Numeric);
        public void EndNumericExpression() => scopes.Leave();

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

        public void StartArgumentList() => scopes.Enter(Scope.Argument);
        public void StartMemberAccess()
        {
            if (scopes.Current == Scope.Numeric)
            {
                builder.Append('<');
            }
            scopes.Enter(Scope.None);
        }

        public void EndMemberAccess()
        {
            scopes.Leave();
            if (scopes.Current == Scope.Numeric)
                builder.Append('>');
        }

        public void StartMacro() => scopes.Enter(Scope.Macro);
        public void EndMacro() => scopes.Leave();
    }
}
