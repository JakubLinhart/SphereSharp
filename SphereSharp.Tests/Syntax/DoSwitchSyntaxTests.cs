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
    public class DoSwitchSyntaxTests
    {
        [TestMethod]
        public void Can_parse_doswitch_with_blank()
        {
            var syntax = DoSwitchSyntax.Parse(@"doswitch argn
    <blank>
    tag(nation,Gondoran)
enddo
");

            syntax.Condition.Should().BeOfType<CallExpressionSyntax>().Which.Call.MemberName.Should().Be("argn");
            syntax.Cases.Should().HaveCount(2);
        }
    }
}
