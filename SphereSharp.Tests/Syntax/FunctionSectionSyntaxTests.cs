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
    public class FunctionSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_function_section()
        {
            var syntax = SectionSyntax.Parse(@"
[FUNCTION dialogclose]
src.CloseDialog(<argv(0)>, argn)
").Should().BeOfType<FunctionSectionSyntax>().Which;

            syntax.Name.Should().Be("dialogclose");
            syntax.Body.Statements.Should().HaveCount(1);
        }
    }
}
