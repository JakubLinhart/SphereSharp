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
    public class EvalSyntaxTests
    {
        [TestMethod]
        public void Can_parse_eval_with_call()
        {
            var syntax = EvalSyntax.Parse("eval tag(basestr)");

            syntax.Expression.Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("tag");
        }

        [TestMethod]
        public void Can_parse_hval_with_call()
        {
            var syntax = EvalSyntax.Parse("hval tag(basestr)");

            syntax.Kind.Should().Be(EvalKind.Hexadecimal);
            syntax.Expression.Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("tag");
        }
    }
}
