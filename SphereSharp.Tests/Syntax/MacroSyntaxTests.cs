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
    public class MacroSyntaxTests
    {
        [TestMethod]
        public void Can_parse_macro_with_member_access()
        {
            var syntax = MacroSyntax.Parse("<SRC.NAME>");

            syntax.Call.MemberName.Should().Be("SRC");
            syntax.Call.ChainedCall.MemberName.Should().Be("NAME");
        }

        [TestMethod]
        public void Can_parse_literal_macro_with_member_access()
        {
            var syntax = MacroSyntax.Parse("<?SRC.NAME?>");

            syntax.Call.MemberName.Should().Be("SRC");
            syntax.Call.ChainedCall.MemberName.Should().Be("NAME");
        }

        [TestMethod]
        public void Can_parse_macro_with_call()
        {
            var syntax = MacroSyntax.Parse("<argv(0)>");

            syntax.Call.MemberName.Should().Be("argv");
            syntax.Call.Arguments.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_macro_with_array_access_and_numer_as_index()
        {
            var syntax = MacroSyntax.Parse("<choosestats[0]>");

            var indexedSymbol = syntax.Call.MemberNameSyntax.Should().BeOfType<IndexedSymbolSyntax>().Which;
            indexedSymbol.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("choosestats");
            indexedSymbol.Index.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_macro_with_array_access_and_call_as_index()
        {
            var syntax = MacroSyntax.Parse("<def_class[argn]>")
                .Call.MemberNameSyntax.Should().BeOfType<IndexedSymbolSyntax>().Which;

            syntax.Segments[0].As<TextSegmentSyntax>().Text.Should().Be("def_class");
            syntax.Index.Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("argn");
        }

        [TestMethod]
        public void Can_parse_nested_macros()
        {
            var syntax = MacroSyntax.Parse("<strlen(<tag(nation)>)>");

            syntax.Call.MemberName.Should().Be("strlen");
            syntax.Call.Arguments.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>();
        }
    }
}
