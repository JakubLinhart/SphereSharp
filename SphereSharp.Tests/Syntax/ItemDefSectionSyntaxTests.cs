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
    public class ItemDefSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_itemdef_with_graphics_and_defname()
        {
            var syntax = SectionSyntax.Parse(@"[ITEMDEF 0f6c]
DEFNAME=i_moongate_blue
");

            syntax.Should().BeOfType<ItemDefSectionSyntax>();
            syntax.As<ItemDefSectionSyntax>().Properties.Should().HaveCount(1);
            syntax.As<ItemDefSectionSyntax>().Properties[0].LValue.Should().Be("DEFNAME");
            syntax.As<ItemDefSectionSyntax>().Properties[0].RValue.Should().Be("i_moongate_blue");
        }

        [TestMethod]
        public void Can_parse_itemdef_with_defname_in_section_header_and_id()
        {
            var syntax = SectionSyntax.Parse(@"[ITEMDEF I_stone_raceclass]
ID=i_grave_stone_4
");

            syntax.Should().BeOfType<ItemDefSectionSyntax>();
            syntax.As<ItemDefSectionSyntax>().Properties.Should().HaveCount(1);
            syntax.As<ItemDefSectionSyntax>().Properties[0].LValue.Should().Be("ID");
            syntax.As<ItemDefSectionSyntax>().Properties[0].RValue.Should().Be("i_grave_stone_4");
        }

        [TestMethod]
        public void Can_parse_itemdef_with_number_in_property_name()
        {
            var syntax = SectionSyntax.Parse(@"[ITEMDEF 0edb]
TDATA2=066").Should().BeOfType<ItemDefSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(1);
            syntax.Properties[0].LValue.Should().Be("TDATA2");
        }

        [TestMethod]
        public void Can_parse_itemdef_with_multiple_properties()
        {
            var syntax = SectionSyntax.Parse(@"[ITEMDEF I_stone_raceclass]
NAME=Stone of the Beginning
ID=i_grave_stone_4
type=t_script
");

            syntax.Should().BeOfType<ItemDefSectionSyntax>();
            syntax.As<ItemDefSectionSyntax>().Properties.Should().HaveCount(3);
            syntax.As<ItemDefSectionSyntax>().Properties[0].LValue.Should().Be("NAME");
            syntax.As<ItemDefSectionSyntax>().Properties[0].RValue.Should().Be("Stone of the Beginning");
            syntax.As<ItemDefSectionSyntax>().Properties[1].LValue.Should().Be("ID");
            syntax.As<ItemDefSectionSyntax>().Properties[1].RValue.Should().Be("i_grave_stone_4");
            syntax.As<ItemDefSectionSyntax>().Properties[2].LValue.Should().Be("type");
            syntax.As<ItemDefSectionSyntax>().Properties[2].RValue.Should().Be("t_script");
        }

        [TestMethod]
        public void Can_parse_itemdef_with_triggers()
        {
            var syntax = SectionSyntax.Parse(@"[ITEMDEF I_stone_raceclass]
NAME=Stone of the Beginning

on=@create
color=0481");

            syntax.Should().BeOfType<ItemDefSectionSyntax>().Which.Triggers.Should().HaveCount(1);
            syntax.As<ItemDefSectionSyntax>().Triggers[0].Name.Should().Be("create");
            syntax.As<ItemDefSectionSyntax>().Triggers[0].CodeBlock.Statements.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_itemdef_with_multiple_properties_and_multiple_triggers()
        {
            var syntax = SectionSyntax.Parse(@"[ITEMDEF I_stone_raceclass]
NAME=Stone of the Beginning

ID=i_grave_stone_4
type=t_script

on=@create
color=0481

on=@userdclick
return 1").Should().BeOfType<ItemDefSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(3);
            syntax.Triggers.Should().HaveCount(2);
        }
    }
}
