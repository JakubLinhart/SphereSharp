using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class SymbolSyntaxTests
    {
        [TestMethod]
        public void Can_parse_text_symbol()
        {
            var syntax = SymbolSyntax.Parse("D_RACEclass_background");

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("D_RACEclass_background");
        }

        [TestMethod]
        public void Can_parse_macro_inside_symbol()
        {
            var syntax = SymbolSyntax.Parse("basestats_<tag.class>_str_min");

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("basestats_");
            syntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberName.Should().Be("tag");
            syntax.Segments[2].As<TextSegmentSyntax>().Text.Should().Be("_str_min");
            syntax.Segments.Length.Should().Be(3);
        }

        [TestMethod]
        public void Can_parse_macro_at_the_end_of_symbol()
        {
            var syntax = SymbolSyntax.Parse("basestats_<tag.class>");

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("basestats_");
            syntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberName.Should().Be("tag");
            syntax.Segments.Length.Should().Be(2);
        }

        [TestMethod]
        public void Can_parse_multiple_macros_inside_symbol()
        {
            var syntax = SymbolSyntax.Parse("basestats_<tag.class>_<tag.race>");

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("basestats_");
            syntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberName.Should().Be("tag");
            syntax.Segments[2].As<TextSegmentSyntax>().Text.Should().Be("_");
            syntax.Segments[3].As<MacroSegmentSyntax>().Macro.Call.MemberName.Should().Be("tag");
            syntax.Segments.Length.Should().Be(4);
        }

        [TestMethod]
        public void Can_parse_nested_macros_inside_symbol()
        {
            var syntax = SymbolSyntax.Parse("test_<realm_<tag(nation)>>");

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("test_");
            syntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberNameSyntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("realm_");
            syntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberNameSyntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberName.Should().Be("tag");
        }

        [TestMethod]
        public void Can_parse_indexed_symbol_with_integer_index()
        {
            var syntax = SymbolSyntax.Parse("test[1]").Should().BeOfType<IndexedSymbolSyntax>().Which;

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("test");
            syntax.Index.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_indexed_symbol_with_call_index()
        {
            var syntax = SymbolSyntax.Parse("test[argn]").Should().BeOfType<IndexedSymbolSyntax>().Which;

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("test");
            syntax.Index.Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("argn");
        }
    }
}
