using Sprache;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class DefNamesSectionParser
    {
        public static Parser<IEnumerable<char>> LValueIndex =>
            from lParen in Parse.String("[")
            from index in CommonParsers.IntegerDecadicNumber
            from rParen in Parse.String("]")
            select lParen.Concat(index).Concat(rParen);

        public static Parser<IEnumerable<char>> WhiteSpaceSeparator =>
            CommonParsers.OneLineWhiteSpace.AtLeastOnce();

        public static Parser<IEnumerable<char>> AssignmentSeparator =>
            from _1 in CommonParsers.OneLineWhiteSpace.Many()
            from assignment in Parse.String("=")
            from _2 in CommonParsers.OneLineWhiteSpace.Many()
            select _1.Concat(assignment).Concat(_2);

        public static Parser<IEnumerable<char>> Separator =>
            WhiteSpaceSeparator.Or(AssignmentSeparator);

        public static Parser<string> LValue =>
            from name in Parse.LetterOrDigit.Or(Parse.Char('_')).AtLeastOnce().Text()
            from index in LValueIndex.Text().Optional()
            from _1 in Separator.Once()
            select index.IsDefined ? name + index.Get() : name;

        public static Parser<string> RValue =>
            from text in Parse.AnyChar.Except(CommonParsers.Eol).AtLeastOnce().Text()
            select text.TrimEnd();

        public static Parser<DefNameSyntax> DefName =>
            from _1 in CommonParsers.Ignored.Many()
            from lValue in LValue.Once()
            from rValue in RValue.Once()
            from _2 in CommonParsers.Eol.AtLeastOnce()
            select new DefNameSyntax(lValue.First(), rValue.First());

        public static Parser<SectionSyntax> ParseDefNames(string sectionType, string sectionName) =>
            from defNames in DefName.AtLeastOnce()
            select new DefNamesSectionSyntax(sectionType, sectionName, defNames.ToImmutableArray());
    }
}
