using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class TextSegmentSyntax : SegmentSyntax
    {
        public string Text { get; }

        public TextSegmentSyntax(string text)
        {
            Text = text;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitTextSegment(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
