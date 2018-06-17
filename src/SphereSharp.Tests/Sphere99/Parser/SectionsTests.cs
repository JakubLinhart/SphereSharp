using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Sphere99.Parser
{
    [TestClass]
    public class SectionsTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_itemdef_section_with_properties_and_triggers()
        {
            CheckStructure("props:3;triggers:3;", @"[itemdef i_dclickME]
name=vain dclicker
id=i_memory
type=t_script

on=@create
attr=010
timer=1

on=@UserDClick
return 1

on=@timer
remove");
        }

        [TestMethod]
        public void Can_parse_empty_itemdef_section()
        {
            CheckStructure("triggers:0;", @"[itemdef i_something]
");
        }

        [TestMethod]
        public void Can_parse_itemdef_without_triggers()
        {
            CheckStructure("props:3;triggers:0;", @"[itemdef i_dclickME]
name=vain dclicker
id=i_memory
type=t_script");
        }

        [TestMethod]
        public void Can_parse_ItemDef_with_comments_and_empty_lines_in_property_list()
        {
            CheckStructure("props:3;triggers:0;", @"[itemdef i_dclickME] // comment after header
// comment as the first property line
name=vain dclicker // comment after property

// empty line before this line is important for this test
id=i_memory
type=t_script");
        }

        [TestMethod]
        public void Can_parse_itemdef_with_number_as_name()
        {
            CheckStructure("props:1;triggers:0;", @"[ItemDef 0ee9]
DUPEITEM = 0e21");
        }

        [TestMethod]
        public void Can_parse_events_section()
        {
            CheckStructure("triggers:2;", @"[events e_something]
on=@step
  call1

on=@timer
  call2
");
        }

        [TestMethod]
        public void Can_parse_chardef_section()
        {
            CheckStructure("props:2;triggers:1;", @"[chardef c_dummy]
id=c_man
name=dummy

on=@create
flag_insubstantial=1
");
        }

        [TestMethod]
        public void Can_parse_template_section_with_property()
        {
            CheckStructure("props:1;", @"[template tmp_ingots]
container=i_pouch
");
        }

        [TestMethod]
        public void Can_parse_TypeDef_section_with_triggers()
        {
            CheckStructure("triggers:2;", @"[typedef t_port_randomvicinty]
on=@step
  call1

on=@timer
  call2
");
        }

        [TestMethod]
        public void Can_parse_defnames_section()
        {
            CheckStructure("props:4;", @"[defnames def_eqTarget]
def_rearAbilities_manareg_class[0]                   //everyone
def_eqTarget_Item        000000001
def_eqTarget_Char        000000002
regy_defs[0]  i_reag_black_pearl,3962");
        }

        [TestMethod]
        public void Can_parse_dialog_section_with_position()
        {
            CheckStructure("", @"[dialog d_input]
350,350
argo.tag(sirka,400)");
        }

        [TestMethod]
        public void Can_parse_dialog_section_without_position()
        {
            CheckStructure("", @"[dialog d_input]
argo.tag(sirka,400)");
        }

        [TestMethod]
        public void Can_parse_dialog_text_section()
        {
            CheckStructure("", @"[dialog d_input TEXT]
some random text
other random text
");
        }

        [TestMethod]
        public void Can_parse_dialog_button_section()
        {
            CheckStructure("buttonTriggers:5;", @"[dialog d_zmenajmena_duvod button]
on=0
dialog(d_zmenajmena_duvod)

on=1
dialog(d_zmenajmena_duvod)

onbutton=2
dialog(d_zmenajmena_duvod)

on=3

on=@anybutton

");
        }

        [TestMethod]
        public void Can_parse_book_section()
        {
            CheckStructure("props:3;", @"[BOOK kniha_hcn_vstup]
PAGES=23
TITLE=""Kronika""
AUTHOR = neznamy..");
        }

        [TestMethod]
        public void Can_parse_book_page_section()
        {
            CheckStructure("", @"[BOOK kniha_hcn_vstup 2]
den 44
Hrobky jsou zajisteny,

[EOF]
");
        }

        [TestMethod]
        public void Can_parse_typedefs_section()
        {
            CheckStructure("props:3;", @"[TYPEDEFS]
t_normal                  0
t_container               1   // any unlocked container or corpse
t_container_locked        2");
        }

        [TestMethod]
        public void Can_parse_speech_section()
        {
            CheckStructure("speechTriggers:3;", @"[SPEECH spk_human_prime]
ON=*
    call1

ON=*hire*
    call2

ON=*train*
ON=*teach*
    call3
");
        }

        [TestMethod]
        public void Can_parse_speech_trigger_with_whitespace_around_assign_operator()
        {
            CheckStructure("speechTriggers:1;", @"[SPEECH spk_human_prime]
ON = *hire*
    call2
");
        }

        [TestMethod]
        public void Can_parse_comment_section_without_name()
        {
            CheckStructure("commentLines:2;", @"[comment]
line 1
line 2
");
        }

        [TestMethod]
        public void Can_parse_comment_section_with_name()
        {
            CheckStructure("commentLines:2;", @"[comment function xxx]
comment line 1
comment line 2
");
        }

        private void CheckStructure(string expectedResult, string src)
        {
            sphereScript99Parser.SectionContext section = null;

            Parse(src, parser =>
            {
                section = parser.section();
            });

            var extractor = new PropertyAndTriggerExtractor();
            extractor.Visit(section);

            extractor.Output.Should().Be(expectedResult);
        }

        [TestMethod]
        public void Can_parse_profession_section()
        {
            CheckStructure("props:3;triggers:1;", @"[profession 1]
DEFNAME=class_necro
NAME=Necro
STATSUM=1000

on=@login
events e_character");
        }

        [TestMethod]
        public void Can_parse_spell_section()
        {
            CheckStructure("props:3;triggers:1;", @"[SPELL 1]
DEFNAME=s_clumsy
NAME=Clumsy
SOUND=snd_spell_clumsy

ON=@Effect
   call1");
        }

        [TestMethod]
        public void Can_parse_spell_section_without_triggers()
        {
            CheckStructure("props:3;triggers:0;", @"[SPELL 1]
DEFNAME=s_clumsy
NAME=Clumsy
SOUND=snd_spell_clumsy");
        }

        [TestMethod]
        public void Can_parse_areadef_section()
        {
            CheckStructure("props:3;",
@"[AREA Gondorske uzemi]//dtto
GROUP=Gondorske uzemi
P=1520, 1704
Rect=1,606,1984,2328");
        }

        [TestMethod]
        public void Can_parse_area_section()
        {
            CheckStructure("props:3;",
@"[AREA Gondorske uzemi]//dtto
GROUP=Gondorske uzemi
P=1520, 1704
Rect=1,606,1984,2328");
        }

        [TestMethod]
        public void Can_parse_are_section_with_tag_assignment()
        {
            CheckStructure("props:2;",
@"[AREA Stredozeme]
CLASS=11
tag.realm=3");
        }

        [TestMethod]
        public void Can_parse_regiontype_section_with_subname()
        {
            CheckStructure("props:1;triggers:0;",
@"[REGIONTYPE r_default_rock t_rock]
RESOURCES=100 r_nothing");
        }

        [TestMethod]
        public void Can_parse_regiontype_section()
        {
            CheckStructure("props:1;triggers:0;",
@"[REGIONTYPE r_default_rock]
RESOURCES=100 r_nothing");
        }

        [TestMethod]
        public void Can_parse_regiontype_section_with_triggers()
        {
            CheckStructure("props:1;triggers:1;",
@"[REGIONTYPE r_default_rock]
RESOURCES=100 r_nothing

ON=@CLIPERIODIC
  SRC.MIDILIST=midi_ForestA");
        }

        [TestMethod]
        public void Can_parse_regiontype_section_without_properties()
        {
            CheckStructure("triggers:1;",
@"[REGIONTYPE r_moria_mesto]

ON=@ENTER
  src.sysmessage(Nachazis se v Morii.)
  SRC.MIDILIST=midi_LBCastle");
        }

        [TestMethod]
        public void Can_parse_regionresource_section()
        {
            CheckStructure("props:3;",
@"[REGIONRESOURCE o_verite]
// t_rock
SKILL=70.0,110.0
AMOUNT=10,15
REAP=i_ore_verite");
        }

        [TestMethod]
        public void Can_parse_names_section()
        {
            CheckStructure("free:3;",
@"[NAMES NAMES_ENT]
53
Stromovous
Listek
Listovlas
");
        }

        [TestMethod]
        public void Spawn_section()
        {
            CheckStructure("props:3;",
@"[SPAWN s_undead_magic_weak]
DEFNAME=s_undead_magic_weak
ID=c_skeleton_mage
ID=c_shadow
");
        }

        [TestMethod]
        public void Can_parse_menu_section()
        {
            CheckStructure("menuTriggers:2;",
@"[MENU m_cons_tool]
Menu Kanclere
ON=0 Presunout char/item
return 1
ON=0 Stats a ability na zaklad. (prikaz .zakladabilities, .zakladstats)
return 1");
        }

        [TestMethod]
        public void Can_parse_scroll_section()
        {
            CheckStructure("free:1;",
@"[scroll sc_q029_alchnezdar_planek]
Vidis zbesilou cmaranici plnou skrtanic a nesrozumitelnych popisku.
");
        }
    }
}
