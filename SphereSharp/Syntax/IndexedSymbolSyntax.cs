using System.Collections.Immutable;

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
    }
}
