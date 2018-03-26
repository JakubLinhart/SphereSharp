namespace SphereSharp.Syntax
{
    public sealed class UnaryOperatorSyntax : ExpressionSyntax
    {
        public UnaryOperatorKind Kind { get; }
        public ExpressionSyntax Operand { get; }

        public UnaryOperatorSyntax(UnaryOperatorKind kind, ExpressionSyntax operand)
        {
            Kind = kind;
            Operand = operand;
        }
    }
}
