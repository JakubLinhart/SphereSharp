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
    public class ArgumentListSyntaxParsingTests
    {
        [TestMethod]
        public void Can_parse_symbol_without_parentheses()
        {
            var syntax = ArgumentListSyntax.Parse("D_RACEclass_races");

            syntax.Arguments.Length.Should().Be(1);
            syntax.Arguments[0].As<TextArgumentSyntax>().Text.Should().Be("D_RACEclass_races");
        }

        [TestMethod]
        public void Can_parse_indexed_symbol()
        {
            var syntax = ArgumentListSyntax.Parse("(seznamnations[0])");

            syntax.Arguments.Length.Should().Be(1);
            syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("seznamnations[0]");
        }

        [TestMethod]
        public void Can_parse_one_symbol_argument()
        {
            var syntax = ArgumentListSyntax.Parse("(D_RACE_class_races)");

            syntax.Arguments.Length.Should().Be(1);
            syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races");
        }

        [TestMethod]
        public void Can_parse_comma_separated_list_of_symbols()
        {
            var syntax = ArgumentListSyntax.Parse("(D_RACE_class_races1,D_RACE_class_races2,D_RACE_class_races3)");

            syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races1");
            syntax.Arguments[1].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races2");
            syntax.Arguments[2].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("D_RACE_class_races3");
        }

        [TestMethod]
        public void Can_parse_one_number_argument()
        {
            var syntax = ArgumentListSyntax.Parse("(123)");

            syntax.Arguments.Length.Should().Be(1);
            syntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_comma_separated_list_of_numbers()
        {
            var syntax = ArgumentListSyntax.Parse("(123,456,789)");

            syntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>();
            syntax.Arguments[1].Should().BeOfType<ExpressionArgumentSyntax>();
            syntax.Arguments[2].Should().BeOfType<ExpressionArgumentSyntax>();
        }

        [TestMethod]
        public void Whitespace_can_be_after_comma()
        {
            var syntax = ArgumentListSyntax.Parse("(123, 456,    789)");

            syntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>();
            syntax.Arguments[1].Should().BeOfType<ExpressionArgumentSyntax>();
            syntax.Arguments[2].Should().BeOfType<ExpressionArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_space_separated_list_of_numbers()
        {
            var syntax = ArgumentListSyntax.Parse("123 456 789");

            syntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression.Should()
                .BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("123");
            syntax.Arguments[1].Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression.Should()
                .BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("456");
            syntax.Arguments[2].Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression.Should()
                .BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("789");
        }

        [TestMethod]
        public void Can_parse_text_as_one_argument()
        {
            var syntax = ArgumentListSyntax.Parse("this is one text, that is parsed as single argument");

            syntax.Arguments[0].As<TextArgumentSyntax>().Text.Should().Be("this is one text, that is parsed as single argument");
        }

        [TestMethod]
        public void Can_parse_one_macro_argument()
        {
            var syntax = ArgumentListSyntax.Parse("(<tag(race)>)");

            var argumentExpressionSyntax = syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_literal_without_doublequotes_containing_macro()
        {
            var syntax = ArgumentListSyntax.Parse("(i_crystal_<tag.class>)");

            syntax.Arguments.Should().HaveCount(1);
            var literalSyntax = syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal;

            literalSyntax.Segments.Should().HaveCount(2);
            literalSyntax.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("i_crystal_");
            literalSyntax.Segments[1].Should().BeOfType<MacroSegmentSyntax>();
        }

        [TestMethod]
        public void Ignores_comment_at_the_end_of_literal_without_doublequotes()
        {
            var syntax = ArgumentListSyntax.Parse("i_crystal//comment");

            syntax.Arguments.Should().HaveCount(1);
            syntax.Arguments[0].Should().BeOfType<TextArgumentSyntax>().Which.Text.Should().Be("i_crystal");
        }

        [TestMethod]
        public void Can_parse_literal_without_doublequotes_containing_macro_at_beginning()
        {
            var syntax = ArgumentListSyntax.Parse("(<tag.class>i_crystal_)");

            syntax.Arguments.Should().HaveCount(1);
            var literalSyntax = syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal;

            literalSyntax.Segments.Should().HaveCount(2);
            literalSyntax.Segments[0].Should().BeOfType<MacroSegmentSyntax>();
            literalSyntax.Segments[1].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("i_crystal_");
        }

        [TestMethod]
        public void Can_parse_two_macro_arguments()
        {
            var syntax = ArgumentListSyntax.Parse("(<tag.race>,<argv(u)>)");

            syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>();
            syntax.Arguments[1].Should().BeOfType<LiteralArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_safe_literal_with_macro()
        {
            var syntax = ArgumentListSyntax.Parse("(123,<?safe?><BASEFONT SIZE=\"+5\" COLOR=\"#000080\"><?seznamclass?></BASEFONT>,456)");

            syntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>();
            syntax.Arguments[1].As<LiteralArgumentSyntax>().Literal.Segments.Length.Should().Be(3);
            syntax.Arguments[2].Should().BeOfType<ExpressionArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_call_with_doublequoted_safe_literal()
        {
            var syntax = ArgumentListSyntax.Parse("(\"<?safe?>something safe\")");

            syntax.Arguments.Should().HaveCount(1);
            var argumentLiteral = syntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal;
            argumentLiteral.Segments.Should().HaveCount(1);
            argumentLiteral.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("something safe");
        }

        [TestMethod]
        public void Can_parse_call_with_doublequoted_safe_literal_between_other_arguments()
        {
            var syntax = ArgumentListSyntax.Parse("(1,\"<?safe?>something safe\",2)");

            syntax.Arguments.Should().HaveCount(3);
            var argumentLiteral = syntax.Arguments[1].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal;
            argumentLiteral.Segments.Should().HaveCount(1);
            argumentLiteral.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("something safe");
        }

        [TestMethod]
        public void Can_parse_nonsafe_literal_without_macro()
        {
            var argumentSyntax = ArgumentListSyntax.Parse("(\"raceinfo\")");
            argumentSyntax.Arguments.Should().HaveCount(1);
            var literal = argumentSyntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal;

            literal.Segments.Should().HaveCount(1);
            literal.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("raceinfo");
        }

        [TestMethod]
        public void Can_parse_nonsafe_literal_with_macro()
        {
            var argumentSyntax = ArgumentListSyntax.Parse("(\"raceinfo_<TAG(race)>\")");
            argumentSyntax.Arguments.Should().HaveCount(1);
            var literal = argumentSyntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal;

            literal.Segments.Should().HaveCount(2);
            literal.Segments[0].Should().BeOfType<TextSegmentSyntax>().Which.Text.Should().Be("raceinfo_");
            literal.Segments[1].Should().BeOfType<MacroSegmentSyntax>().Which.Macro.Expression
                .Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("TAG");
        }

        [TestMethod]
        public void Can_parse_nonsafe_literal_with_macro_only()
        {
            var argumentSyntax = ArgumentListSyntax.Parse("(\"<TAG(race)>\")");
            argumentSyntax.Arguments.Should().HaveCount(1);
            var literal = argumentSyntax.Arguments[0].Should().BeOfType<LiteralArgumentSyntax>().Which.Literal;

            literal.Segments.Should().HaveCount(1);
            literal.Segments[0].Should().BeOfType<MacroSegmentSyntax>().Which.Macro.Expression
                .Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("TAG");
        }

        [TestMethod]
        public void Can_parser_eval_macro()
        {
            var argumentSyntax = ArgumentListSyntax.Parse("(<eval tag(basestr)>)");
            argumentSyntax.Arguments.Should().HaveCount(1);
            var evalArgument = argumentSyntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>().Which;
            evalArgument.Expression.Should().BeOfType<EvalMacroExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_expression()
        {
            var listSyntax = ArgumentListSyntax.Parse("(1+1)");

            listSyntax.Arguments.Should().HaveCount(1);
            var argumentSyntax = listSyntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>().Which;
            var expression = argumentSyntax.Expression.Should().BeOfType<BinaryOperatorSyntax>().Which;
            expression.Operator.Should().Be(BinaryOperatorKind.Add);
        }

        [TestMethod]
        public void Can_parse_multiple_expressions()
        {
            var listSyntax = ArgumentListSyntax.Parse("(1+1, 2*2)");

            listSyntax.Arguments.Should().HaveCount(2);
            listSyntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>();
            listSyntax.Arguments[1].Should().BeOfType<ExpressionArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_eval_as_argument()
        {
            var listSyntax = ArgumentListSyntax.Parse("(<eval 1>)");

            listSyntax.Arguments.Should().HaveCount(1);
            listSyntax.Arguments[0].Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression.Should().BeOfType<EvalMacroExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_resource_argument_in_parentheses()
        {
            var listSyntax = ArgumentListSyntax.Parse("(1000 t_custom_spell)");

            listSyntax.Arguments.Should().HaveCount(1);
            var argument = listSyntax.Arguments[0].Should().BeOfType<ResourceArgumentSyntax>().Which;
            argument.Name.Should().Be("t_custom_spell");
            argument.Amount.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1000");
        }
    }
}
