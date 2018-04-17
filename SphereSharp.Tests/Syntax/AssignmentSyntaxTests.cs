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
    public class AssignmentSyntaxTests
    {
        [TestMethod]
        public void Can_parse_constant_integer_assignment()
        {
            var syntax = AssignmentSyntax.Parse("var1=1");

            syntax.LValue.Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("var1");
            syntax.RValue.Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression
                .Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_assignment_with_whitespace_between_assignment_operator()
        {
            var syntax = AssignmentSyntax.Parse("var1 = 1");

            syntax.LValue.Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("var1");
            syntax.RValue.Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression
                .Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_constant_integer_assignment_followed_by_comment()
        {
            var syntax = AssignmentSyntax.Parse("var1=1 // comment");

            syntax.LValue.Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("var1");
            syntax.RValue.Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression
                .Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_assignment_to_chained_member_access_with_function_call()
        {
            var syntax = AssignmentSyntax.Parse("var1.fun2(i_something).var3=1");

            syntax.LValue.Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("var1");
            syntax.LValue.Should().BeOfType<CallSyntax>().Which.ChainedCall.MemberName.Should().Be("fun2");
            syntax.LValue.Should().BeOfType<CallSyntax>().Which.ChainedCall.ChainedCall.MemberName.Should().Be("var3");
            syntax.RValue.Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression
                .Should().BeOfType<IntegerConstantExpressionSyntax>().Which.Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_complex_assignment()
        {
            var syntax = AssignmentSyntax.Parse("var1=((1+2)*(4+5))-6");

            syntax.LValue.Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("var1");
            syntax.RValue.Should().BeOfType<ExpressionArgumentSyntax>().Which.Expression
                .Should().BeOfType<BinaryOperatorSyntax>().Which.Operator.Should().Be(BinaryOperatorKind.Subtract);
        }

        [TestMethod]
        public void Can_parse_macro_expression_assignment()
        {
            var syntax = AssignmentSyntax.Parse("home=<tag(nation)>");

            syntax.LValue.Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("home");
            syntax.RValue.Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Segments.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_assignment_to_indexed_lvalue()
        {
            var syntax = AssignmentSyntax.Parse("titul_necro[0]=1");

            syntax.LValue.MemberNameSyntax.Should().BeOfType<IndexedSymbolSyntax>()
                .Which.Index.Should().BeOfType<IntegerConstantExpressionSyntax>()
                .Which.Value.Should().Be("0");
        }

        [TestMethod]
        public void Can_parse_string_assignment()
        {
            var syntax = AssignmentSyntax.Parse("deed=\"Sbalena lod bez klice\"");

            syntax.LValue.MemberName.Should().Be("deed");
            syntax.RValue.Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("Sbalena lod bez klice");
        }

        [TestMethod]
        public void Can_parse_string_without_doublequotes_assignment()
        {
            var syntax = AssignmentSyntax.Parse("NPC=brain_undead");

            syntax.LValue.MemberName.Should().Be("NPC");
            syntax.RValue.Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("brain_undead");
        }
    }
}
