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
    }
}
