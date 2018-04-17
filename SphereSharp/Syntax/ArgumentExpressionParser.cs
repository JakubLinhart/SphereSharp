using Sprache;
using System.Linq;

namespace SphereSharp.Syntax
{
    public static class ArgumentExpressionParser
    {
        public static Parser<BinaryOperatorKind> BinaryOperator(string op, BinaryOperatorKind kind) =>
            from _1 in CommonParsers.OneLineWhiteSpace.Optional()
            from _2 in Parse.String(op)
            from _3 in CommonParsers.OneLineWhiteSpace.Optional()
            select kind;

        public static Parser<UnaryOperatorKind> UnaryOperator(string op, UnaryOperatorKind kind)
            => Parse.String(op).Return(kind);

        public static Parser<UnaryOperatorKind> LogicalNot => UnaryOperator("!", UnaryOperatorKind.LogicalNot);
        public static Parser<BinaryOperatorKind> Add => BinaryOperator("+", BinaryOperatorKind.Add);
        public static Parser<BinaryOperatorKind> Subtract => BinaryOperator("-", BinaryOperatorKind.Subtract);
        public static Parser<BinaryOperatorKind> Multiply => BinaryOperator("*", BinaryOperatorKind.Multiply);
        public static Parser<BinaryOperatorKind> Divide => BinaryOperator("/", BinaryOperatorKind.Divide);
        public static Parser<BinaryOperatorKind> LogicalAnd => BinaryOperator("&&", BinaryOperatorKind.LogicalAnd);
        public static Parser<BinaryOperatorKind> LogicalOr => BinaryOperator("||", BinaryOperatorKind.LogicalOr);
        public static Parser<BinaryOperatorKind> BinaryOr => BinaryOperator("|", BinaryOperatorKind.BinaryOr);
        public static Parser<BinaryOperatorKind> Equal => BinaryOperator("==", BinaryOperatorKind.Equal);
        public static Parser<BinaryOperatorKind> NotEqual => BinaryOperator("!=", BinaryOperatorKind.NotEqual);
        public static Parser<BinaryOperatorKind> MoreThan => BinaryOperator(">", BinaryOperatorKind.MoreThan);
        public static Parser<BinaryOperatorKind> LessThan => BinaryOperator("<", BinaryOperatorKind.LessThan);

        public static Parser<ExpressionSyntax> ExpressionInParentheses =>
            from _1 in Parse.Char('(')
            from expr in Expr.Once()
            from _2 in Parse.Char(')')
            select expr.Single().Enclose();

        public static Parser<ExpressionSyntax> Factor =>
            ExpressionInParentheses.Or(MacroIntegerConstant).Or(Constant).Or(Interval).Or(EvalMacroExpression);

        public static Parser<ExpressionSyntax> LogicalNotExpression =>
            from op in LogicalNot
            from factor in Factor
            select new UnaryOperatorSyntax(op, factor);

        public static Parser<ExpressionSyntax> Operand =>
            LogicalNotExpression.Or(Factor);

        public static Parser<ExpressionSyntax> Term => Parse.ChainOperator(Multiply.Or(Divide), Operand, CreateBinaryExpression);
        public static Parser<ExpressionSyntax> EqualityTerm => Parse.ChainOperator(Add.Or(Subtract), Term, CreateBinaryExpression);
        public static Parser<ExpressionSyntax> LogicalTerm => Parse.ChainOperator(Equal.Or(NotEqual).Or(MoreThan).Or(LessThan), EqualityTerm, CreateBinaryExpression);
        public static Parser<ExpressionSyntax> Expr => Parse.ChainOperator(LogicalAnd.Or(LogicalOr).Or(BinaryOr), LogicalTerm, CreateBinaryExpression);

        private static ExpressionSyntax CreateBinaryExpression(BinaryOperatorKind kind, ExpressionSyntax arg1, ExpressionSyntax arg2)
            => new BinaryOperatorSyntax(kind, arg1, arg2);

        public static Parser<ExpressionSyntax> IntervalBoundary =>
            ExpressionInParentheses.Or(DecimalDecadicConstant).Or(IntegerDecadicConstant).Or(IntegerHexConstant);

        public static Parser<ExpressionSyntax> Interval =>
            from _1 in Parse.String("{").Once()
            from min in IntervalBoundary.Once()
            from _2 in CommonParsers.OneLineWhiteSpace
            from max in IntervalBoundary.Once()
            from _3 in Parse.String("}").Once()
            select new IntervalExpressionSyntax(min.Single(), max.Single());

        public static Parser<ExpressionSyntax> MacroIntegerConstant =>
            from firstDigits in Parse.Digit.AtLeastOnce().Text()
            from macro in MacroExpression
            select new MacroIntegerConstantExpressionSyntax(firstDigits, macro);

        public static Parser<ExpressionSyntax> IntegerDecadicConstant =>
            from number in CommonParsers.IntegerDecadicNumber
            select new IntegerConstantExpressionSyntax(number, ConstantExpressionSyntaxKind.Decadic);

        public static Parser<ExpressionSyntax> DecimalDecadicConstant =>
            from integralPart in CommonParsers.IntegerDecadicNumber
            from dot in Parse.String(".")
            from decimalPart in CommonParsers.IntegerDecadicNumber
            select new DecimalConstantExpressionSyntax(integralPart + "." + decimalPart, ConstantExpressionSyntaxKind.Decadic);

        public static Parser<ExpressionSyntax> IntegerHexConstant =>
            from number in CommonParsers.IntegerHexNumber
            select new IntegerConstantExpressionSyntax(number, ConstantExpressionSyntaxKind.Hex);

        public static Parser<ExpressionSyntax> Constant =>
            DecimalDecadicConstant.Or(IntegerHexConstant).Or(IntegerDecadicConstant);

        public static Parser<MacroExpressionSyntax> MacroExpression =>
            from macro in MacroParser.Macro
            select new MacroExpressionSyntax(macro);

        public static Parser<EvalMacroExpressionSyntax> EvalMacroExpression =>
            from macro in EvalMacroParser.Macro
            select new EvalMacroExpressionSyntax(macro);

        public static Parser<CallExpressionSyntax> CallExpression =>
            from call in CallParser.Call
            select new CallExpressionSyntax(call);
    }
}
