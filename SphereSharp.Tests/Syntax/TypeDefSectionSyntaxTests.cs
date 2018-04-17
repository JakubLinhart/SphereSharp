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
    public class TypeDefSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_typedef_section()
        {
            var syntax = SectionSyntax.Parse(@"[typedef t_port_randomvicinty]
on=@step
call1
").Should().BeOfType<TypeDefSectionSyntax>().Which;

            syntax.Name.Should().Be("t_port_randomvicinty");
            syntax.Triggers.Should().HaveCount(1);
            syntax.Triggers[0].Name.Should().Be("step");
        }
    }
}
