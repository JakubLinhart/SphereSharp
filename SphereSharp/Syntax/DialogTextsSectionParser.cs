using Sprache;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SphereSharp.Syntax
{
    internal static class DialogTextsSectionParser
    {
        public static Parser<IEnumerable<char>> TextLine =>
            from _1 in CommonParsers.EmptyLine.Many()
            from line in Parse.Until(Parse.AnyChar, CommonParsers.Eol).Except(SectionParser.SectionHeader)
            from _2 in CommonParsers.EmptyLine.Many()
            select line;

        public static Parser<DialogTextsSectionSyntax> ParseTexts(string type, string name, string subName) =>
            from texts in TextLine.Text().Many()
            select new DialogTextsSectionSyntax(type, name, subName, texts.ToImmutableArray());
    }
}
