using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public enum ConstantExpressionSyntaxKind
    {
        Decadic,
        Hex
    }

    public abstract class ConstantExpressionSyntax : ExpressionSyntax
    {
        public string Value { get; protected set; }
    }

    public class IntegerConstantExpressionSyntax : ConstantExpressionSyntax
    {
        public ConstantExpressionSyntaxKind Kind { get; }

        public IntegerConstantExpressionSyntax(string numberString, ConstantExpressionSyntaxKind kind)
        {
            Value = numberString;
            Kind = kind;
        }

        public override string ToString() => Value;

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitIntegerConstantExpression(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }

    public class MacroIntegerConstantExpressionSyntax : ExpressionSyntax
    {
        public string FirstDigits { get; }
        public MacroExpressionSyntax Macro { get; }

        public MacroIntegerConstantExpressionSyntax(string firstDigits, MacroExpressionSyntax macro)
        {
            FirstDigits = firstDigits;
            Macro = macro;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitMacroIntegerConstantExpression(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
        }
    }

    public class DecimalConstantExpressionSyntax : ConstantExpressionSyntax
    {
        public ConstantExpressionSyntaxKind Kind { get; }

        public DecimalConstantExpressionSyntax(string numberString, ConstantExpressionSyntaxKind kind)
        {
            Value = numberString;
            Kind = kind;
        }

        public override string ToString() => Value;

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitDecimalConstantExpression(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }

    public class IntervalExpressionSyntax : ExpressionSyntax
    {
        public ConstantExpressionSyntax MinValue { get; }
        public ConstantExpressionSyntax MaxValue { get; }

        public IntervalExpressionSyntax(ConstantExpressionSyntax minValue, ConstantExpressionSyntax maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitIntervalExpression(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return MinValue;
            yield return MaxValue;
        }
    }
}
