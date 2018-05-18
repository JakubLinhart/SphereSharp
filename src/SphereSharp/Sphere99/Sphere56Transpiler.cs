using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99
{
    public class Sphere56Transpiler : sphereScript99BaseVisitor<bool>
    {
        private SourceCodeBuilder builder = new SourceCodeBuilder();

        public string Output => builder.Output;

        public override bool VisitMemberName([NotNull] sphereScript99Parser.MemberNameContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitChainedMemberAccess([NotNull] sphereScript99Parser.ChainedMemberAccessContext context)
        {
            builder.Append(".");

            return base.VisitChainedMemberAccess(context);
        }

        public override bool VisitEnclosedArgumentList([NotNull] sphereScript99Parser.EnclosedArgumentListContext context)
        {
            builder.Append(" ");

            return base.VisitEnclosedArgumentList(context);
        }

        public override bool VisitArgumentList([NotNull] sphereScript99Parser.ArgumentListContext context)
        {
            var arguments = context.argument();
            for (int i = 0; i < arguments.Length; i++)
            {
                VisitArgument(arguments[i]);
                if (i < arguments.Length - 1)
                    builder.Append(",");
            }

            return true;
        }

        public override bool VisitArgument([NotNull] sphereScript99Parser.ArgumentContext context)
        {
            return semanticContext.Execute(() => base.VisitArgument(context));
        }

        public override bool VisitFirstFreeArgumentMandatoryWhiteSpace([NotNull] sphereScript99Parser.FirstFreeArgumentMandatoryWhiteSpaceContext context)
        {
            builder.Append(context.WS());

            return base.VisitFirstFreeArgumentMandatoryWhiteSpace(context);
        }

        public override bool VisitFirstFreeArgumentOptionalWhiteSpace([NotNull] sphereScript99Parser.FirstFreeArgumentOptionalWhiteSpaceContext context)
        {
            builder.Append(context.WS());

            return base.VisitFirstFreeArgumentOptionalWhiteSpace(context);
        }

        private string lastSharpSubstitution;

        public override bool VisitEvalOperand([NotNull] sphereScript99Parser.EvalOperandContext context)
        {
            if (context.GetText().Equals("#", StringComparison.OrdinalIgnoreCase))
            {
                builder.Append(lastSharpSubstitution);
                return true;
            }

            return base.VisitEvalOperand(context);
        }

        public override bool VisitEvalOperator([NotNull] sphereScript99Parser.EvalOperatorContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitConstantExpression([NotNull] sphereScript99Parser.ConstantExpressionContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitRandomExpression([NotNull] sphereScript99Parser.RandomExpressionContext context)
        {
            builder.Append('{');
            var result = base.VisitRandomExpression(context);
            builder.Append('}');

            return result;
        }

        public override bool VisitRandomExpressionList([NotNull] sphereScript99Parser.RandomExpressionListContext context)
        {
            builder.Append(context.STARTING_WS?.Text);
            var result = base.VisitRandomExpressionList(context);
            builder.Append(context.ENDING_WS?.Text);

            return result;
        }

        public override bool VisitRandomExpressionElement([NotNull] sphereScript99Parser.RandomExpressionElementContext context)
        {
            builder.Append(context.WS());

            return base.VisitRandomExpressionElement(context);
        }

        public override bool VisitNonEscapedMacro([NotNull] sphereScript99Parser.NonEscapedMacroContext context)
        {
            builder.Append(context.LESS_THAN());
            var result = Visit(context.macroBody());
            builder.Append(context.MORE_THAN());

            return result;
        }

        public override bool VisitEscapedMacro([NotNull] sphereScript99Parser.EscapedMacroContext context)
        {
            builder.Append('<');
            var result = Visit(context.macroBody());
            builder.Append('>');

            return result;
        }

        public override bool VisitEvalSubExpression([NotNull] sphereScript99Parser.EvalSubExpressionContext context)
        {
            builder.Append('(');
            builder.Append(context.LEFT_WS?.Text);

            var result = Visit(context.numericExpression());
            builder.Append(context.RIGHT_WS?.Text);
            builder.Append(')');
            return result;
        }

        public override bool VisitFirstMemberAccessExpression([NotNull] sphereScript99Parser.FirstMemberAccessExpressionContext context)
        {
            string memberName = context.firstMemberAccess()?.customMemberAccess()?.memberName()?.GetText() ?? string.Empty;
            bool requiresMacro = semanticContext.IsNumeric && !memberName.Equals("strlen");

            if (requiresMacro)
                builder.Append('<');
            var result = base.VisitFirstMemberAccessExpression(context);
            if (requiresMacro)
                builder.Append('>');

            return result;
        }

        public override bool VisitEvalCall([NotNull] sphereScript99Parser.EvalCallContext context)
        {
            builder.Append(context.EVAL_FUNCTIONS());
            builder.Append(context.WS());

            return base.VisitEvalCall(context);
        }

        public override bool VisitFunctionSectionHeader([NotNull] sphereScript99Parser.FunctionSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        private ISet<string> globalVariables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private SemanticContext semanticContext = new SemanticContext();

        public override bool VisitFunctionSection([NotNull] sphereScript99Parser.FunctionSectionContext context)
        {
            return semanticContext.Execute(() => base.VisitFunctionSection(context));
        }

        public override bool VisitTriggerBody([NotNull] sphereScript99Parser.TriggerBodyContext context)
        {
            return semanticContext.Execute(() => base.VisitTriggerBody(context));
        }

        public override bool VisitStatement([NotNull] sphereScript99Parser.StatementContext context)
        {
            builder.Append(context.WS());
            var result = base.VisitStatement(context);

            builder.Append(context.NEWLINE());

            return result;
        }

        public override bool VisitAssignment([NotNull] sphereScript99Parser.AssignmentContext context)
        {
            Visit(context.firstMemberAccess());

            builder.Append(context.assign().GetText());

            return Visit(context.argumentList());
        }

        public override bool VisitItemDefSectionHeader([NotNull] sphereScript99Parser.ItemDefSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitPropertyAssignment([NotNull] sphereScript99Parser.PropertyAssignmentContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitTriggerHeader([NotNull] sphereScript99Parser.TriggerHeaderContext context)
        {
            builder.AppendLine(context.GetText());

            return true;
        }
        
        private void AppendTerminalsVisitNodes(IList<IParseTree> children)
        {
            if (children != null)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];

                    switch (child)
                    {
                        case sphereScript99Parser.MemberAccessContext memberAccess:
                            if (memberAccess.GetText() == " ")
                            {
                                builder.Append(memberAccess.GetText());
                            }
                            else
                                Visit(memberAccess);
                            break;
                        case ITerminalNode node:
                            builder.Append(node);
                            break;
                        default:
                            Visit(child);
                            break;
                    }
                }
            }
        }

        public override bool VisitUnquotedLiteralArgument([NotNull] sphereScript99Parser.UnquotedLiteralArgumentContext context)
        {
            AppendTerminalsVisitNodes(context.children);

            return true;
        }

        public override bool VisitQuotedLiteralArgument([NotNull] sphereScript99Parser.QuotedLiteralArgumentContext context)
        {
            builder.Append('"');
            AppendTerminalsVisitNodes(context.innerQuotedLiteralArgument().children);
            builder.Append('"');

            return true;
        }

        public override bool VisitNumericExpression([NotNull] sphereScript99Parser.NumericExpressionContext context)
        {
            try
            {
                semanticContext.EnterNumeric();
                return base.VisitNumericExpression(context);
            }
            finally
            {
                semanticContext.Leave();
            }
        }

        public override bool VisitIfStatement([NotNull] sphereScript99Parser.IfStatementContext context)
        {
            builder.Append(context.IF());
            builder.Append(context.IF_WS?.Text);

            Visit(context.condition());

            builder.Append(context.NEWLINE());
            Visit(context.codeBlock());

            var elseIfs = context.elseIfStatement();
            if (elseIfs != null)
            {
                foreach (var elseIf in elseIfs)
                {
                    builder.Append(elseIf.elseIf().GetText());
                    Visit(elseIf.condition());
                    builder.Append(elseIf.ELSEIF_NEWLINE.Text);
                    Visit(elseIf.codeBlock());
                }
            }

            if (context.elseStatement() != null)
                Visit(context.elseStatement());

            builder.Append(context.endIf().GetText());

            return true;
        }

        public override bool VisitElse([NotNull] sphereScript99Parser.ElseContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitWhileStatement([NotNull] sphereScript99Parser.WhileStatementContext context)
        {
            builder.Append(context.WHILE());
            builder.Append(context.WS());
            Visit(context.condition());
            builder.Append(context.NEWLINE());
            if (context.codeBlock() != null)
                Visit(context.codeBlock());
            builder.Append(context.endWhile().GetText());

            return true;
        }

        private bool AlwaysChainArguments(string name)
        {
            return name.Equals("findid", StringComparison.OrdinalIgnoreCase);
        }

        public override bool VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            var name = context.nativeFunctionName()?.GetText();
            if (!string.IsNullOrEmpty(name))
            {
                var arguments = context.nativeArgumentList()?.enclosedArgumentList()?.argumentList()?.argument();
                builder.Append(name);
                if (arguments != null && (context.chainedMemberAccess() != null || AlwaysChainArguments(name)))
                {
                    foreach (var argument in arguments)
                    {
                        builder.Append(".");
                        base.Visit(argument);
                    }

                    return base.Visit(context.chainedMemberAccess());
                }
                else
                {
                    var nativeArgumentList = context.nativeArgumentList();
                    if (nativeArgumentList != null)
                    {
                        return base.Visit(nativeArgumentList);
                    }
                }
            }

            return base.VisitNativeMemberAccess(context);
        }

        public override bool VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            var name = context.memberName()?.GetText();
            if (!string.IsNullOrEmpty(name))
            {
                var arguments = context.enclosedArgumentList()?.argumentList()?.argument();
                if (name.Equals("tag", StringComparison.OrdinalIgnoreCase) || name.Equals("var", StringComparison.OrdinalIgnoreCase))
                {
                    builder.Append(name);
                    if (semanticContext.IsNumeric && name.Equals("tag", StringComparison.OrdinalIgnoreCase))
                    {
                        builder.Append('0');
                    }
                    builder.Append(".");
                    if (arguments != null)
                    {
                        if (arguments.Length == 2)
                        {
                            if (name.Equals("var", StringComparison.OrdinalIgnoreCase))
                            {
                                globalVariables.Add(arguments[0].GetText());
                            }

                            builder.Append(arguments[0].GetText());
                            builder.Append("=");
                            return base.Visit(arguments[1]);
                        }
                        else if (arguments.Length == 1)
                        {
                            builder.Append(arguments[0].GetText());
                            return true;
                        }
                        else
                            throw new TranspilerException(context, $"Invalid number of arguments for 'tag': {arguments.Length}");
                    }
                    else
                    {
                        string chainedName = context.chainedMemberAccess()?.memberAccess()?.firstMemberAccess()?.customMemberAccess()?.memberName()?.GetText();
                        if (chainedName != null && chainedName.Equals("remove", StringComparison.OrdinalIgnoreCase))
                        {
                            var chainedArgument = context.chainedMemberAccess()?.memberAccess()?.firstMemberAccess()?.customMemberAccess()?.enclosedArgumentList()?.argumentList()?.argument();
                            if (chainedArgument != null && chainedArgument.Length == 1)
                            {
                                builder.Append($"{chainedArgument[0].GetText()}=");
                                return true;
                            }
                            else
                                throw new TranspilerException(context, $"Wrong number of arguments tag.remove: {chainedArgument.Length}");
                        }
                    }
                }
                else if (TransformFirstMemberAccessName(name))
                {
                    if (context.chainedMemberAccess() != null)
                        return base.Visit(context.chainedMemberAccess());

                    return true;
                }
                else
                {
                    if (arguments != null && (context.chainedMemberAccess() != null || AlwaysChainArguments(name)))
                    {
                        builder.Append(name);
                        foreach (var argument in arguments)
                        {
                            builder.Append(".");
                            base.Visit(argument);
                        }

                        if (context.chainedMemberAccess() != null)
                            return base.Visit(context.chainedMemberAccess());
                        else
                            return true;
                    }
                }
            }

            return base.VisitCustomMemberAccess(context);
        }

        private static Dictionary<string, string> skillMemberNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Skill_Alchemy", "alchemy" },
            { "Skill_Anatomy", "anatomy" },
            { "Skill_Animal_Lore", "animallore" },
            { "SKILL_APPRAISE", "appraise" },
            { "SKILL_ARMSLORE", "armslore" },
            { "SKILL_Parrying", "parrying" },
            { "SKILL_Begging", "begging" },
            { "SKILL_Blacksmith", "blacksmith" },
            { "Skill_Bowcraft", "bowcraft" },
            { "SKILL_PEACEMAKING", "peacemaking" },
            { "Skill_Camping", "camping" },
            { "Skill_Carpentry", "carpentry" },
            { "SKILL_CARTOGRAPHY", "cartography" },
            { "Skill_Cooking", "cooking" },
            { "Skill_DetectHidden", "detecthidden" },
            { "Skill_Enticement", "enticement" },
            { "Skill_EvalInt", "evalint" },
            { "Skill_Healing", "healing" },
            { "Skill_Fishing", "fishing" },
            { "Skill_Forensics", "forensics" },
            { "Skill_Herding", "herding" },
            { "Skill_Hiding", "hidding" },
            { "SKILL_PROVOCATION", "provocation" },
            { "Skill_Inscription", "inscription" },
            { "Skill_LockPick", "lockpick" },
            { "Skill_Magery", "magery" },
            { "Skill_MagicResist", "magicresist" },
            { "Skill_Tactics", "tactics" },
            { "Skill_Snooping", "snooping" },
            { "Skill_Musicianship", "musicianship" },
            { "SKILL_POISONING", "poisoning" },
            { "SKILL_SPIRITSPEAK", "spiritspeak" },
            { "Skill_Stealing", "stealing" },
            { "Skill_Tailoring", "tailoring" },
            { "Skill_Taming", "taming" },
            { "skill_tasteid", "tatedid" },
            { "Skill_Tinkering", "tinkering" },
            { "Skill_Tracking", "tracking" },
            { "Skill_Vet", "veterinary" },
            { "Skill_Swordsmanship", "swordsmanship" },
            { "Skill_Macefighting", "macefighting" },
            { "Skill_Fencing", "fencing" },
            { "Skill_Wrestling", "wrestling" },
            { "SKILL_LUMBERJACK", "lumberjack" },
            { "SKILL_MINING", "mining" },
            { "SKILL_MEDITATION", "meditation" },
            { "SKILL_STEALTH", "stealth" },
            { "SKILL_REMOVE_TRAP", "removetrap" },
            { "SKILL_Necromancy", "necromancy" },
        };

        private bool TransformFirstMemberAccessName(string name)
        {
            if (name.Equals("lastnew", StringComparison.OrdinalIgnoreCase))
            {
                builder.Append("new");
                return true;
            }

            if (skillMemberNames.TryGetValue(name, out string replacement))
            {
                builder.Append("serv.skill.");
                builder.Append(replacement);
                return true;
            }

            return false;
        }

        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            var name = context.customMemberAccess()?.memberName()?.GetText();
            if (!string.IsNullOrEmpty(name))
            {
                var arguments = context.customMemberAccess()?.enclosedArgumentList()?.argumentList()?.argument();

                if (name.Equals("arg", StringComparison.OrdinalIgnoreCase))
                {
                    if (arguments != null)
                    {
                        string localVariableName = arguments[0].GetText();
                        var localVariableAccess = $"local.{localVariableName}";
                        builder.Append(localVariableAccess);

                        if (arguments.Length == 2)
                        {
                            builder.Append('=');

                            try
                            {
                                lastSharpSubstitution = $"<{localVariableAccess}>";
                                semanticContext.DefineLocalVariable(localVariableName);

                                return VisitArgument(arguments[1]);
                            }
                            finally
                            {
                                lastSharpSubstitution = null;
                            }
                        }
                        else if (arguments.Length == 1)
                            return true;
                        else
                            throw new TranspilerException(context, $"Invalid number of arguments for 'arg': {arguments.Length}");
                    }
                    else
                        throw new TranspilerException(context, "No arguments for 'arg'");
                }
                else if (name.Equals("argcount", StringComparison.OrdinalIgnoreCase) || name.Equals("ARGVCOUNT", StringComparison.OrdinalIgnoreCase))
                {
                    builder.Append($"argv");
                    return true;
                }
                else if (name.Equals("argv", StringComparison.OrdinalIgnoreCase))
                {
                    if (arguments != null)
                    {
                        builder.Append($"argv[");
                        base.Visit(arguments[0]);
                        builder.Append("]");
                        return true;
                    }
                    else
                    {
                        throw new TranspilerException(context, "No arguments for 'argv'");
                    }
                }
                else if (name.Equals("strlen", StringComparison.OrdinalIgnoreCase))
                {
                    bool requiresMacro = false;

                    if (!(context.Parent.Parent.Parent.Parent.Parent.Parent is sphereScript99Parser.EvalCallContext))
                    {
                        if (context.Parent is sphereScript99Parser.MacroBodyContext)
                        {
                            builder.Append("eval ");
                            requiresMacro = false;
                        }
                        else
                        {
                            builder.Append("<eval ");
                            requiresMacro = true;
                        }

                        if (arguments.Length != 1)
                            throw new TranspilerException(context, $"Wrong number of arguments ({arguments.Length}) for strlen.");
                    }

                    builder.Append(name);
                    builder.Append('(');
                    Visit(arguments[0]);
                    builder.Append(')');

                    if (requiresMacro)
                        builder.Append(">");

                    return true;
                }
                else if (context.customMemberAccess() != null && arguments == null)
                {
                    if (semanticContext.IsLocalVariable(name))
                    {
                        builder.Append("local.");
                        builder.Append(name);

                        if (context.customMemberAccess().chainedMemberAccess() != null)
                            Visit(context.customMemberAccess().chainedMemberAccess());

                        return true;
                    }
                    else if (globalVariables.Contains(name))
                    {
                        builder.Append("var.");
                        builder.Append(name);

                        if (context.customMemberAccess().chainedMemberAccess() != null)
                            Visit(context.customMemberAccess().chainedMemberAccess());

                        return true;
                    }
                }
            }

            return base.VisitFirstMemberAccess(context);
        }

        public override bool VisitEofSection([NotNull] sphereScript99Parser.EofSectionContext context)
        {
            builder.Append(context.GetText());

            return base.VisitEofSection(context);
        }

        private class SemanticContext
        {
            private class Scope
            {
                HashSet<string> localVariables = new HashSet<string>();

                public Scope(bool isNumeric)
                {
                    IsNumeric = isNumeric;
                }

                public bool IsNumeric { get; }
                public bool IsVariable(string variableName) => localVariables.Contains(variableName);
                public void DefineVariable(string variableName) => localVariables.Add(variableName);
            }

            private Stack<Scope> scopes = new Stack<Scope>();

            public void Close()
            {

            }

            public void EnterNumeric()
            {
                scopes.Push(new Scope(true));
            }

            public T ExecuteNumeric<T>(Func<T> func)
            {
                try
                {
                    EnterNumeric();
                    return func();
                }
                finally
                {
                    Leave();
                }
            }

            public T Execute<T>(Func<T> func)
            {
                try
                {
                    Enter();
                    return func();
                }
                finally
                {
                    Leave();
                }
            }

            public void Enter()
            {
                scopes.Push(new Scope(false));
            }

            public void Leave()
            {
                scopes.Pop();
            }

            public void DefineLocalVariable(string variableName)
            {
                if (scopes.Count > 0)
                {
                    var scope = scopes.Peek();
                    scope.DefineVariable(variableName);
                }
            }

            public bool IsLocalVariable(string variableName)
            {
                if (scopes.Count > 0)
                {
                    foreach (var scope in scopes)
                    {
                        if (scope.IsVariable(variableName))
                            return true;
                    }
                }

                return false;
            }

            public bool IsNumeric
            {
                get
                {
                    if (scopes.Count > 0)
                    {
                        return scopes.Peek().IsNumeric;
                    }

                    return false;
                }
            }
        }
    }
}
