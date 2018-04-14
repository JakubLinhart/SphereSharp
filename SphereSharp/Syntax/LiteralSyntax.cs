using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Immutable;

namespace SphereSharp.Syntax
{
    public class LiteralSyntax : SyntaxNode
    {
        public ImmutableArray<SegmentSyntax> Segments { get; }

        public string Text
        {
            get
            {
                if (Segments.Length == 1 && Segments[0] is TextSegmentSyntax textSegment)
                    return textSegment.Text;

                throw new NotImplementedException();
            }
        }

        public LiteralSyntax(ImmutableArray<SegmentSyntax> segments)
        {
            Segments = segments;
        }

        public static LiteralSyntax Parse(string src)
            => LiteralParser.Literal.Parse(src);

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitLiteral(this);

        public override IEnumerable<SyntaxNode> GetChildNodes() => Segments;
    }
}
