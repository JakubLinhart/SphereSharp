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

            syntax.LValue.As<MemberAccessSyntax>().MemberName.Should().Be("var1");
            syntax.RValue.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_assignment_with_whitespace_between_assignment_operator()
        {
            var syntax = AssignmentSyntax.Parse("var1 = 1");

            syntax.LValue.As<MemberAccessSyntax>().MemberName.Should().Be("var1");
            syntax.RValue.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_constant_integer_assignment_followed_by_comment()
        {
            var syntax = AssignmentSyntax.Parse("var1=1 // comment");

            syntax.LValue.As<MemberAccessSyntax>().MemberName.Should().Be("var1");
            syntax.RValue.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_assignment_to_multipart_member_access()
        {
            var syntax = AssignmentSyntax.Parse("var1.var2.var3=1");

            syntax.LValue.As<MemberAccessSyntax>().MemberName.Should().Be("var3");
            syntax.LValue.As<MemberAccessSyntax>().Object.MemberName.Should().Be("var2");
            syntax.LValue.As<MemberAccessSyntax>().Object.Object.MemberName.Should().Be("var1");
            syntax.RValue.As<IntegerConstantExpressionSyntax>().Value.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_complex_assignment()
        {
            var syntax = AssignmentSyntax.Parse("var1=((1+2)*(4+5))-6");

            syntax.LValue.As<MemberAccessSyntax>().MemberName.Should().Be("var1");
            syntax.RValue.As<BinaryOperatorSyntax>().Kind.Should().Be(BinaryOperatorKind.Subtract);
        }

        [TestMethod]
        public void Can_parse_macro_expression_assignment()
        {
            var syntax = AssignmentSyntax.Parse("home=<tag(nation)>");

            syntax.LValue.As<MemberAccessSyntax>().MemberName.Should().Be("home");
            syntax.RValue.As<MacroExpressionSyntax>().Macro.Call.MemberName.Should().Be("tag");
        }

        [TestMethod]
        public void Can_parse_assignment_to_indexed_lvalue()
        {
            var syntax = AssignmentSyntax.Parse("titul_necro[0]=1");

            syntax.LValue.MemberNameSyntax.Should().BeOfType<IndexedSymbolSyntax>()
                .Which.Index.Should().BeOfType<IntegerConstantExpressionSyntax>()
                .Which.Value.Should().Be("0");
        }
    }
}
