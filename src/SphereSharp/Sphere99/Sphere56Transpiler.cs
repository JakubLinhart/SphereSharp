using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using SphereSharp.Sphere99.Sphere56Transpiler;

namespace SphereSharp.Sphere99
{
    public sealed class Sphere56TranspilerVisitor : sphereScript99BaseVisitor<bool>
    {
        private readonly SpecialFunctionTranspiler specialFunctionTranspiler;
        private readonly ExpressionRequiresMacroVisitor expressionRequiresMacroVisitor = new ExpressionRequiresMacroVisitor();

        private readonly SourceCodeBuilder builder = new SourceCodeBuilder();

        public string Output => builder.Output;

        public Sphere56TranspilerVisitor()
        {
            specialFunctionTranspiler = new SpecialFunctionTranspiler(builder, this);
        }

        public override bool VisitFile([NotNull] sphereScript99Parser.FileContext context)
        {
            builder.Append(context.NEWLINE());

            return base.VisitFile(context);
        }

        public override bool VisitMemberName([NotNull] sphereScript99Parser.MemberNameContext context)
        {
            AppendTerminalsVisitNodes(context.children);

            return true;
        }

        public override bool VisitIndexedMemberName([NotNull] sphereScript99Parser.IndexedMemberNameContext context)
        {
            Visit(context.memberName());
            builder.Append('[');
            Visit(context.numericExpression());
            builder.Append(']');

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
            AppendArguments(context.argument());

            return true;
        }

