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
    public class CharDefSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_chardef_with_graphics_and_defname()
        {
            var syntax = SectionSyntax.Parse(@"[CHARDEF 03]
defname=c_zombie
").Should().BeOfType<CharDefSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(1);
            syntax.Properties[0].LValue.Should().Be("defname");
            syntax.Properties[0].RValue.Should().Be("c_zombie");
        }

        [TestMethod]
        public void Can_parse_chardef_with_defname_in_section_header_and_id()
        {
            var syntax = SectionSyntax.Parse(@"[CHARDEF c_zombie_nb]
ID=c_zombie
").Should().BeOfType<CharDefSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(1);
            syntax.Properties[0].LValue.Should().Be("ID");
            syntax.Properties[0].RValue.Should().Be("c_zombie");
        }

        [TestMethod]
        public void Can_parse_chardef_with_multiple_properties()
        {
            var syntax = SectionSyntax.Parse(@"[CHARDEF 03]
defname=c_zombie
NAME=Zombie
SOUND=snd_MONSTER_GHOST1
").Should().BeOfType<CharDefSectionSyntax>().Which; ;

            syntax.Properties.Should().HaveCount(3);
            syntax.Properties[0].LValue.Should().Be("defname");
            syntax.Properties[0].RValue.Should().Be("c_zombie");
            syntax.Properties[1].LValue.Should().Be("NAME");
            syntax.Properties[1].RValue.Should().Be("Zombie");
            syntax.Properties[2].LValue.Should().Be("SOUND");
            syntax.Properties[2].RValue.Should().Be("snd_MONSTER_GHOST1");
        }

        [TestMethod]
        public void Can_parse_chardef_with_triggers()
        {
            var syntax = SectionSyntax.Parse(@"[CHARDEF I_stone_raceclass]
NAME=Stone of the Beginning

on=@create
color=0481").Should().BeOfType<CharDefSectionSyntax>().Which;

            syntax.Triggers.Should().HaveCount(1);
            syntax.Triggers[0].Name.Should().Be("create");
            syntax.Triggers[0].CodeBlock.Statements.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_chardef_with_multiple_properties_and_multiple_triggers()
        {
            var syntax = SectionSyntax.Parse(@"[CHARDEF c_mummy]
defname=c_zombie
NAME=Zombie
SOUND=snd_MONSTER_GHOST1

on=@Create
NPC=brain_undead
FAME=20

on=@NPCRestock
ITEM=loot_undead_body
").Should().BeOfType<CharDefSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(3);
            syntax.Triggers.Should().HaveCount(2);
        }

        [TestMethod]
        public void Can_parse_chardef_with_multiple_comment_lines()
        {
            var syntax = SectionSyntax.Parse(@"[CHARDEF c_mummy]
defname=c_zombie
NAME=Zombie
//=r_dungeon,r_spooky
//=i_gold
SOUND=snd_MONSTER_GHOST1

on=@Create
NPC=brain_undead
FAME=20

on=@NPCRestock
ITEM=loot_undead_body
").Should().BeOfType<CharDefSectionSyntax>().Which;

            syntax.Properties.Should().HaveCount(3);
            syntax.Triggers.Should().HaveCount(2);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var syntax = SectionSyntax.Parse(@"[CHARDEF 03]
defname=c_zombie
NAME = Zombie
SOUND=snd_MONSTER_GHOST1
ICON = i_pet_ZOMBIE
ANIM=078c7f
CAN = MT_WALK

ATTACK=16
ARMOR=15
MOVERATE=3

RESOURCES=7 t_eerie_stuff
BLOODCOLOR = colors_green

TEVENTS=e_monster
TEVENTS = e_character
TEVENTS=e_selfreser
TEVENTS = e_undead

CATEGORY=Monsters
SUBSECTION = Undeads
DESCRIPTION=Zombie

ON=@Create
FAME=20").Should().BeOfType<CharDefSectionSyntax>().Which;
        }
    }
}
