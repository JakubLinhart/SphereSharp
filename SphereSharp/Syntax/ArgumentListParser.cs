using Sprache;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class ArgumentListParser
    {
        public static Parser<ArgumentSyntax> Argument =>
            ResourceArgument.Or(ExpressionArgument).Or(LiteralArgument);

        public static Parser<ArgumentSyntax> ExpressionArgument =>
            from expr in ArgumentExpressionParser.Expr
            select new ExpressionArgumentSyntax(expr);

        public static Parser<ArgumentSyntax> LiteralArgument =>
            from argument in LiteralParser.Literal
            select new LiteralArgumentSyntax(argument);

        public static Parser<ArgumentSyntax> ResourceArgument =>
            from amount in ExpressionParser.Expr
            from _1 in CommonParsers.OneLineWhiteSpace
            from name in SymbolParser.NonIndexedSymbol
            select new ResourceArgumentSyntax(amount, name);


        public static Parser<IEnumerable<ArgumentSyntax>> StringArgument =>
            from firstLetter in Parse.Letter.Once()
            from nextLetters in Parse.AnyChar.Except(Parse.LineEnd).Many()
            select new[] { new TextArgumentSyntax(new string(firstLetter.Concat(nextLetters).ToArray())) };

        public static Parser<IEnumerable<ArgumentSyntax>> InnerArgumentList =>
            from firstArg in Argument.Once()
            from nextArgs in (
                from _1 in Parse.Char(',')
                from _2 in CommonParsers.OneLineWhiteSpace.Many()
                from arg in Argument
                select arg
            ).Many()
            select firstArg.Concat(nextArgs);

        public static Parser<ArgumentListSyntax> ArgumentList =>
            from args in StringArgument
                .Or(ArgumentListWithParenthesis)
                .Or(ArgumentListWithoutParenthesis)
            select new ArgumentListSyntax(args.ToImmutableArray());

        public static Parser<IEnumerable<ArgumentSyntax>> ArgumentListWithoutParenthesis =>
            from firstNumber in CommonParsers.IntegerDecadicNumber.Once()
            from nextNumbers in (
                from _ in CommonParsers.OneLineWhiteSpace.AtLeastOnce()
                from arg in CommonParsers.IntegerDecadicNumber
                select arg
            ).Many()
            select firstNumber.Concat(nextNumbers).Select(x => new TextArgumentSyntax(x));

        public static Parser<IEnumerable<ArgumentSyntax>> ArgumentListWithParenthesis =>
            from leftParen in Parse.Char('(')
            from arguments in InnerArgumentList
            from rightParen in Parse.Char(')')
            select arguments;
    }
}
