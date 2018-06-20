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

        private readonly HashSet<string> nativeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "p", "price", "lastxpos", "lastypos", "lastybuttpos"
        };

        private readonly SourceCodeBuilder builder;

        public string Output => builder.Output;

        public Sphere56TranspilerVisitor(IDefinitionsRepository definitionRepository, SourceCodeBuilder builder = null)
        {
            this.builder = builder ?? new SourceCodeBuilder();
            specialFunctionTranspiler = new SpecialFunctionTranspiler(this.builder, this);
            this.definitionRepository = definitionRepository ?? new DefinitionsRepository();
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
            string memberName = context.memberName().GetText();

            builder.StartMemberAccess();
            Visit(context.memberName());
            builder.Append('_');
            var indexMemberName = new FirstMemberAccessNameVisitor().Visit(context.numericExpression());
            if (indexMemberName == null || indexMemberName.Equals("eval", StringComparison.OrdinalIgnoreCase))
            {
                Visit(context.numericExpression());
            }
            else
            {
                builder.EnsureEvalCall("eval", () =>
                {
                    Visit(context.numericExpression());
                });
            }

            builder.Append('_');
            builder.EndMemberAccess();

            return true;
        }

        public override bool VisitChainedMemberAccess([NotNull] sphereScript99Parser.ChainedMemberAccessContext context)
        {
            builder.Append(".");

            builder.RestrictVariables();
            base.VisitChainedMemberAccess(context);
            builder.AllowVariables();

            return true;
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

        public void AppendArguments(IEnumerable<IParseTree> arguments)
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

        public override bool VisitTriggerArgument([NotNull] sphereScript99Parser.TriggerArgumentContext context)
        {
            AppendTriggerName(context.SYMBOL().GetText());

            return true;
        }

        public override bool VisitEvalOperand([NotNull] sphereScript99Parser.EvalOperandContext context)
        {
            if (context.GetText().Equals("#", StringComparison.OrdinalIgnoreCase))
            {
                builder.AppendLastSharpSubstitution();
                return true;
            }

            return base.VisitEvalOperand(context);
        }

        public override bool VisitEvalOperator([NotNull] sphereScript99Parser.EvalOperatorContext context)
        {
            if (context.LEADING_WS?.Text != null)
                builder.Append(context.LEADING_WS.Text);

            var op = context.evalBinaryOperator().GetText();
            if (context.evalBinaryOperator()?.rightBitShiftOperator() != null)
            {
                if (context.evalBinaryOperator().rightBitShiftOperator().LEADING_WS?.Text != null)
                    builder.Append(context.evalBinaryOperator().rightBitShiftOperator().LEADING_WS?.Text);
                builder.Append("<op_shiftright>"); 
                if (context.evalBinaryOperator().rightBitShiftOperator().LEADING_WS?.Text != null)
                    builder.Append(context.evalBinaryOperator().rightBitShiftOperator().TRAILING_WS?.Text);
            }
            else
                builder.Append(op);

            if (context.TRAILING_WS?.Text != null)
                builder.Append(context.TRAILING_WS.Text);

            return true;
        }

        public override bool VisitNumber([NotNull] sphereScript99Parser.NumberContext context)
        {
            if (context.HEX_NUMBER() != null)
            {
                builder.Append(context.HEX_NUMBER().GetText().TrimStart('#'));
                return true;
            }
            else if (context.DEC_NUMBER() != null)
            {
                builder.Append(context.DEC_NUMBER().GetText());
                return true;
            }

            return base.VisitNumber(context);
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

        public override bool VisitSectionName([NotNull] sphereScript99Parser.SectionNameContext context)
        {
            if (context.SYMBOL() != null)
                builder.Append(context.SYMBOL().GetText());
            else if (context.number() != null)
                Visit(context.number());

            return true;
        }

        public override bool VisitVarNamesSectionHeader([NotNull] sphereScript99Parser.VarNamesSectionHeaderContext context)
        {
            builder.Append("[GLOBALS]");
            if (context.NEWLINE() != null)
                builder.Append(context.NEWLINE().GetText());

            return true;
        }

        public override bool VisitWorldCharSection([NotNull] sphereScript99Parser.WorldCharSectionContext context)
        {
            Visit(context.worldCharSectionHeader());
            new CharSaveFilePropertiesTranspiler(builder, this).Visit(context.propertyList());

            return true;
        }

        public override bool VisitWorldCharSectionHeader([NotNull] sphereScript99Parser.WorldCharSectionHeaderContext context)
        {
            builder.Append(context.WORLDCHAR_SECTION_HEADER_START());
            Visit(context.sectionName());
            builder.Append(']');
            builder.Append(context.NEWLINE());

            return true;
        }

        public override bool VisitWorldItemSection([NotNull] sphereScript99Parser.WorldItemSectionContext context)
        {
            Visit(context.worldItemSectionHeader());
            new ItemSaveFilePropertiesTranspiler(builder, this).Visit(context.propertyList());

            return true;
        }

        public override bool VisitWorldItemSectionHeader([NotNull] sphereScript99Parser.WorldItemSectionHeaderContext context)
        {
            builder.Append(context.WORLDITEM_SECTION_HEADER_START());
            Visit(context.sectionName());
            builder.Append(']');
            builder.Append(context.NEWLINE());

            return true;
        }

        public override bool VisitSectorSectionHeader([NotNull] sphereScript99Parser.SectorSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
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

        public override bool VisitMenuSectionHeader([NotNull] sphereScript99Parser.MenuSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitMenuTriggerHeader([NotNull] sphereScript99Parser.MenuTriggerHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitScrollSectionHeader([NotNull] sphereScript99Parser.ScrollSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitPlevelSectionHeader([NotNull] sphereScript99Parser.PlevelSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitSkillMenuSectionHeader([NotNull] sphereScript99Parser.SkillMenuSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitSkillMenuItem([NotNull] sphereScript99Parser.SkillMenuItemContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitNewbieSection([NotNull] sphereScript99Parser.NewbieSectionContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitDefNamesSectionHeader([NotNull] sphereScript99Parser.DefNamesSectionHeaderContext context)
        {
            builder.Append("[defname");
            if (context.defNameSectionName() != null)
                builder.Append(context.defNameSectionName().GetText());
            builder.Append(']');
            builder.Append(context.NEWLINE());

            return true;
        }

        private string TransformDefName(sphereScript99Parser.PropertyAssignmentContext assignment)
        {
            var nameBuilder = new StringBuilder();

            nameBuilder.Append(assignment.propertyName().propertyNameText().GetText());
            if (assignment.propertyName().propertyNameIndex()?.number() != null)
            {
                nameBuilder.Append('_');
                nameBuilder.Append(assignment.propertyName().propertyNameIndex().number().GetText());
                nameBuilder.Append('_');
            }

            return nameBuilder.ToString();
        }

        public override bool VisitDefNamesSection([NotNull] sphereScript99Parser.DefNamesSectionContext context)
        {
            Visit(context.defNamesSectionHeader());

            if (context.propertyList().NEWLINE() != null)
                builder.Append(context.propertyList().NEWLINE().GetText());

            foreach (var assignment in context.propertyList().propertyAssignment())
            {
                if (assignment.LEADING_WS?.Text != null)
                    builder.Append(assignment.LEADING_WS.Text);

                builder.Append(TransformDefName(assignment));

                if (assignment.propertyAssignmentOperator() != null)
                    builder.Append(assignment.propertyAssignmentOperator().GetText());
                if (assignment.propertyValue() != null)
                    builder.Append(assignment.propertyValue().GetText());

                if (assignment.NEWLINE() != null)
                    builder.Append(assignment.NEWLINE());
            }

            builder.AppendLine();

            foreach (var assignment in context.propertyList().propertyAssignment())
            {
                string transformedDefName = TransformDefName(assignment);
                builder.AppendLine($"[function {transformedDefName}]");
                builder.AppendLine($"return <def.{transformedDefName}>");
                builder.AppendLine();
            }

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

        public override bool VisitProfessionSection([NotNull] sphereScript99Parser.ProfessionSectionContext context)
        {
            base.VisitProfessionSection(context);

            if (new PropertyValueExtractor().TryExtract("DEFNAME", context, out string professionName))
            {
                builder.AppendLine();
                builder.Append("[function ");
                builder.Append(professionName);
                builder.AppendLine(']');
                builder.Append("return ");
                builder.AppendLine(professionName);
            }
            else
                throw new TranspilerException(context, "no DEFNAME property found");

            return true;
        }

        public override bool VisitProfessionSectionHeader([NotNull] sphereScript99Parser.ProfessionSectionHeaderContext context)
        {
            builder.Append("[skillclass ");
            builder.Append(context.professionSectionName().GetText());
            builder.Append(']');
            builder.Append(context.NEWLINE());

            return true;
        }

        public override bool VisitSpellSectionHeader([NotNull] sphereScript99Parser.SpellSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitAreaSection([NotNull] sphereScript99Parser.AreaSectionContext context)
        {
            Visit(context.areaSectionHeader());

            if (context.propertyList()?.NEWLINE() != null)
                builder.Append(context.propertyList().NEWLINE().GetText());

            if (context.propertyList()?.propertyAssignment() != null)
            {
                if (new PropertyValueExtractor().TryExtract("mapplane", context.propertyList(), out string mapplaneValue))
                {
                    foreach (var assignment in context.propertyList().propertyAssignment())
                    {
                        AppendPropertyAssignmentWithoutNewLine(assignment);
                        if (assignment.propertyName().GetText().Equals("rect", StringComparison.OrdinalIgnoreCase))
                        {
                            builder.Append(',');
                            builder.Append(mapplaneValue);
                        }

                        builder.Append(assignment.NEWLINE());
                    }
                }
                else
                    Visit(context.propertyList());
            }

            return true;
        }

        public override bool VisitAreaSectionHeader([NotNull] sphereScript99Parser.AreaSectionHeaderContext context)
        {
            builder.Append("[AREADEF ");
            builder.Append(context.areaSectionName().GetText());
            builder.Append(']');
            builder.Append(context.NEWLINE());

            return true;
        }

        public override bool VisitRegionTypeSectionHeader([NotNull] sphereScript99Parser.RegionTypeSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitRegionResourceSectionHeader([NotNull] sphereScript99Parser.RegionResourceSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitNamesSectionHeader([NotNull] sphereScript99Parser.NamesSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitNamesCount([NotNull] sphereScript99Parser.NamesCountContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitFreeTextLine([NotNull] sphereScript99Parser.FreeTextLineContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitTemplateSectionHeader([NotNull] sphereScript99Parser.TemplateSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitSpawnSectionHeader([NotNull] sphereScript99Parser.SpawnSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitDialogSection([NotNull] sphereScript99Parser.DialogSectionContext context)
        {
            var dialogName = context.dialogSectionHeader().dialogName.Text;
            Visit(context.dialogSectionHeader());

            if (!new DialogPositionTranspiler(builder, this).Visit(context))
                builder.AppendLine("0,0");

            if (context.codeBlock() != null)
            {
                foreach (var statement in context.codeBlock().statement())
                {
                    VisitDialogStatement(dialogName, statement);
                }
            }

            return true;
        }

        public override bool VisitDialogPosition([NotNull] sphereScript99Parser.DialogPositionContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitDialogSectionHeader([NotNull] sphereScript99Parser.DialogSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitDialogButtonSectionHeader([NotNull] sphereScript99Parser.DialogButtonSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitDialogButtonTriggerHeader([NotNull] sphereScript99Parser.DialogButtonTriggerHeaderContext context)
        {
            var triggerName = context.dialogButtonTriggerName().GetText();
            if (triggerName.Equals("@anybutton", StringComparison.OrdinalIgnoreCase))
            {
                builder.Append("on=0 255");
                if (context.NEWLINE() != null)
                    builder.Append(context.NEWLINE().GetText());
            }
            else
                builder.Append(context.GetText());

            return true;
        }

        private bool replaceArgoWithSrc = false;

        public override bool VisitDialogButtonTrigger([NotNull] sphereScript99Parser.DialogButtonTriggerContext context)
        {
            replaceArgoWithSrc = true;
            base.VisitDialogButtonTrigger(context);
            replaceArgoWithSrc = false;

            return true;
        }

        public override bool VisitDialogTextSection([NotNull] sphereScript99Parser.DialogTextSectionContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        private void GenerateFunctionsForPropertyList(sphereScript99Parser.PropertyListContext propertyList)
        {
            foreach (var property in propertyList.propertyAssignment())
            {
                builder.AppendLine($"[function {property.propertyName().GetText()}]");
                builder.AppendLine($"return <def.{property.propertyName().GetText()}>");
                builder.AppendLine();
            }
        }

        private SemanticContext semanticContext = new SemanticContext();

        public override bool VisitFunctionSection([NotNull] sphereScript99Parser.FunctionSectionContext context)
        {
            semanticContext.ClearLocalVariables();
            base.VisitFunctionSection(context);
            semanticContext.ClearLocalVariables();

            return true;
        }

        private Dictionary<string, string> triggerNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "UserDClick", "dclick" },
        };

        public override bool VisitTriggerHeader([NotNull] sphereScript99Parser.TriggerHeaderContext context)
        {
            builder.Append(context.TRIGGER_HEADER());

            var triggerName = context.triggerName().GetText();
            AppendTriggerName(triggerName);

            return true;
        }

        private void AppendTriggerName(string triggerName)
        {
            builder.Append('@');
            if (triggerNames.TryGetValue(triggerName, out string translatedTriggerName))
                builder.Append(translatedTriggerName);
            else
                builder.Append(triggerName);
        }

        public override bool VisitTrigger([NotNull] sphereScript99Parser.TriggerContext context)
        {
            Visit(context.triggerHeader());

            if (context.NEWLINE() != null)
                builder.Append(context.NEWLINE().GetText());

            if (context.triggerBody() != null)
                Visit(context.triggerBody());

            return true;
        }

        public override bool VisitTriggerBody([NotNull] sphereScript99Parser.TriggerBodyContext context)
        {
            semanticContext.ClearLocalVariables();
            base.VisitTriggerBody(context);
            semanticContext.ClearLocalVariables();

            return true;
        }

        private Dictionary<string, string> genericGumpFunctionTranslations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "button", "button" },
            { "htmlgump", "htmlgump" },
            { "gumppic", "gumppic" },
            { "texta", "dtext" },
            { "textentry", "textentry" },
        };

        public bool VisitDialogStatement(string dialogName, [NotNull] sphereScript99Parser.StatementContext context)
        {
            var name = new FinalChainedMemberAccessNameVisitor().Visit(context);
            if (name != null)
            {
                var arguments = new FinalChainedMemberAccessArgumentsVisitor().Visit(context);
                if (name.Equals("settext", StringComparison.OrdinalIgnoreCase))
                {
                    if (context.WS() != null)
                        builder.Append(context.WS());

                    builder.Append("setdialogtext ");
                    builder.Append(dialogName);
                    builder.Append(',');
                    AppendArguments(arguments);

                    if (context.NEWLINE() != null)
                        builder.Append(context.NEWLINE().GetText());

                    return true;
                }
                else if (name.Equals("textentry", StringComparison.OrdinalIgnoreCase))
                {
                    if (context.WS() != null)
                        builder.Append(context.WS());

                    builder.Append("dtextentry ");
                    if (arguments.Length > 1)
                    {
                        new DialogFunctionArgumentsTranspiler(this, builder)
                            .AppendArguments(arguments.Reverse().Skip(1).Reverse());

                        builder.Append(' ');

                        builder.Append("<getdialogtext ");
                        builder.Append(dialogName);
                        builder.Append(',');
                        Visit(arguments.Last());
                        builder.Append('>');
                    }
                    else
                    {
                        var subArguments = arguments[0].GetText().Split(' ');
                        var newSubArguments = string.Join(" ", subArguments.Reverse().Skip(1).Reverse().ToArray());
                        builder.Append(newSubArguments);
                        builder.Append(' ');

                        builder.Append("<getdialogtext ");
                        builder.Append(dialogName);
                        builder.Append(',');
                        builder.Append(subArguments.Last());
                        builder.Append('>');
                    }

                    if (context.NEWLINE() != null)
                        builder.Append(context.NEWLINE().GetText());

                    return true;
                }
            }

            return VisitStatement(context);
        }

        public override bool VisitStatement([NotNull] sphereScript99Parser.StatementContext context)
        {
            var name = new FinalChainedMemberAccessNameVisitor().Visit(context);
            if (name != null)
            {
                var arguments = new FinalChainedMemberAccessArgumentsVisitor().Visit(context);

                if (name.Equals("htmlgumpa", StringComparison.OrdinalIgnoreCase))
                {
                    if (context.WS() != null)
                        builder.Append(context.WS());

                    builder.Append("dhtmlgump ");
                    Visit(arguments[0]);
                    builder.Append(' ');
                    Visit(arguments[1]);
                    builder.Append(' ');
                    Visit(arguments[2]);
                    builder.Append(' ');
                    Visit(arguments[3]);
                    builder.Append(' ');
                    Visit(arguments[5]);
                    builder.Append(' ');
                    Visit(arguments[6]);
                    builder.Append(' ');
                    new LiteralArgumentTranspiler(this, builder, true).Visit(arguments[4]);

                    if (context.NEWLINE() != null)
                        builder.Append(context.NEWLINE().GetText());

                    return true;
                }
                else if (name.Equals("dialog", StringComparison.OrdinalIgnoreCase))
                {
                    if (arguments.Length > 1)
                    {
                        if (context.WS() != null)
                            builder.Append(context.WS());

                        builder.Append("dialog ");
                        Visit(arguments[0]);
                        builder.Append(",0,");
                        AppendArguments(arguments.Skip(1));

                        if (context.NEWLINE() != null)
                            builder.Append(context.NEWLINE().GetText());

                        return true;
                    }
                }
                else if (genericGumpFunctionTranslations.TryGetValue(name, out string translatedName))
                {
                    if (context.WS() != null)
                        builder.Append(context.WS());

                    builder.Append(translatedName);
                    builder.Append(' ');

                    new DialogFunctionArgumentsTranspiler(this, builder).AppendArguments(arguments);

                    if (context.NEWLINE() != null)
                        builder.Append(context.NEWLINE().GetText());

                    return true;
                }
                else if (name.Equals("setlocation", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            builder.Append(context.WS());
            var result = base.VisitStatement(context);

            builder.Append(context.NEWLINE());

            return result;
        }

        public override bool VisitCall([NotNull] sphereScript99Parser.CallContext context)
        {
            builder.StartCall();
            base.VisitCall(context);
            builder.EndCall();

            return true;
        }

        public override bool VisitAssignment([NotNull] sphereScript99Parser.AssignmentContext context)
        {
            var name = new FinalChainedMemberAccessNameVisitor().Visit(context.firstMemberAccess());
            if (name != null)
            {
                if (name.Equals("setlocation", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            Visit(context.firstMemberAccess());

            builder.Append(context.assign().GetText());

            if (context.argumentList() != null)
                return Visit(context.argumentList());

            return true;
        }

        public override bool VisitItemDefSection([NotNull] sphereScript99Parser.ItemDefSectionContext context)
        {
            Visit(context.itemDefSectionHeader());
            if (context.propertyList() != null)
                Visit(context.propertyList());
            if (context.triggerList() != null)
                Visit(context.triggerList());

            string shadowFunctionName;
            if (context.propertyList() != null && new PropertyValueExtractor().TryExtract("defname", context.propertyList(), out string defName))
                shadowFunctionName = defName;
            else
                shadowFunctionName = context.itemDefSectionHeader().sectionName().GetText();

            builder.AppendLine();
            builder.Append("[function ");
            builder.Append(shadowFunctionName);
            builder.AppendLine(']');
            builder.Append("return ");
            builder.AppendLine(shadowFunctionName);
            builder.AppendLine();

            return true;
        }

        public override bool VisitItemDefSectionHeader([NotNull] sphereScript99Parser.ItemDefSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitCharDefSection([NotNull] sphereScript99Parser.CharDefSectionContext context)
        {
            Visit(context.charDefSectionHeader());
            if (context.propertyList() != null)
                Visit(context.propertyList());
            if (context.triggerList() != null)
                Visit(context.triggerList());

            string shadowFunctionName;
            if (context.propertyList() != null && new PropertyValueExtractor().TryExtract("defname", context.propertyList(), out string defName))
                shadowFunctionName = defName;
            else
                shadowFunctionName = context.charDefSectionHeader().sectionName().GetText();

            builder.AppendLine();
            builder.Append("[function ");
            builder.Append(shadowFunctionName);
            builder.AppendLine(']');
            builder.Append("return ");
            builder.AppendLine(shadowFunctionName);
            builder.AppendLine();

            return true;
        }

        public override bool VisitCharDefSectionHeader([NotNull] sphereScript99Parser.CharDefSectionHeaderContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        private readonly Dictionary<string, string> propertyNameDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "EI", "EvaluatingIntel" },
            { "Resist", "MagicResistance" },
            { "RUNEITEM", "RUNE_ITEM" },
            { "SCROLLITEM", "SCROLL_ITEM" },
            { "CASTTIME", "CAST_TIME" },
            { "EFFECTID", "EFFECT_ID" },
            { "ATTACK", "DAM" },
            { "RESOURCES2", "RESOURCES" },
            { "can_PILE", "pile" },
            { "can_replicate", "REPLICATE" },
            { "Age", "Create" },
        };
        private readonly ISet<string> promiles2PercentProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "AnimalLore", "Alchemy", "Anatomy","Archery","ArmsLore","Begging","Blacksmithing","Bowcraft","Camping",
            "Carpentry","Cartography","Cooking","DetectingHidden","Enticement","EI","Fencing","Fishing","Forensics",
            "Healing","Herding","Hiding","Inscription","ItemID","LockPicking","Lumberjacking","Macefighting","Magery",
            "Resist","Meditation","Mining","Musicianship","Parrying","Peacemaking","Poisoning","Provocation",
            "RemoveTrap","Snooping","SpiritSpeak","Stealing","Stealth","Swordsmanship","Tactics","Tailoring",
            "Taming","TasteID","Tinkering","Tracking","Veterinary","Wrestling",
        };
        private readonly IDefinitionsRepository definitionRepository;

        public void AppendPropertyAssignment(sphereScript99Parser.PropertyAssignmentContext context, string valueText = null)
        {
            AppendPropertyAssignmentWithoutNewLine(context, valueText);

            if (context.NEWLINE()?.GetText() != null)
                builder.Append(context.NEWLINE()?.GetText());
        }

        public void AppendPropertyAssignmentWithoutNewLine(sphereScript99Parser.PropertyAssignmentContext context, string propertyValueText = null)
        {
            if (context.LEADING_WS?.Text != null)
                builder.Append(context.LEADING_WS.Text);

            var originalPropertyName = context.propertyName().GetText();
            if (!propertyNameDictionary.TryGetValue(originalPropertyName, out string propertyName))
                propertyName = originalPropertyName;

            builder.Append(propertyName);

            if (context.propertyAssignmentOperator() != null)
                builder.Append(context.propertyAssignmentOperator().GetText());

            propertyValueText = propertyValueText ?? context.propertyValue()?.GetText();

            if (propertyValueText != null)
            {
                if (promiles2PercentProperties.Contains(originalPropertyName))
                {
                    if (int.TryParse(propertyValueText, out int propertyValueNumber))
                    {
                        if (propertyValueNumber != 0)
                        {
                            decimal percents = (decimal)propertyValueNumber / 10;
                            builder.Append($"{percents:###.0}");
                        }
                        else
                            builder.Append(propertyValueNumber.ToString());
                    }
                    else
                        builder.Append(propertyValueText);
                }
                else
                    builder.Append(propertyValueText);
            }
        }

        public override bool VisitPropertyAssignment([NotNull] sphereScript99Parser.PropertyAssignmentContext context)
        {
            AppendPropertyAssignmentWithoutNewLine(context);

            builder.Append(context.NEWLINE());

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
            new LiteralArgumentTranspiler(this, builder).Visit(context);
            return true;
        }

        public override bool VisitQuotedLiteralArgument([NotNull] sphereScript99Parser.QuotedLiteralArgumentContext context)
        {
            new LiteralArgumentTranspiler(this, builder).Visit(context);

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

        public override bool VisitDoswitchStatement([NotNull] sphereScript99Parser.DoswitchStatementContext context)
        {
            builder.Append(context.DOSWITCH());
            if (context.BEFORE_CONDITION_WS?.Text != null)
                builder.Append(context.BEFORE_CONDITION_WS.Text);

            Visit(context.condition());
            builder.Append(context.NEWLINE().GetText());
            Visit(context.codeBlock());

            if (context.AFTER_BLOCK_WS?.Text != null)
                builder.Append(context.AFTER_BLOCK_WS.Text);

            builder.Append(context.ENDDO().GetText());

            return true;
        }

        public override bool VisitTestIfStatement([NotNull] sphereScript99Parser.TestIfStatementContext context)
        {
            builder.Append(context.GetText());

            return true;
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

        public override bool VisitGenericNativeMemberAccess([NotNull] sphereScript99Parser.GenericNativeMemberAccessContext context)
        {
            if (context.nativeMemberAccess() == null)
            {
                var name = firstMemberAccessNameVisitor.Visit(context);
                var arguments = new FirstMemberAccessArgumentsVisitor().Visit(context);

                builder.Append(name);
                if (arguments != null && arguments.Length > 0)
                    builder.Append(' ');
                AppendArguments(arguments);

                return true;
            }

            return base.VisitGenericNativeMemberAccess(context);
        }

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
                    if (semanticContext.IsNumeric || requiresUid)
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
                                builder.RestrictVariables();
                                Visit(arguments[0]);
                                builder.AllowVariables();
                                builder.CaptureLastSharpSubstitution();
                            }
                            else
                            {
                                builder.RestrictVariables();
                                Visit(arguments[0]);
                                builder.CaptureLastSharpSubstitution();
                                builder.AllowVariables();
                            }

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
                        var chainedName = new FirstChainedMemberAccessNameVisitor().Visit(context);
                        if (chainedName != null && chainedName.Equals("remove", StringComparison.OrdinalIgnoreCase))
                        {
                            builder.Append(".");
                            var chainedArgument = new FinalChainedMemberAccessArgumentsVisitor().Visit(context);
                            if (chainedArgument != null && chainedArgument.Length == 1)
                            {
                                Visit(chainedArgument[0]);
                                builder.Append('=');
                                return true;
                            }
                        }
                        else
                        {
                            var secondChainedName = new ChainedMemberAccessNameVisitor(2).Visit(context);
                            if (secondChainedName != null && secondChainedName.Equals("remove", StringComparison.OrdinalIgnoreCase))
                            {
                                builder.Append('.');
                                builder.Append(chainedName);
                                builder.Append('=');
                                return true;
                            }
                            else
                            {
                                builder.RestrictVariables();
                                Visit(context.chainedMemberAccess());
                                builder.AllowVariables();
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
                    builder.StartRequireMacro();
                    Visit(arguments[1]);
                    builder.EndRequireMacro();

                    if (context.chainedMemberAccess() != null)
                        return base.Visit(context.chainedMemberAccess());

                    return true;
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
            { "Skill_EvalInt", "EvaluatingIntel" },
            { "Skill_Healing", "healing" },
            { "Skill_Fishing", "fishing" },
            { "Skill_Forensics", "forensics" },
            { "Skill_Herding", "herding" },
            { "Skill_Hiding", "hiding" },
            { "SKILL_PROVOCATION", "provocation" },
            { "Skill_Inscription", "inscription" },
            { "Skill_LockPick", "lockpick" },
            { "Skill_Magery", "magery" },
            { "Skill_MagicResist", "MagicResistance" },
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
            if (builder.MemberNameNeedsTranspilation)
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
                else if (name.Equals("profession", StringComparison.OrdinalIgnoreCase))
                {
                    builder.Append("skillclass");
                    return true;
                }

                if (skillMemberNames.TryGetValue(name, out string replacement))
                {
                    builder.StartMemberAccess();
                    builder.Append("serv.skill.");
                    builder.Append(replacement);
                    builder.EndMemberAccess();
                    return true;
                }
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
                                builder.StartRequireMacro();

                            builder.StartMemberAccess();
                            bool requiresUid = context.customMemberAccess()?.chainedMemberAccess() != null && arguments.Length == 1;
                            if (requiresUid)
                                builder.Append("uid.<");

                            var localVariableAccess = $"local.{localVariableName}";
                            builder.Append(localVariableAccess);
                            builder.EndMemberAccess();
                            builder.CaptureLastSharpSubstitution();

                            if (requiresMacro)
                                builder.EndRequireMacro();

                            if (arguments.Length > 1)
                            {
                                builder.Append('=');

                                semanticContext.DefineLocalVariable(localVariableName);
                                VisitArgument(arguments[1]);

                                foreach (var argument in arguments.Skip(2))
                                {
                                    builder.Append(',');
                                    VisitArgument(argument);
                                }

                                return true;
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
                    else if (name.Equals("argo", StringComparison.OrdinalIgnoreCase) && replaceArgoWithSrc)
                    {
                        builder.Append("src");
                        if (context.customMemberAccess()?.chainedMemberAccess() != null)
                            Visit(context.customMemberAccess().chainedMemberAccess());
                        return true;
                    }
                    else if (name.Equals("argv", StringComparison.OrdinalIgnoreCase))
                    {
                        if (arguments != null)
                        {
                            bool requiresUid = new HasChainedMemberVisitor().Visit(context);
                            if (requiresUid)
                                builder.Append("uid.<");
                            builder.Append($"argv[");
                            base.Visit(arguments[0]);
                            builder.Append("]");
                            if (requiresUid)
                            {
                                builder.Append('>');
                                new ChainedMemberTranspiler(builder, this).Visit(context);
                            }
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
                            builder.Append("uid.<args>");
                            Visit(context.customMemberAccess().chainedMemberAccess());

                            return false;
                        }
                    }
                    else if (name.Equals("strmid", StringComparison.OrdinalIgnoreCase))
                    {
                        builder.Append("strsub");
                        builder.Append(' ');
                        Visit(arguments[1]);
                        builder.Append(' ');
                        Visit(arguments[2]);
                        builder.Append(' ');
                        new LiteralArgumentTranspiler(this, builder, true).Visit(arguments[0]);

                        return true;
                    }
                    else if (name.Equals("finduid", StringComparison.OrdinalIgnoreCase))
                    {
                        builder.StartMemberAccess();
                        builder.Append("uid.");
                        builder.StartRequireMacro();
                        Visit(arguments[0]);
                        builder.EndRequireMacro();

                        if (context.customMemberAccess().chainedMemberAccess() != null)
                            Visit(context.customMemberAccess().chainedMemberAccess());

                        builder.EndMemberAccess();

                        return true;
                    }
                    else if (name.Equals("findlayer", StringComparison.OrdinalIgnoreCase) && arguments != null)
                    {
                        builder.StartMemberAccess();
                        builder.Append("findlayer.");
                        builder.StartRequireMacro();
                        Visit(arguments[0]);
                        builder.EndRequireMacro();

                        if (context.customMemberAccess().chainedMemberAccess() != null)
                            Visit(context.customMemberAccess().chainedMemberAccess());

                        builder.EndMemberAccess();

                        return true;
                    }
                    else if (context.customMemberAccess() != null && arguments == null)
                    {
                        if (semanticContext.IsLocalVariable(name))
                        {
                            bool requiresUid = new HasChainedMemberVisitor().Visit(context);
                            if (requiresUid)
                            {
                                builder.Append("uid.");
                                builder.StartMemberAccess();
                                builder.StartRequireMacro();
                            }

                            builder.AppendLocalVariable(name);

                            if (requiresUid)
                            {
                                builder.EndRequireMacro();
                                builder.EndMemberAccess();

                                new ChainedMemberTranspiler(builder, this).Visit(context);
                            }

                            return true;
                        }
                        else if (definitionRepository.IsGlobalVariable(name) && !nativeNames.Contains(name))
                        {
                            bool requiresUid = new HasChainedMemberVisitor().Visit(context);
                            if (requiresUid)
                            {
                                builder.Append("uid.");
                                builder.StartMemberAccess();
                                builder.StartRequireMacro();
                            }

                            builder.StartGlobalVariable();
                            new FirstMemberAccessNameTranspiler(builder, this).Visit(context);
                            builder.EndGlobalVariable();

                            if (requiresUid)
                            {
                                builder.EndRequireMacro();
                                builder.EndMemberAccess();

                                new ChainedMemberTranspiler(builder, this).Visit(context);
                            }

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
            HashSet<string> localVariables = new HashSet<string>();

            private class Scope
            {
                public Scope(bool isNumeric)
                {
                    IsNumeric = isNumeric;
                }

                public bool IsNumeric { get; }
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

            public void ClearLocalVariables() => localVariables.Clear();
            public void DefineLocalVariable(string variableName) => localVariables.Add(variableName);
            public bool IsLocalVariable(string variableName) => localVariables.Contains(variableName);

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
