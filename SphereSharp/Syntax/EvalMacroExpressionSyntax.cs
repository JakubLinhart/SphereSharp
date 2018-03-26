namespace SphereSharp.Syntax
{
    public class EvalMacroExpressionSyntax : ExpressionSyntax
    {
        public EvalMacroSyntax Macro { get; }
        public ExpressionSyntax Expression { get; }

        public EvalMacroExpressionSyntax(EvalMacroSyntax macro)
        {
            Macro = macro;
            Expression = macro.Eval.Expression;
        }
    }
}
