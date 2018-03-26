using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class SpellSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_spell_with_properties()
        {
            var syntax = SectionSyntax.Parse(@"[Spell 12]//utocne
DEFNAME=s_harm
NAME=""Harm""
").Should().BeOfType<SpellSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(2);
        }
    }
}
