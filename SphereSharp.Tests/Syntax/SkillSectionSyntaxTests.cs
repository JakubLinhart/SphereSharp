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
    public class SkillSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_skill_with_multiple_triggers_and_properties()
        {
            var syntax = SectionSyntax.Parse(@"[SKILL 48]
DEFNAME=SKILL_REMOVE_TRAP
KEY=RemoveTrap

ON=@Start
f_removetrap
return 1

on=@fail
return 1

").Should().BeOfType<SkillSectionSyntax>().Which;

            syntax.Name.Should().Be("48");
            syntax.Properties.Should().HaveCount(2);
            syntax.Properties[0].LValue.Should().Be("DEFNAME");
            syntax.Properties[0].RValue.Should().Be("SKILL_REMOVE_TRAP");
            syntax.Properties[1].LValue.Should().Be("KEY");
            syntax.Properties[1].RValue.Should().Be("RemoveTrap");

            syntax.Triggers.Should().HaveCount(2);
        }
    }
}
