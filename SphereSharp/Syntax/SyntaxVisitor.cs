using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public abstract class SyntaxVisitor
    {
        public virtual void VisitArgumentList(ArgumentListSyntax argumentList) => DefaultVisit(argumentList);
        public virtual void VisitEofSection(EofSectionSyntax eofSectionSyntax) => DefaultVisit(eofSectionSyntax);
        public virtual void VisitEventsStatement(EventsStatementSyntax syntaxNode) => DefaultVisit(syntaxNode);
        public virtual void VisitTextArgument(TextArgumentSyntax textArgumentSyntax) => DefaultVisit(textArgumentSyntax);
        public virtual void VisitLiteralArgument(LiteralArgumentSyntax literalArgumentSyntax) => DefaultVisit(literalArgumentSyntax);
        public virtual void VisitWhileStatement(WhileStatementSyntax whileStatementSyntax) => DefaultVisit(whileStatementSyntax);
        public virtual void VisitExpressionArgument(ExpressionArgumentSyntax expressionArgumentSyntax) => DefaultVisit(expressionArgumentSyntax);
        public virtual void VisitLiteral(LiteralSyntax literalSyntax) => DefaultVisit(literalSyntax);
        public virtual void VisitDefName(DefNameSyntax defNameSyntax) => DefaultVisit(defNameSyntax);
        public virtual void VisitEvalMacroSegment(EvalMacroSegmentSyntax evalMacroSegmentSyntax) => DefaultVisit(evalMacroSegmentSyntax);
        public virtual void VisitSection(SectionSyntax sectionSyntax) => DefaultVisit(sectionSyntax);
        public virtual void VisitSymbol(SymbolSyntax symbolSyntax) => DefaultVisit(symbolSyntax);
        public virtual void VisitIndexedSymbol(IndexedSymbolSyntax indexedSymbolSyntax) => DefaultVisit(indexedSymbolSyntax);
        public virtual void VisitTextSegment(TextSegmentSyntax textSegmentSyntax) => DefaultVisit(textSegmentSyntax);
        public virtual void VisitMacroSegment(MacroSegmentSyntax macroSegmentSyntax) => DefaultVisit(macroSegmentSyntax);
        public virtual void VisitEvalMacro(EvalMacroSyntax evalMacroSyntax) => DefaultVisit(evalMacroSyntax);
        public virtual void VisitEvalMacroExpression(MacroExpressionSyntax macroExpressionSyntax) => DefaultVisit(macroExpressionSyntax);
        public virtual void VisitCodeBlock(CodeBlockSyntax codeBlockSyntax) => DefaultVisit(codeBlockSyntax);
        public virtual void VisitMacro(MacroSyntax macroSyntax) => DefaultVisit(macroSyntax);
        public virtual void VisitEval(EvalSyntax evalSyntax) => DefaultVisit(evalSyntax);
        public virtual void VisitFile(FileSyntax fileSyntax) => DefaultVisit(fileSyntax);
        public virtual void VisitProperty(PropertySyntax propertySyntax) => DefaultVisit(propertySyntax);
        public virtual void VisitFunctionSection(FunctionSectionSyntax functionSectionSyntax) => DefaultVisit(functionSectionSyntax);
        public virtual void VisitEventsSection(EventsSectionSyntax eventsSectionSyntax) => DefaultVisit(eventsSectionSyntax);
        public virtual void VisitTemplateSection(TemplateSectionSyntax templateSectionSyntax) => DefaultVisit(templateSectionSyntax);
        public virtual void VisitSpellSection(SpellSectionSyntax spellSectionSyntax) => DefaultVisit(spellSectionSyntax);
        public virtual void VisitDialogTextsSection(DialogTextsSectionSyntax dialogTextsSectionSyntax) => DefaultVisit(dialogTextsSectionSyntax);
        public virtual void VisitProfessionSection(ProfessionSectionSyntax professionSectionSyntax) => DefaultVisit(professionSectionSyntax);
        public virtual void VisitSkillSection(SkillSectionSyntax skillSectionSyntax) => DefaultVisit(skillSectionSyntax);
        public virtual void VisitItemDefSection(ItemDefSectionSyntax itemDefSectionSyntax) => DefaultVisit(itemDefSectionSyntax);
        public virtual void VisitDialogSection(DialogSectionSyntax dialogSectionSyntax) => DefaultVisit(dialogSectionSyntax);
        public virtual void VisitDialogButtonsSection(DialogButtonsSectionSyntax dialogButtonsSectionSyntax) => DefaultVisit(dialogButtonsSectionSyntax);
        public virtual void VisitDefNamesSection(DefNamesSectionSyntax defNamesSectionSyntax) => DefaultVisit(defNamesSectionSyntax);
        public virtual void VisitTrigger(TriggerSyntax triggerSyntax) => DefaultVisit(triggerSyntax);
        public virtual void VisitUnaryOperator(UnaryOperatorSyntax unaryOperatorSyntax) => DefaultVisit(unaryOperatorSyntax);
        public virtual void VisitCallExpression(CallExpressionSyntax callExpressionSyntax) => DefaultVisit(callExpressionSyntax);
        public virtual void AcceptEvalMacroExpression(EvalMacroExpressionSyntax evalMacroExpressionSyntax) => DefaultVisit(evalMacroExpressionSyntax);
        public virtual void VisitCharDefSection(CharDefSectionSyntax charDefSectionSyntax) => DefaultVisit(charDefSectionSyntax);
        public virtual void VisitCall(CallSyntax syntaxNode) => DefaultVisit(syntaxNode);
        public virtual void VisitIntegerConstantExpression(IntegerConstantExpressionSyntax integerConstantExpressionSyntax) => DefaultVisit(integerConstantExpressionSyntax);
        public virtual void VisitBinaryOperator(BinaryOperatorSyntax binaryOperatorSyntax) => DefaultVisit(binaryOperatorSyntax);
        public virtual void VisitReturn(ReturnSyntax returnSyntax) => DefaultVisit(returnSyntax);
        public virtual void VisitMacroStatement(MacroStatementSyntax macroStatementSyntax) => DefaultVisit(macroStatementSyntax);
        public virtual void VisitMacroIntegerConstantExpression(MacroIntegerConstantExpressionSyntax macroIntegerConstantExpressionSyntax) => DefaultVisit(macroIntegerConstantExpressionSyntax);
        public virtual void VisitDoSwitch(DoSwitchSyntax doSwitchSyntax) => DefaultVisit(doSwitchSyntax);
        public virtual void VisitIf(IfSyntax ifSyntax) => DefaultVisit(ifSyntax);
        public virtual void VisitElseIf(ElseIfSyntax elseIfSyntax) => DefaultVisit(elseIfSyntax);
        public virtual void VisitDecimalConstantExpression(DecimalConstantExpressionSyntax decimalConstantExpressionSyntax) => DefaultVisit(decimalConstantExpressionSyntax);
        public virtual void VisitIntervalExpression(IntervalExpressionSyntax intervalExpressionSyntax) => DefaultVisit(intervalExpressionSyntax);
        public virtual void VisitAssignment(AssignmentSyntax syntaxNode) => DefaultVisit(syntaxNode);

        public virtual void Visit(SyntaxNode node)
        {
            if (node != null)
                node.Accept(this);
        }

        public virtual void DefaultVisit(SyntaxNode node) { }
    }
}
