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
    public class ExpressionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_decadic_integer()
        {
            var syntax = ExpressionSyntax.Parse("123").Should().BeOfType<IntegerConstantExpressionSyntax>().Which;

            syntax.Kind.Should().Be(ConstantExpressionSyntaxKind.Decadic);
            syntax.Value.Should().Be("123");
        }

        [TestMethod]
        public void Can_parse_negative_decadic_integer()
        {
            var syntax = ExpressionSyntax.Parse("-123").Should().BeOfType<IntegerConstantExpressionSyntax>().Which;

            syntax.Kind.Should().Be(ConstantExpressionSyntaxKind.Decadic);
            syntax.Value.Should().Be("-123");
        }

        [TestMethod]
        public void Can_parse_decadic_integer_starting_with_single_digit_ending_with_macro()
        {
            var syntax = ExpressionSyntax.Parse("0<x>").Should().BeOfType<MacroIntegerConstantExpressionSyntax>().Which;
            syntax.FirstDigits.Should().Be("0");
            syntax.Macro.Macro.Expression.Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("x");
        }

        [TestMethod]
        public void Can_parse_more_than_operator_with_macro_integer_and_integer()
        {
            var syntax = ExpressionSyntax.Parse("0<x>>1").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.MoreThan);
            syntax.Operand1.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_more_than_operator_with_integer_and_macro_integer()
        {
            var syntax = ExpressionSyntax.Parse("1>0<x>").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.MoreThan);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>();
            syntax.Operand2.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_more_than_operator_with_macro_integer_and_macro_integer()
        {
            var syntax = ExpressionSyntax.Parse("0<x>>1<y>").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.MoreThan);
            syntax.Operand1.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
            syntax.Operand2.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_less_than_operator_with_macro_integer_and_integer()
        {
            var syntax = ExpressionSyntax.Parse("0<x><1").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.LessThan);
            syntax.Operand1.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_less_than_operator_with_integer_and_macro_integer()
        {
            var syntax = ExpressionSyntax.Parse("1<0<x>").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.LessThan);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>();
            syntax.Operand2.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_less_than_operator_with_macro_integer_and_macro_integer()
        {
            var syntax = ExpressionSyntax.Parse("0<x><1<y>").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.LessThan);
            syntax.Operand1.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
            syntax.Operand2.Should().BeOfType<MacroIntegerConstantExpressionSyntax>();
        }

        [TestMethod]
        public void Can_parse_decadic_decimal()
        {
            var syntax = ExpressionSyntax.Parse("123.321").Should().BeOfType<DecimalConstantExpressionSyntax>().Which;

            syntax.Kind.Should().Be(ConstantExpressionSyntaxKind.Decadic);
            syntax.Value.Should().Be("123.321");
        }

        [TestMethod]
        public void Can_parse_integer_interval()
        {
            var syntax = ExpressionSyntax.Parse("{-2000 -3999}").Should().BeOfType<IntervalExpressionSyntax>().Which;

            syntax.MinValue.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("-2000");
            syntax.MaxValue.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("-3999");
        }



        [TestMethod]
        public void Can_parse_decimal_interval()
        {
            var syntax = ExpressionSyntax.Parse("{3.0 4.0}").Should().BeOfType<IntervalExpressionSyntax>().Which;

            syntax.MinValue.Should().BeOfType<DecimalConstantExpressionSyntax>().Which.Value.Should().Be("3.0");
            syntax.MaxValue.Should().BeOfType<DecimalConstantExpressionSyntax>().Which.Value.Should().Be("4.0");
        }

        [TestMethod]
        public void Can_parse_expression_interval()
        {
            var syntax = ExpressionSyntax.Parse("{(1+1) (2-2)}").Should().BeOfType<IntervalExpressionSyntax>().Which;

            syntax.MinValue.Should().BeOfType<BinaryOperatorSyntax>().Which.Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.MaxValue.Should().BeOfType<BinaryOperatorSyntax>().Which.Operator.Should().Be(BinaryOperatorKind.Subtract);
        }

        [TestMethod]
        public void Can_parse_hex_integer()
        {
            var syntax = ExpressionSyntax.Parse("0DEAD");

            syntax.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Kind.Should().Be(ConstantExpressionSyntaxKind.Hex);
            syntax.As<IntegerConstantExpressionSyntax>().Value.Should().Be("DEAD");
        }

        [TestMethod]
        public void Can_parse_logical_not_operator()
        {
            var syntax = ExpressionSyntax.Parse("!123");

            syntax.As<UnaryOperatorSyntax>()
                .Kind.Should().Be(UnaryOperatorKind.LogicalNot);
            syntax.As<UnaryOperatorSyntax>()
                .Operand.As<IntegerConstantExpressionSyntax>().Value.Should().Be("123");
        }

        [TestMethod]
        public void Can_parse_logical_not_operator_on_expression_with_parenthesis()
        {
            var syntax = ExpressionSyntax.Parse("!(1==2)");

            syntax.As<UnaryOperatorSyntax>().Kind.Should().Be(UnaryOperatorKind.LogicalNot);
        }

        [TestMethod]
        public void Can_parse_macro_expression()
        {
            var syntax = ExpressionSyntax.Parse("<strlen(nation)>");

            syntax.Should().BeOfType<MacroExpressionSyntax>().Which.Macro.Expression
                .Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("strlen");
        }

        [TestMethod]
        public void Can_parse_eval_macro_expression()
        {
            var syntax = ExpressionSyntax.Parse("<eval tag.basestr>").
                Should().BeOfType<EvalMacroExpressionSyntax>().Which;

            syntax.Expression.Should().BeOfType<CallExpressionSyntax>().Which
                .Call.MemberName.Should().Be("tag");
        }

        [TestMethod]
        public void Can_parse_eval_macro_expression_with_BinaryOr_operator()
        {
            var syntax = ExpressionSyntax.Parse("<eval 1|2>").
                Should().BeOfType<EvalMacroExpressionSyntax>().Which;

            syntax.Expression.Should().BeOfType<BinaryOperatorSyntax>().Which.OperatorString.Should().Be("|");
        }

        [TestMethod]
        public void Can_parse_add_operator()
        {
            var syntax = ExpressionSyntax.Parse("1+2");
            syntax.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("2");
        }

        [TestMethod]
        public void Can_parse_multiple_add_operators()
        {
            var syntax = ExpressionSyntax.Parse("1+2+3").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("3");

            var operand1 = syntax.Operand1.Should().BeOfType<BinaryOperatorSyntax>().Which;
            operand1.Operator.Should().Be(BinaryOperatorKind.Add);
            operand1.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            operand1.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("2");
        }

        [TestMethod]
        public void CanParse_whitespace_between_comparison_operators_and_operands()
        {
            var syntax = ExpressionSyntax.Parse("1 < 2").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.LessThan);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("2");
        }

        [TestMethod]
        public void Can_parse_minus_operator()
        {
            var syntax = ExpressionSyntax.Parse("1-2");
            syntax.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Subtract);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("2");
        }

        [TestMethod]
        public void Can_parse_multiply_operator()
        {
            var syntax = ExpressionSyntax.Parse("1*2");

            syntax.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Multiply);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("2");
        }

        [TestMethod]
        public void Can_parse_divide_operator()
        {
            var syntax = ExpressionSyntax.Parse("1/2");

            syntax.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Divide);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("2");
        }

        [TestMethod]
        public void Can_parse_logical_or_operator()
        {
            var syntax = ExpressionSyntax.Parse("1||0");

            syntax.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.LogicalOr);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_binary_or_operator()
        {
            var syntax = ExpressionSyntax.Parse("1|0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.BinaryOr);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_logical_and_operator()
        {
            var syntax = ExpressionSyntax.Parse("1&&0");

            syntax.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.LogicalAnd);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_binary_and_operator()
        {
            var syntax = ExpressionSyntax.Parse("1&0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.BinaryAnd);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_equal_operator()
        {
            var syntax = ExpressionSyntax.Parse("1==0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_not_equal_operator()
        {
            var syntax = ExpressionSyntax.Parse("1!=0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.NotEqual);
            syntax.Operand1.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
            syntax.Operand2.As<IntegerConstantExpressionSyntax>().Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_more_than_operator()
        {
            var syntax = ExpressionSyntax.Parse("1>0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.MoreThan);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_more_than_or_equal_operator()
        {
            var syntax = ExpressionSyntax.Parse("1>=0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.MoreThanOrEqual);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_less_than_operator()
        {
            var syntax = ExpressionSyntax.Parse("1<0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.LessThan);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_less_than_or_equal_operator()
        {
            var syntax = ExpressionSyntax.Parse("1<=0").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.LessThanOrEqual);
            syntax.Operand1.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_multiple_operators_in_expression_without_parenthesis()
        {
            var syntax = ExpressionSyntax.Parse("1+2+3");

            syntax.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<BinaryOperatorSyntax>()
                .Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand1.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("2");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("3");
        }

        [TestMethod]
        public void Can_recognize_binary_operator_in_parenthesis_as_enclosed()
        {
            var syntax = ExpressionSyntax.Parse("(1+2)");
            syntax.Enclosed.Should().BeTrue();
        }

        [TestMethod]
        public void Can_recognize_binary_operator_as_NOT_enclosed()
        {
            var syntax = ExpressionSyntax.Parse("1+2");
            syntax.Enclosed.Should().BeFalse();
        }

        [TestMethod]
        public void Can_parse_expression_with_nested_subexpressions_in_parenthesis()
        {
            var syntax = ExpressionSyntax.Parse("(1+2)+(3+(4+5))");

            syntax.Should().BeOfType<BinaryOperatorSyntax>().Which.Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.Should().BeOfType<BinaryOperatorSyntax>().Which.Operand1.As<BinaryOperatorSyntax>()
                .Operator.Should().Be(BinaryOperatorKind.Add);

            syntax.As<BinaryOperatorSyntax>().Operand1.As<BinaryOperatorSyntax>()
                .Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.As<BinaryOperatorSyntax>().Operand1.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("1");
            syntax.As<BinaryOperatorSyntax>().Operand1.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("2");

            syntax.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>()
                .Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("3");

            syntax.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>()
                .Operator.Should().Be(BinaryOperatorKind.Add);
            syntax.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>().Operand1.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("4");
            syntax.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>().Operand2.As<BinaryOperatorSyntax>().Operand2.As<IntegerConstantExpressionSyntax>()
                .Value.Should().Be("5");
        }

        [TestMethod]
        public void Can_parse_method_call_expression()
        {
            var syntax = ExpressionSyntax.Parse("strlen(\"asdf\")");

            syntax.As<CallExpressionSyntax>().Call.MemberName.Should().Be("strlen");
            syntax.As<CallExpressionSyntax>().Call.Arguments.Arguments[0].As<LiteralArgumentSyntax>().Literal.Text.Should().Be("asdf");
        }

        [TestMethod]
        public void Can_parse_more_than_operator_with_macros()
        {
            var syntax = ExpressionSyntax.Parse("strlen(<?classinfo?>)>5").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.MoreThan);
            syntax.Operand1.Should().BeOfType<CallExpressionSyntax>();
            syntax.Operand2.Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("5");
        }

        [TestMethod]
        public void Can_parse_call_as_first_argument_of_subtract_operator_in_parentheses()
        {
            var syntax = ExpressionSyntax.Parse("(p_x-10)").Should().BeOfType<BinaryOperatorSyntax>().Which;

            syntax.Operator.Should().Be(BinaryOperatorKind.Subtract);
        }
    }
}
