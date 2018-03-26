using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public class FileSyntax
    {
        public ImmutableArray<SectionSyntax> Sections { get; }
        public string FileName { get; }

        public FileSyntax(string fileName, ImmutableArray<SectionSyntax> sections)
        {
            Sections = sections;
            FileName = fileName;
        }

        public static FileSyntax Parse(string fileName, string src) 
            => FileParser.File(fileName).Parse(src);
    }

    internal static class FileParser
    {
        public static Parser<SectionSyntax> Section =>
            from _1 in CommonParsers.Ignored.Optional()
            from section in SectionParser.Section
            from _2 in CommonParsers.Ignored.Optional()
            select section;

        public static Parser<FileSyntax> File(string fileName) =>
            from sections in Section.Many()
            select new FileSyntax(fileName, sections.ToImmutableArray());
    }
}
