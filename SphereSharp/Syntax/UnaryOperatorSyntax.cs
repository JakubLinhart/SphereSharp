using System;
using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class UnaryOperatorSyntax : ExpressionSyntax
    {
        public UnaryOperatorKind Kind { get; }
        public ExpressionSyntax Operand { get; }
        public string OperatorString
        {
            get
            {
                switch (Kind)
                {
                    case UnaryOperatorKind.LogicalNot:
                        return "!";
                    case UnaryOperatorKind.BitComplement:
                        return "~";
                    default:
                        throw new NotImplementedException($"Unary operator kind {Kind}.");
                }
            }
        }

        public UnaryOperatorSyntax(UnaryOperatorKind kind, ExpressionSyntax operand)
        {
            Kind = kind;
            Operand = operand;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitUnaryOperator(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Operand;
        }
    }
}
