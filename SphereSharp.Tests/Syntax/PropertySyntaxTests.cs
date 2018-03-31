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
    public class PropertySyntaxTests
    {
        [TestMethod]
        public void Can_parse_property_with_empty_value()
        {
            var syntax = PropertySyntax.Parse("PROMPTMSG=");

            syntax.LValue.Should().Be("PROMPTMSG");
            syntax.RValue.Should().BeEmpty();
        }
    }
}
