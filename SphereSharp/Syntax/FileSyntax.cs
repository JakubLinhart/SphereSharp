using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public class FileSyntax : SyntaxNode
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

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitFile(this);

        public override IEnumerable<SyntaxNode> GetChildNodes() => Sections;
    }
}
