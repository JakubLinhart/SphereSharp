using Sprache;
using System.Collections.Immutable;

namespace SphereSharp.Syntax
{
    internal static class FileParser
    {
        public static Parser<SectionSyntax> Section =>
            from _1 in CommonParsers.Ignored.Many()
            from section in SectionParser.Section
            from _2 in CommonParsers.Ignored.Many()
            select section;

        public static Parser<FileSyntax> File(string fileName) =>
            from sections in Section.Many()
            from _1 in Parse.LineTerminator
            select new FileSyntax(fileName, sections.ToImmutableArray());
    }
}
