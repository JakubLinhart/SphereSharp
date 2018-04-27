using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class CustomFunctionArgumentListTests
    {
        [TestMethod]
        public void Can_parse_one_symbol_argument()
        {
            var syntax = ArgumentListSyntax._Parse("(D_RACE_class_races)");

            syntax.Arguments.Length.Should().Be(1);
            syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races");
        }

        [TestMethod]
        public void Can_parse_indexed_symbol_argument()
        {
            var syntax = ArgumentListSyntax._Parse("(D_RACE_class_races[arg(i)])");

            syntax.Arguments.Length.Should().Be(1);
            syntax.Arguments[0].Should().BeOfType<SymbolArgumentSyntax>().Which
                .Symbol.Should().BeOfType<IndexedSymbolSyntax>().Which
                .Index.Should().BeOfType<CallExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_comma_separated_list_of_symbols()
        {
            var syntax = ArgumentListSyntax._Parse("(D_RACE_class_races1,D_RACE_class_races2,D_RACE_class_races3)");

            syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races1");
            syntax.Arguments[1].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races2");
            syntax.Arguments[2].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races3");
        }

        [TestMethod]
        public void Can_parse_one_number_argument()
        {
            var syntax = ArgumentListSyntax._Parse("(123)");

            syntax._Arguments.Length.Should().Be(1);
            syntax._Arguments[0].Should().BeExpression("123");
        }

        [TestMethod]
        public void Can_parse_comma_separated_list_of_numbers()
        {
            var syntax = ArgumentListSyntax._Parse("(123,456,789)");

            syntax.Argument(0).Should().BeExpression("123");
            syntax.Argument(1).Should().BeExpression("456");
            syntax.Argument(2).Should().BeExpression("789");
        }

        [TestMethod]
        public void Whitespace_can_be_after_comma()
        {
            var syntax = ArgumentListSyntax._Parse("(123, 456,    789)");

            syntax.Argument(0).Should().BeExpression("123");
            syntax.Argument(1).Should().BeExpression("456");
            syntax.Argument(2).Should().BeExpression("789");
        }

        [TestMethod]
        public void Can_parse_comma_separated_list_of_subexpressions()
        {
            var syntax = ArgumentListSyntax._Parse("((123),(456+321),(789+987+222))");

            syntax.Argument(0).Should().BeExpression("(123)");
            syntax.Argument(1).Should().BeExpression("(456+321)");
            syntax.Argument(2).Should().BeExpression("(789+987+222)");
        }
    }
}
