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
    }
}
