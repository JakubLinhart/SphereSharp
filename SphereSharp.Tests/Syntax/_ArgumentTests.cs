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
    public class _ArgumentTests
    {
        [TestMethod]
        public void Can_parse_number()
        {
            var syntax = _ArgumentSyntax.Parse("123").Should().BeOfType<_ExpressionArgumentSyntax>().Which;

            syntax.Segments.Should().HaveCount(1);
            syntax.Segments.First().Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("123");
        }

        [TestMethod]
        public void Can_parse_expression_with_one_operation()
        {
            var syntax = _ArgumentSyntax.Parse("123+321").Should().BeOfType<_ExpressionArgumentSyntax>().Which;

            syntax.Segments.Should().HaveCount(2);
            syntax.Segments.First().Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("123");

            var op = syntax.Segments.ElementAt(1).Should().BeOfType<_OperatorExpressionSegmentSyntax>().Which;
            op.Kind.Should().Be(BinaryOperatorKind.Add);
            op.Operand2.Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("321");
        }

        [TestMethod]
        public void Can_parse_expression_with_two_operations()
        {
            var syntax = _ArgumentSyntax.Parse("123+321-987").Should().BeOfType<_ExpressionArgumentSyntax>().Which;

            syntax.Segments.Should().HaveCount(3);
            syntax.Segments.ElementAt(0).Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("123");

            var op1 = syntax.Segments.ElementAt(1).Should().BeOfType<_OperatorExpressionSegmentSyntax>().Which;
            op1.Kind.Should().Be(BinaryOperatorKind.Add);
            op1.Operand2.Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("321");

            var op2 = syntax.Segments.ElementAt(2).Should().BeOfType<_OperatorExpressionSegmentSyntax>().Which;
            op2.Kind.Should().Be(BinaryOperatorKind.Subtract);
            op2.Operand2.Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("987");
        }

        [TestMethod]
        public void Can_parse_expression_with_one_number_in_parenthesis()
        {
            var syntax = _ArgumentSyntax.Parse("(123)").Should().BeOfType<_ExpressionArgumentSyntax>().Which;

            syntax.Segments.Should().HaveCount(1);

            var subExpression = syntax.Segments.ElementAt(0).Should().BeOfType<_SubExpressionSegmentSyntax>().Which.Segments;
            subExpression.Should().HaveCount(1);
            subExpression.ElementAt(0).Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("123");
        }

        [TestMethod]
        public void Can_parse_subexpression_with_one_operation()
        {
            var syntax = _ArgumentSyntax.Parse("(123+456)").Should().BeOfType<_ExpressionArgumentSyntax>().Which;

            syntax.Segments.Should().HaveCount(1);

            var subExpression = syntax.Segments.ElementAt(0).Should().BeOfType<_SubExpressionSegmentSyntax>().Which.Segments;
            subExpression.Should().HaveCount(2);
            subExpression.ElementAt(0).Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("123");

            var subExpressionOperation1 = subExpression.ElementAt(1).Should().BeOfType<_OperatorExpressionSegmentSyntax>().Which;
            subExpressionOperation1.Kind.Should().Be(BinaryOperatorKind.Add);
            subExpressionOperation1.Operand2.Should().BeOfType<_NumberExpressionSegmentSyntax>().Which.Value.Should().Be("456");
        }
    }
}
