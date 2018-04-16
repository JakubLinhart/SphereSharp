using System;
using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class BinaryOperatorSyntax : ExpressionSyntax
    {
        public BinaryOperatorKind Operator { get; }
        public ExpressionSyntax Operand1 { get; }
        public ExpressionSyntax Operand2 { get; }


        public BinaryOperatorSyntax(BinaryOperatorKind op, ExpressionSyntax operand1, ExpressionSyntax operand2)
        {
            Operator = op;
            Operand1 = operand1;
            Operand2 = operand2;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitBinaryOperator(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Operand1;
            yield return Operand2;
        }

        public string OperatorString
        {
            get
            {
                switch (Operator)
                {
                    case BinaryOperatorKind.Add:
                        return "+";
                    case BinaryOperatorKind.BinaryOr:
                        return "|";
                    case BinaryOperatorKind.Equal:
                        return "==";
                    case BinaryOperatorKind.LessThan:
                        return "<";
                    case BinaryOperatorKind.LessThanOrEqual:
                        return "<=";
                    case BinaryOperatorKind.LogicalAnd:
                        return "&&";
                    case BinaryOperatorKind.LogicalOr:
                        return "||";
                    case BinaryOperatorKind.MoreThan:
                        return ">";
                    case BinaryOperatorKind.MoreThanOrEqual:
                        return ">=";
                    case BinaryOperatorKind.Multiply:
                        return "*";
                    case BinaryOperatorKind.NotEqual:
                        return "!=";
                    case BinaryOperatorKind.Subtract:
                        return "-";
                    default:
                        throw new NotImplementedException($"Operator {Operator}");
                }
            }
        }
    }
}
