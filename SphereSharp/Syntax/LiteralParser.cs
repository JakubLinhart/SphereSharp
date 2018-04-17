using Sprache;
using System.Linq;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    internal static class LiteralParser
    {
        public static Parser<TextSegmentSyntax> SafeTextSegment =>
            from chars in Parse.AnyChar.Except(
                CommonParsers.Eol
                .Or(CommonParsers.LeftLiteralMacroParenthesis)
                .Or(Parse.String(","))
                .Or(Parse.String(")"))
                .Or(SafeLiteralTerminator))
                .Many()
            select new TextSegmentSyntax(new string(chars.ToArray()));

        public static Parser<SegmentSyntax> SafeMacroSegment =>
            from macro in MacroParser.LiteralMacro
            select new MacroSegmentSyntax(macro);

        public static Parser<SegmentSyntax> SafeSegment =>
            SafeTextSegment.Or(SafeMacroSegment);

        public static Parser<TextSegmentSyntax> TextSegment =>
            from chars in Parse.AnyChar.Except(
                CommonParsers.LeftMacroParenthesis
                .Or(Parse.String("\"")))
                .Many()
            select new TextSegmentSyntax(new string(chars.ToArray()));

        public static Parser<TextSegmentSyntax> TextSegmentWithoutDoubleQuotes =>
            from chars in Parse.AnyChar.Except(Parse.Chars(new[] { '<', '"', ',', ')' }))
                .AtLeastOnce()
                .Except(Parse.LineEnd.Or(CommonParsers.Comment))
            select new TextSegmentSyntax(new string(chars.ToArray()));

        public static Parser<SegmentSyntax> MacroSegment =>
            from macro in MacroParser.Macro
            select new MacroSegmentSyntax(macro);

        public static Parser<SegmentSyntax> EvalMacroSegment =>
            from macro in EvalMacroParser.Macro
            select new EvalMacroSegmentSyntax(macro);

        public static Parser<SegmentSyntax> Segment =>
            TextSegment.Or(EvalMacroSegment).Or(MacroSegment);

        public static Parser<SegmentSyntax> SegmentWithoutDoubleQuotes =>
            TextSegmentWithoutDoubleQuotes.Or(MacroSegment);

        public static Parser<LiteralSyntax> SafeLiteralWithDoubleQuotes =>
            from _1 in Parse.Char('"').Once()
            from literal in SafeLiteralWithoutDoubleQuotes
            from _2 in Parse.Char('"').Once()
            select literal;

        public static Parser<IEnumerable<char>> SafeLiteralTerminator =>
            SafeLiteralEolTerminator.Or(SafeLiteralArgumentListTerminator).Or(SafeLiteralArgumentTerminator);

        public static Parser<IEnumerable<char>> SafeLiteralEolTerminator =>
            from _1 in Parse.Char('"').Once()
            from _2 in Parse.LineTerminator.Once()
            select _1.Concat(_2.Single());

        public static Parser<IEnumerable<char>> SafeLiteralArgumentListTerminator =>
            from _1 in Parse.Char('"').Once()
            from _2 in CommonParsers.OneLineWhiteSpace.Many()
            from _3 in Parse.Char(')').Once()
            select _1.Concat(_2).Concat(_3);

        public static Parser<IEnumerable<char>> SafeLiteralArgumentTerminator =>
              from _1 in Parse.Char('"').Once()
              from _2 in CommonParsers.OneLineWhiteSpace.Many()
              from _3 in Parse.Char(',').Once()
              select _1.Concat(_2).Concat(_3);

        public static Parser<LiteralSyntax> SafeLiteralWithoutDoubleQuotes =>
            from _1 in Parse.String("<?safe?>")
            from segments in SafeSegment.AtLeastOnce()
            select new LiteralSyntax(segments.ToImmutableArray());

        public static Parser<LiteralSyntax> LiteralWithDoubleQuotes =>
            from _1 in Parse.String("\"")
            from segments in Segment.AtLeastOnce()
            from _2 in Parse.String("\"")
            select new LiteralSyntax(segments.ToImmutableArray());

        public static Parser<LiteralSyntax> LiteralWithoutDoubleQuotes =>
            from segments in SegmentWithoutDoubleQuotes.AtLeastOnce()
            select new LiteralSyntax(segments.ToImmutableArray());

        public static Parser<LiteralSyntax> Literal =>
            from literal in SafeLiteralWithDoubleQuotes.Or(SafeLiteralWithoutDoubleQuotes).Or(LiteralWithDoubleQuotes).Or(LiteralWithoutDoubleQuotes)
            select literal;
    }
}
