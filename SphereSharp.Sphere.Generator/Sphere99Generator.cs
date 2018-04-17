using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SphereSharp.Sphere.Generator
{

    public class Sphere99Generator : SyntaxWalker
    {
        private IndentedStringBuilder builder = new IndentedStringBuilder();

        private void Generate(SectionSyntax section)
        {
            builder.Append($"[{section.Type} {section.Name}");
            if (!string.IsNullOrEmpty(section.SubType))
            {
                builder.Append(' ');
                builder.Append(section.SubType);
            }
            builder.AppendLine(']');
            DefaultVisit(section);
            builder.AppendLine();
        }

        public override void VisitCharDefSection(CharDefSectionSyntax charDefSectionSyntax) => Generate(charDefSectionSyntax);
        public override void VisitDialogButtonsSection(DialogButtonsSectionSyntax dialogButtonsSectionSyntax) => Generate(dialogButtonsSectionSyntax);
        public override void VisitDialogTextsSection(DialogTextsSectionSyntax dialogTextsSectionSyntax) => Generate(dialogTextsSectionSyntax);

        public override void VisitFunctionSection(FunctionSectionSyntax functionSectionSyntax)
        {
            Generate(functionSectionSyntax);
        }

        public override void VisitDefNamesSection(DefNamesSectionSyntax defNamesSectionSyntax) => Generate(defNamesSectionSyntax);
        public override void VisitDialogSection(DialogSectionSyntax dialogSectionSyntax) => Generate(dialogSectionSyntax);
        public override void VisitEventsSection(EventsSectionSyntax eventsSectionSyntax) => Generate(eventsSectionSyntax);
        public override void VisitItemDefSection(ItemDefSectionSyntax itemDefSectionSyntax) => Generate(itemDefSectionSyntax);
        public override void VisitProfessionSection(ProfessionSectionSyntax professionSectionSyntax) => Generate(professionSectionSyntax);
        public override void VisitSkillSection(SkillSectionSyntax skillSectionSyntax) => Generate(skillSectionSyntax);
        public override void VisitSpellSection(SpellSectionSyntax spellSectionSyntax) => Generate(spellSectionSyntax);
        public override void VisitTemplateSection(TemplateSectionSyntax templateSectionSyntax) => Generate(templateSectionSyntax);

        public override void VisitIndexedSymbol(IndexedSymbolSyntax indexedSymbolSyntax)
        {
            foreach (var segment in indexedSymbolSyntax.Segments)
                Visit(segment);
            builder.Append('[');
            Visit(indexedSymbolSyntax.Index);
            builder.Append(']');
        }

        public override void VisitTrigger(TriggerSyntax triggerSyntax)
        {
            builder.Append("on=");
            if (triggerSyntax.IsNamedTrigger)
                builder.Append('@');
            builder.AppendLine(triggerSyntax.Name);

            base.Visit(triggerSyntax.CodeBlock);
            builder.AppendLine();
        }

        public override void VisitEventsStatement(EventsStatementSyntax syntaxNode)
        {
            builder.Append("events ");
            switch (syntaxNode.Kind)
            {
                case EventsOperationKind.Subscribe:
                    break;
                case EventsOperationKind.Unsubscribe:
                    builder.Append('-');
                    break;
                default:
                    throw new NotImplementedException($"Events statement kind {syntaxNode.Kind}");
            }

            builder.Append(syntaxNode.EventName);
        }

        public override void VisitTextSegment(TextSegmentSyntax textSegmentSyntax)
        {
            builder.Append(textSegmentSyntax.Text);
            base.DefaultVisit(textSegmentSyntax);
        }

        public override void VisitArgumentList(ArgumentListSyntax argumentList)
        {
            if (!argumentList.IsEmpty)
            {
                if (!argumentList.Arguments.IsEmpty)
                {
                    builder.Append('(');
                    Visit(argumentList.Arguments.First());
                    foreach (var argument in argumentList.Arguments.Skip(1))
                    {
                        builder.Append(",");
                        Visit(argument);
                    }
                    builder.Append(')');
                }
            }
        }

        public override void VisitTextArgument(TextArgumentSyntax textArgumentSyntax)
        {
            builder.Append(textArgumentSyntax.Text);
        }

        public override void VisitResourceArgument(ResourceArgumentSyntax resourceArgumentSyntax)
        {
            Visit(resourceArgumentSyntax.Amount);
            builder.Append(' ');
            Visit(resourceArgumentSyntax.Name);
        }

        public override void VisitMacro(MacroSyntax macroSyntax)
        {
            builder.Append('<');
            base.VisitMacro(macroSyntax);
            builder.Append('>');
        }

        public override void VisitEval(EvalSyntax evalSyntax)
        {
            builder.Append("eval ");
            base.VisitEval(evalSyntax);
        }

        public override void VisitEvalMacro(EvalMacroSyntax evalMacroSyntax)
        {
            builder.Append('<');
            base.VisitEvalMacro(evalMacroSyntax);
            builder.Append('>');
        }

        public override void VisitProperty(PropertySyntax propertySyntax)
        {
            builder.Append(propertySyntax.LValue);
            builder.Append("=");
            builder.AppendLine(propertySyntax.RValue);
        }

        public override void VisitDefName(DefNameSyntax defNameSyntax)
        {
            builder.Append(defNameSyntax.LValue);
            builder.Append('\t');
            builder.AppendLine(defNameSyntax.RValue);
        }

        public override void VisitIf(IfSyntax ifSyntax)
        {
            builder.Append("if ");
            Visit(ifSyntax.Condition);
            builder.AppendLine();

            VisitIndetedCodeBlock(ifSyntax.ThenBlock);

            foreach (var elseIf in ifSyntax.ElseIfs)
                Visit(elseIf);

            if (ifSyntax.ElseBlock.Statements.Any())
            {
                builder.AppendLine("else");
                builder.Indent();
                VisitIndetedCodeBlock(ifSyntax.ElseBlock);
                builder.Unindent();
            }

            builder.Append("endif");
        }

        public override void VisitElseIf(ElseIfSyntax elseIfSyntax)
        {
            builder.Append("elseif ");
            Visit(elseIfSyntax.Condition);
            builder.AppendLine();

            VisitIndetedCodeBlock(elseIfSyntax.ThenBlock);
        }

        public override void VisitWhileStatement(WhileStatementSyntax whileStatementSyntax)
        {
            builder.Append("while ");
            Visit(whileStatementSyntax.Condition);
            builder.AppendLine();

            VisitIndetedCodeBlock(whileStatementSyntax.Body);

            builder.Append("endwhile");
        }

        private void VisitIndetedCodeBlock(CodeBlockSyntax codeBlock)
        {
            if (!codeBlock.IsEmpty)
            {
                builder.Indent();
                Visit(codeBlock);
                builder.Unindent();
            }
        }

        public override void VisitDoSwitch(DoSwitchSyntax doSwitchSyntax)
        {
            builder.Append("doswitch ");
            Visit(doSwitchSyntax.Condition);
            builder.AppendLine();
            builder.Indent();
            foreach (var @case in doSwitchSyntax.Cases)
            {
                Visit(@case);
                builder.AppendLine();
            }

            builder.Unindent();
            builder.Append("enddo");
        }

        public override void VisitReturn(ReturnSyntax returnSyntax)
        {
            builder.Append("return ");
            Visit(returnSyntax.Argument);
        }

        public override void VisitAssignment(AssignmentSyntax syntaxNode)
        {
            Visit(syntaxNode.LValue);
            builder.Append('=');
            Visit(syntaxNode.RValue);
        }

        public override void VisitCall(CallSyntax syntaxNode)
        {
            Visit(syntaxNode.MemberNameSyntax);
            Visit(syntaxNode.Arguments);
            if (syntaxNode.ChainedCall != null)
            {
                builder.Append('.');
                Visit(syntaxNode.ChainedCall);
            }
        }

        public override void VisitCallExpression(CallExpressionSyntax callExpressionSyntax)
        {
            if (callExpressionSyntax.Enclosed)
                builder.Append('(');

            base.VisitCallExpression(callExpressionSyntax);

            if (callExpressionSyntax.Enclosed)
                builder.Append(')');
        }

        public override void VisitCodeBlock(CodeBlockSyntax codeBlockSyntax)
        {
            var children = codeBlockSyntax.GetChildNodes();
            if (children.Any())
            {
                foreach (var statement in children)
                {
                    Visit(statement);
                    builder.AppendLine();
                }
            }
        }

        public override void VisitUnaryOperator(UnaryOperatorSyntax unaryOperatorSyntax)
        {
            builder.Append(unaryOperatorSyntax.OperatorString);
            Visit(unaryOperatorSyntax.Operand);
        }

        public override void VisitBinaryOperator(BinaryOperatorSyntax binaryOperatorSyntax)
        {
            if (binaryOperatorSyntax.Enclosed)
                builder.Append('(');

            Visit(binaryOperatorSyntax.Operand1);
            builder.Append(binaryOperatorSyntax.OperatorString);
            Visit(binaryOperatorSyntax.Operand2);

            if (binaryOperatorSyntax.Enclosed)
                builder.Append(')');
        }

        public override void VisitIntegerConstantExpression(IntegerConstantExpressionSyntax integerConstantExpressionSyntax)
        {
            switch (integerConstantExpressionSyntax.Kind)
            {
                case ConstantExpressionSyntaxKind.Hex:
                    builder.Append('0');
                    break;
                case ConstantExpressionSyntaxKind.Decadic:
                    break;
                default:
                    throw new NotImplementedException($"Integer constant expression kind {integerConstantExpressionSyntax.Kind}");
            }

            builder.Append(integerConstantExpressionSyntax.Value);
        }

        public override void VisitEofSection(EofSectionSyntax eofSectionSyntax)
        {
            builder.AppendLine("[EOF]");
        }

        public override string ToString() => builder.ToString();
    }
}
