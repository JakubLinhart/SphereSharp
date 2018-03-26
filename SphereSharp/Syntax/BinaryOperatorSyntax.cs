namespace SphereSharp.Syntax
{
    public sealed class BinaryOperatorSyntax : ExpressionSyntax
    {
        public BinaryOperatorKind Kind { get; }
        public ExpressionSyntax Operand1 { get; }
        public ExpressionSyntax Operand2 { get; }

        public BinaryOperatorSyntax(BinaryOperatorKind kind, ExpressionSyntax operand1, ExpressionSyntax operand2)
        {
            Kind = kind;
            Operand1 = operand1;
            Operand2 = operand2;
        }
    }
}
