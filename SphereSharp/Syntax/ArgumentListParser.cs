using Sprache;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class ArgumentListParser
    {
        public static Parser<ArgumentSyntax> Argument =>
            EvalMacroArgument.Or(MacroArgument).Or(LiteralArgument).Or(TextArgument);

        public static Parser<ArgumentSyntax> LiteralArgument =>
            from argument in LiteralParser.Literal
            select new LiteralArgumentSyntax(argument);

        public static Parser<ArgumentSyntax> MacroArgument =>
            from argument in MacroParser.Macro
            select new MacroArgumentSyntax(argument);

        public static Parser<ArgumentSyntax> EvalMacroArgument =>
            from argument in EvalMacroParser.Macro
            select new EvalMacroArgumentSyntax(argument);

        public static Parser<string> IndexedSymbolArgument =>
            from symbol in SymbolParser.IndexedSymbol
            select symbol.ToString();

        public static Parser<ArgumentSyntax> TextArgument =>
            from argument in IndexedSymbolArgument
                .Or(CommonParsers.Symbol)
                .Or(CommonParsers.IntegerDecadicNumber)
            select new TextArgumentSyntax(argument);

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
