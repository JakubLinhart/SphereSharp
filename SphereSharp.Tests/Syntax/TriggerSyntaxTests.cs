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
    public class TriggerSyntaxTests
    {
        [TestMethod]
        public void Can_parse_trigger_with_lower_case_on_keyword()
        {
            var syntax = TriggerSyntax.Parse(@"on=@Create
NPC = brain_undead
FAME = 20
");

            syntax.Name.Should().Be("Create");
        }

        [TestMethod]
        public void Can_parse_trigger_with_upper_case_on_keyword()
        {
            var syntax = TriggerSyntax.Parse(@"ON=@Create
NPC = brain_undead
FAME = 20
");

            syntax.Name.Should().Be("Create");
        }
    }
}
