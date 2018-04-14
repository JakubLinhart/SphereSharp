using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    public sealed class IndexedSymbolSyntax : SymbolSyntax
    {
        public ExpressionSyntax Index { get; }

        public IndexedSymbolSyntax(ImmutableArray<SegmentSyntax> segments, ExpressionSyntax index)
            : base(segments)
        {
            Index = index;
        }

        public override string ToString() =>
            $"{base.ToString()}[{Index.ToString()}]";

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitIndexedSymbol(this);
        public override IEnumerable<SyntaxNode> GetChildNodes() => base.GetChildNodes().Concat(Index.GetChildNodes());
    }
}
