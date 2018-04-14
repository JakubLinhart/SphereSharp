using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class EvalMacroSegmentSyntax : SegmentSyntax
    {
        public EvalMacroSyntax Macro { get; }
        public ExpressionSyntax Expression { get; }

        public EvalMacroSegmentSyntax(EvalMacroSyntax macro)
        {
            Macro = macro;
            Expression = macro.Eval.Expression;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitEvalMacroSegment(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
            yield return Expression;
        }
    }
}
