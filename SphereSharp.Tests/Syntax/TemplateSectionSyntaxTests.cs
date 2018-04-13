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
    public class TemplateSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_template_section()
        {
            var syntax = SectionSyntax.Parse(@"[template tm_shamannewbie]
ITEM=i_robe
color=colors_all
item=i_spellbook_3
item=i_bandage_bloody, 100
ITEM=i_staff_gnarled_copper
ITEM=i_dagger_copper
ITEM=i_potion_nightsight, 3
").Should().BeOfType<TemplateSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(7);
        }
    }
}