        private void AppendArguments(IEnumerable<IParseTree> arguments)
        {
            if (arguments == null)
                return;

            var argumentCount = arguments.Count();
            if (argumentCount == 0)
                return;

            var firstArgument = arguments.First();
            Visit(firstArgument);

            foreach (var argument in arguments.Skip(1))
            {
                builder.Append(',');
                Visit(argument);
            }
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

        public override bool VisitUnaryOperator([NotNull] sphereScript99Parser.UnaryOperatorContext context)
        {
            builder.Append(context.GetText());

            return true;
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

        public override bool VisitMacro([NotNull] sphereScript99Parser.MacroContext context)
        {
            builder.StartMacro();
            base.VisitMacro(context);
            builder.EndMacro();

            return true;
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

        private readonly FirstMemberAccessNameVisitor firstMemberAccessNameVisitor = new FirstMemberAccessNameVisitor();

        public override bool VisitEvalCall([NotNull] sphereScript99Parser.EvalCallContext context)
        {
            builder.StartEvalCall();
            builder.Append(context.EVAL_FUNCTIONS());
            builder.Append(context.WS());

            base.VisitEvalCall(context);

            builder.EndEvalCall();

            return true;
        }

        public override bool VisitDialogTextSection([NotNull] sphereScript99Parser.DialogTextSectionContext context)
        {
            return false;
        }

        public override bool VisitDialogSection([NotNull] sphereScript99Parser.DialogSectionContext context)
        {
            return false;
        }

        public override bool VisitDialogButtonSection([NotNull] sphereScript99Parser.DialogButtonSectionContext context)
        {
            return false;
        }

        public override bool VisitEventsSectionHeader([NotNull] sphereScript99Parser.EventsSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitFunctionSectionHeader([NotNull] sphereScript99Parser.FunctionSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitDefNamesSectionHeader([NotNull] sphereScript99Parser.DefNamesSectionHeaderContext context)
        {
            builder.Append("[defname ");
            builder.Append(context.defNameSectionName().GetText());
            builder.Append(']');
            builder.Append(context.NEWLINE());

            return true;
        }

        public override bool VisitDefNamesSection([NotNull] sphereScript99Parser.DefNamesSectionContext context)
        {
            Visit(context.defNamesSectionHeader());
            Visit(context.propertyList());

            builder.AppendLine();
            builder.AppendLine();

            GenerateFunctionsForPropertyList(context.propertyList());

            return true;
        }

        public override bool VisitTypeDefSection([NotNull] sphereScript99Parser.TypeDefSectionContext context)
        {
            Visit(context.typeDefSectionHeader());
            Visit(context.triggerList());

            return true;
        }

        public override bool VisitTypeDefSectionHeader([NotNull] sphereScript99Parser.TypeDefSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitTypeDefsSection([NotNull] sphereScript99Parser.TypeDefsSectionContext context)
        {
            builder.Append(context.typeDefsSectionHeader().GetText());

            Visit(context.propertyList());
            builder.AppendLine();

            GenerateFunctionsForPropertyList(context.propertyList());

            return true;
        }

        public override bool VisitSpeechSectionHeader([NotNull] sphereScript99Parser.SpeechSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitSpeechTriggerHeader([NotNull] sphereScript99Parser.SpeechTriggerHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitCommentSection([NotNull] sphereScript99Parser.CommentSectionContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        private void GenerateFunctionsForPropertyList(sphereScript99Parser.PropertyListContext propertyList)
        {
            foreach (var property in propertyList.propertyAssignment())
            {
                builder.AppendLine($"[function {property.propertyName().GetText()}]");
                builder.AppendLine($"return {property.propertyValue().GetText()}");
                builder.AppendLine();
            }
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

            if (context.argumentList() != null)
                return Visit(context.argumentList());

            return true;
        }

        public override bool VisitItemDefSectionHeader([NotNull] sphereScript99Parser.ItemDefSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitPropertyAssignment([NotNull] sphereScript99Parser.PropertyAssignmentContext context)
        {
            if (context.LEADING_WS?.Text != null)
                builder.Append(context.LEADING_WS.Text);
            builder.Append(context.propertyName().GetText());
            builder.Append(context.propertyAssignmentOperator().GetText());

            var propertyValueText = context.propertyValue()?.GetText();
            if (propertyValueText != null)
                builder.Append(propertyValueText);

            builder.Append(context.NEWLINE());

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
                builder.StartNumericExpression();
                semanticContext.EnterRequireMacro();

                return base.VisitNumericExpression(context);
            }
            finally
            {
                builder.EndNumericExpression();
                semanticContext.Leave();
            }
        }

        public override bool VisitIfStatement([NotNull] sphereScript99Parser.IfStatementContext context)
        {
            builder.Append(context.IF());
            if (!string.IsNullOrEmpty(context.IF_WS?.Text))
                builder.Append(context.IF_WS?.Text);
            else
                builder.Append(' ');

            Visit(context.condition());

            builder.Append(context.NEWLINE());

            if (context.codeBlock() != null)
                Visit(context.codeBlock());

            var elseIfs = context.elseIfStatement();
            if (elseIfs != null)
            {
                foreach (var elseIf in elseIfs)
                {
                    builder.Append(elseIf.elseIf().GetText());
                    Visit(elseIf.condition());
                    builder.Append(elseIf.ELSEIF_NEWLINE.Text);
                    if (elseIf.codeBlock() != null)
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

        private HashSet<string> alwaysChainArgumentsFunctionNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "findid",
            "isevent"
        };

        private bool AlwaysChainArguments(string name) => alwaysChainArgumentsFunctionNames.Contains(name);

        public override bool VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            var name = context.nativeFunctionName()?.GetText();
            if (!string.IsNullOrEmpty(name))
            {
                var arguments = new FirstMemberAccessArgumentsVisitor().Visit(context);
                builder.Append(name);

                if (name.Equals("trigger", StringComparison.OrdinalIgnoreCase))
                {
                    var argumentList = context.nativeArgumentList().GetText().Trim().TrimStart('(').TrimEnd(')');
                    if (!argumentList.StartsWith("@"))
                    {
                        builder.Append(" @");
                        builder.Append(argumentList);
                        return true;
                    }
                }

                if (arguments != null && (context.chainedMemberAccess() != null || AlwaysChainArguments(name)))
                {
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

                if (arguments != null && arguments.Length > 0)
                {
                    builder.Append(' ');
                    AppendArguments(arguments);
                }
                return true;
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
                    bool requiresUid = context.chainedMemberAccess() != null && arguments != null;

                    if (requiresUid)
                        builder.Append("uid.<");

                    builder.Append(name);
                    if (semanticContext.IsNumeric)
                    {
                        builder.Append('0');
                    }
                    if (arguments != null)
                    {
                        builder.Append(".");
                        if (arguments.Length > 1)
                        {
                            if (name.Equals("var", StringComparison.OrdinalIgnoreCase))
                            {
                                builder.Append(arguments[0].GetText());
                                globalVariables.Add(arguments[0].GetText());
                            }
                            else
                                Visit(arguments[0]);

                            builder.Append("=");

                            Visit(arguments[1]);

                            if (name.Equals("tag", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var argument in arguments.Skip(2))
                                {
                                    builder.Append(",");
                                    Visit(argument);
                                }
                            }

                            return true;
                        }
                        else if (arguments.Length == 1)
                        {
                            builder.Append(arguments[0].GetText());

                            if (context.chainedMemberAccess() != null)
                            {
                                if (requiresUid)
                                    builder.Append(">");
                                Visit(context.chainedMemberAccess());
                            }

                            return true;
                        }
                        else
                            throw new TranspilerException(context, $"No arguments for '{name}'");
                    }
                    else
                    {
                        string chainedName = context.chainedMemberAccess()?.memberAccess()?.firstMemberAccess()?.customMemberAccess()?.memberName()?.GetText();
                        if (chainedName != null && chainedName.Equals("remove", StringComparison.OrdinalIgnoreCase))
                        {
                            builder.Append(".");
                            var chainedArgument = context.chainedMemberAccess()?.memberAccess()?.firstMemberAccess()?.customMemberAccess()?.enclosedArgumentList()?.argumentList()?.argument();
                            if (chainedArgument != null && chainedArgument.Length == 1)
                            {
                                builder.Append($"{chainedArgument[0].GetText()}=");
                                return true;
                            }
                        }
                        else
                        {
                            var secondChainedName = context.chainedMemberAccess()?.memberAccess()?.firstMemberAccess()?.customMemberAccess()?.chainedMemberAccess()?.memberAccess().firstMemberAccess()?.customMemberAccess()?.memberName()?.GetText();
                            if (secondChainedName != null && secondChainedName.Equals("remove", StringComparison.OrdinalIgnoreCase))
                            {
                                builder.Append('.');
                                builder.Append(chainedName);
                                builder.Append('=');
                                return true;
                            }
                            else
                            {
                                Visit(context.chainedMemberAccess());
                                return true;
                            }
                        }
                    }
                }
                else if (name.Equals("findres", StringComparison.OrdinalIgnoreCase))
                {
                    builder.Append("serv.");
                    Visit(arguments[0]);
                    builder.Append('.');
                    builder.StartArgumentRequiringEval();
                    Visit(arguments[1]);
                    builder.EndArgumentRequiringEval();

                    if (context.chainedMemberAccess() != null)
                        return base.Visit(context.chainedMemberAccess());
                }
                else if (TransformFirstMemberAccessName(name, context))
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
            { "Skill_DetectHidden", "DetectingHidden" },
            { "Skill_Enticement", "enticement" },
            { "Skill_EvalInt", "evalint" },
            { "Skill_Healing", "healing" },
            { "Skill_Fishing", "fishing" },
            { "Skill_Forensics", "forensics" },
            { "Skill_Herding", "herding" },
            { "Skill_Hiding", "hiding" },
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

        private bool TransformFirstMemberAccessName(string name, sphereScript99Parser.CustomMemberAccessContext context)
        {
            if (name.Equals("lastnew", StringComparison.OrdinalIgnoreCase))
            {
                bool requiresMacro = context.Parent?.Parent?.Parent?.Parent?.Parent?.Parent?.Parent?.Parent?.Parent is sphereScript99Parser.NativeArgumentListContext;
                if (requiresMacro)
                    builder.Append("<new>");
                else
                    builder.Append("new");
                return true;
            }
            else if (name.Equals("flag_underground", StringComparison.OrdinalIgnoreCase))
            {
                builder.Append("underground");
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
            if (new SafeTranspiler(builder, this).Visit(context) || specialFunctionTranspiler.Visit(context))
                return true;

            builder.StartMemberAccess();

            try
            {
                var name = firstMemberAccessNameVisitor.Visit(context);

                if (!string.IsNullOrEmpty(name))
                {
                    var arguments = context.customMemberAccess()?.enclosedArgumentList()?.argumentList()?.argument();

                    if (name.Equals("arg", StringComparison.OrdinalIgnoreCase))
                    {
                        if (arguments != null)
                        {
                            string localVariableName = arguments[0].GetText();
                            bool requiresMacro = context.Parent is sphereScript99Parser.FirstMemberAccessExpressionContext && arguments.Length == 1 && !semanticContext.IsNumeric;
                            if (requiresMacro)
                                builder.Append('<');

                            bool requiresUid = context.customMemberAccess()?.chainedMemberAccess() != null && arguments.Length == 1;
                            if (requiresUid)
                                builder.Append("uid.<");

                            var localVariableAccess = $"local.{localVariableName}";
                            builder.Append(localVariableAccess);
                            if (requiresMacro)
                                builder.Append('>');

                            if (arguments.Length > 1)
                            {
                                builder.Append('=');

                                try
                                {
                                    lastSharpSubstitution = $"<{localVariableAccess}>";
                                    semanticContext.DefineLocalVariable(localVariableName);
                                    VisitArgument(arguments[1]);

                                    foreach (var argument in arguments.Skip(2))
                                    {
                                        builder.Append(',');
                                        VisitArgument(argument);
                                    }

                                    return true;
                                }
                                finally
                                {
                                    lastSharpSubstitution = null;
                                }
                            }
                            else if (arguments.Length == 1)
                            {
                                if (requiresUid)
                                {
                                    builder.Append('>');
                                    Visit(context.customMemberAccess().chainedMemberAccess());
                                }
                                return true;
                            }
                        }
                        else
                        {
                            var chainedCall = context.customMemberAccess()?.chainedMemberAccess();
                            if (chainedCall != null)
                            {
                                if (chainedCall.memberAccess()?.firstMemberAccess()?.customMemberAccess()?.chainedMemberAccess() != null)
                                    throw new TranspilerException(context, "two chained calls after 'arg'");
                                builder.Append("local");
                                builder.Append(chainedCall.GetText());
                                return true;
                            }
                            else
                                throw new TranspilerException(context, "No arguments and no chained parameter for 'arg'");
                        }
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
                            builder.Append("args");
                            return true;
                        }
                    }
                    else if (name.Equals("args", StringComparison.OrdinalIgnoreCase))
                    {
                        if (context.customMemberAccess().chainedMemberAccess() != null)
                        {
                            builder.Append("<args>");
                            Visit(context.customMemberAccess().chainedMemberAccess());

                            return false;
                        }
                    }
                    else if (context.customMemberAccess() != null && arguments == null)
                    {
                        if (semanticContext.IsLocalVariable(name))
                        {
                            bool requiresMacro = context.Parent is sphereScript99Parser.FirstMemberAccessExpressionContext;
                            if (requiresMacro)
                                builder.Append('<');
                            builder.Append("local.");
                            builder.Append(name);

                            if (context.customMemberAccess().chainedMemberAccess() != null)
                                Visit(context.customMemberAccess().chainedMemberAccess());

                            if (requiresMacro)
                                builder.Append('>');

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
            finally
            {
                builder.EndMemberAccess();
            }
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

            public void EnterRequireMacro()
            {
                scopes.Push(new Scope(true));
            }

            public T RequireMacro<T>(Func<T> func)
            {
                try
                {
                    EnterRequireMacro();
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
