using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public class SymbolSyntax : SyntaxNode
    {
        public ImmutableArray<SegmentSyntax> Segments { get; }

        public SymbolSyntax(ImmutableArray<SegmentSyntax> segments)
        {
            Segments = segments;
        }

        public SymbolSyntax(string name)
        {
            Segments = new SegmentSyntax[] { new TextSegmentSyntax(name) }.ToImmutableArray();
        }

        public static SymbolSyntax Parse(string src)
            => SymbolParser.Symbol.Parse(src);

        public override string ToString()
        {
            if (Segments.Length == 1 && Segments[0] is TextSegmentSyntax symbolSegment)
                return symbolSegment.Text;

            throw new NotImplementedException();
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitSymbol(this);
        public override IEnumerable<SyntaxNode> GetChildNodes() => Segments;
    }
}
