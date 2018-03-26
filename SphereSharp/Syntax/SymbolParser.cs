using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class SymbolParser
    {
        public static Parser<string> TextSegmentCore =>
            from nextChars in Parse.LetterOrDigit.Or(Parse.Chars('_')).AtLeastOnce()
            select new string(nextChars.ToArray());

        public static Parser<TextSegmentSyntax> TextSegment =>
            from symbol in TextSegmentCore.Except(CommonParsers.Keyword)
            select new TextSegmentSyntax(symbol);

        public static Parser<SegmentSyntax> MacroSegment =>
            from macro in MacroParser.Macro
            select new MacroSegmentSyntax(macro);

        public static Parser<SegmentSyntax> SymbolSegment =>
            TextSegment.Or(MacroSegment);

        public static Parser<SymbolSyntax> NonIndexedSymbol =>
            from symbolSegments in SymbolSegment.AtLeastOnce()
            select new SymbolSyntax(symbolSegments.ToImmutableArray());

        public static Parser<SymbolSyntax> IndexedSymbol =>
            from symbolSegments in SymbolSegment.AtLeastOnce()
            from _1 in Parse.Char('[')
            from index in ExpressionParser.Expr
            from _2 in Parse.Char(']')
            select new IndexedSymbolSyntax(symbolSegments.ToImmutableArray(), index);

        public static Parser<SymbolSyntax> Symbol
            => IndexedSymbol.Or(NonIndexedSymbol);
    }
}
