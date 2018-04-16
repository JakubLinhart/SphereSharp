using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class LiteralSyntaxTests
    {
        [TestMethod]
        public void Can_parse_literal_with_double_qoutes()
        {
            var syntax = LiteralSyntax.Parse("\"this is some text\"");

            syntax.Text.Should().Be("this is some text");
        }

        [TestMethod]
        public void Can_parse_safe_literal_without_double_qoutes()
        {
            var syntax = LiteralSyntax.Parse("<?safe?>Gondoran");

            syntax.Text.Should().Be("Gondoran");
        }

        [TestMethod]
        public void Can_parse_safe_literal_without_double_qoutes_terminated_by_comma()
        {
            var syntax = LiteralSyntax.Parse("<?safe?>Gondoran, Gondoran");

            syntax.Text.Should().Be("Gondoran");
        }

        [TestMethod]
        public void Can_parse_safe_literal_without_double_qoutes_terminated_by_parenthesis()
        {
            var syntax = LiteralSyntax.Parse("<?safe?>Gondoran), Gondoran");

            syntax.Text.Should().Be("Gondoran");
        }

        [TestMethod]
        public void Can_parse_safe_literal_containing_html_tags()
        {
            var syntax = LiteralSyntax.Parse("<?safe?><BASEFONT SIZE=\"+5\" COLOR=\"#000080\">some text</BASEFONT>");

            syntax.Text.Should().Be("<BASEFONT SIZE=\"+5\" COLOR=\"#000080\">some text</BASEFONT>");
        }

        [TestMethod]
        public void Can_parse_safe_literal_containing_macro()
        {
            var syntax = LiteralSyntax.Parse("<?safe?><BASEFONT SIZE=\"+5\" COLOR=\"#000080\"><?seznamclass?></BASEFONT>");

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("<BASEFONT SIZE=\"+5\" COLOR=\"#000080\">");
            syntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberName.Should().Be("seznamclass");
            syntax.Segments[2].As<TextSegmentSyntax>().Text.Should().Be("</BASEFONT>");
            syntax.Segments.Should().HaveCount(3);
        }

        [TestMethod]
        public void Can_parse_literal_with_double_quotes_containing_macro()
        {
            var syntax = LiteralSyntax.Parse("\"segment1<tag(basevit)>segment3\"");

            syntax.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("segment1");
            syntax.Segments[1].Should().BeOfType<MacroSegmentSyntax>();
            syntax.Segments[2].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("segment3");
        }

        [TestMethod]
        public void Can_parse_literal_with_double_quotes_containing_eval_macro()
        {
            var syntax = LiteralSyntax.Parse("\"segment1<eval tag(basevit)>segment3\"");

            syntax.Segments.Should().HaveCount(3);
            syntax.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("segment1");
            syntax.Segments[1].Should().BeOfType<EvalMacroSegmentSyntax>();
            syntax.Segments[2].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("segment3");
        }

        [TestMethod]
        public void Can_parse_safe_literal_with_doublequotes()
        {
            var syntax = LiteralSyntax.Parse("\"<?safe?><BASEFONT SIZE=\"+5\" COLOR=\"#000080\"><?seznamclass?></BASEFONT>\"");

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("<BASEFONT SIZE=\"+5\" COLOR=\"#000080\">");
            syntax.Segments[1].As<MacroSegmentSyntax>().Macro.Call.MemberName.Should().Be("seznamclass");
            syntax.Segments[2].As<TextSegmentSyntax>().Text.Should().Be("</BASEFONT>");
            syntax.Segments.Should().HaveCount(3);
        }

        [TestMethod]
        public void Can_parse_literal_without_doublequotes_with_macro()
        {
            var syntax = LiteralSyntax.Parse("i_crystal<tag.class>");

            syntax.Segments.Should().HaveCount(2);
            syntax.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("i_crystal");
            syntax.Segments[1].Should().BeOfType<MacroSegmentSyntax>();
        }
    }
}
