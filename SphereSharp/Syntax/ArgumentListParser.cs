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

        public static Parser<string> ResourceName =>
            from firstLetter in Parse.Letter
            from nextLetters in Parse.LetterOrDigit.Or(Parse.Char('_')).Many().Text()
            select firstLetter + nextLetters;

        public static Parser<ArgumentSyntax> ResourceArgument =>
            from amount in ExpressionParser.Expr
            from _1 in CommonParsers.OneLineWhiteSpace
            from name in ResourceName
            select new ResourceArgumentSyntax(amount, name);


        public static Parser<IEnumerable<ArgumentSyntax>> StringArgument =>
            from firstLetter in Parse.Letter.Once()
            from nextLetters in Parse.AnyChar.Except(Parse.LineTerminator.Or(CommonParsers.Comment)).Many()
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

        public static Parser<ArgumentListSyntax> ArgumentList => ArgumentListWithParenthesis.Or(ArgumentListWithoutParenthesis);

        public static Parser<ArgumentSyntax> ArgumentWithoutParenthesis =>
            from argument in ResourceArgument.Or(ExpressionArgument)
            select argument;

        public static Parser<IEnumerable<ArgumentSyntax>> ArgumentsWithoutParenthesis =>
            from firstArgument in ArgumentWithoutParenthesis
            from nextArguments in (
                from _ in CommonParsers.OneLineWhiteSpace.AtLeastOnce()
                from arg in ArgumentWithoutParenthesis
                select arg
            ).Many()
            select new ArgumentSyntax[] { firstArgument }.Concat(nextArguments);

        public static Parser<ArgumentListSyntax> ArgumentListWithoutParenthesis =>
            from args in StringArgument
                .Or(ArgumentsWithoutParenthesis)
            select new ArgumentListSyntax(args.ToImmutableArray());

        public static Parser<ArgumentListSyntax> ArgumentListWithParenthesis =>
            from leftParen in Parse.Char('(')
            from arguments in InnerArgumentList
            from rightParen in Parse.Char(')')
            select new ArgumentListSyntax(arguments.ToImmutableArray());
    }
}
