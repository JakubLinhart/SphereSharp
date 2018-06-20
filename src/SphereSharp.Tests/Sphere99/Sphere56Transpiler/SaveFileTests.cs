using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.Tests.Sphere99.Sphere56Transpiler.TranspilerTestsHelper;


namespace SphereSharp.Tests.Sphere99.Sphere56Transpiler
{
    [TestClass]
    public class SaveFileTests
    {
        [TestMethod]
        public void Removes_worldchar_properties_with_invalid_values()
        {
            // TODO: must be configurable
            TranspileSaveFileCheck(
@"[WorldChar c_golem_verite]
Serial=0527A6
Tag.npc_knockback=)
P=5631,106",
@"[WorldChar c_golem_verite]
Serial=0527A6
P=5631,106");
        }

        [TestMethod]
        public void Removes_worlditem_properties_with_invalid_values()
        {
            // TODO: must be configurable
            TranspileSaveFileCheck(
@"[WorldItem i_pouch]
Serial=04000378A
Tag.cm_dispid=)
Cont=040002DFA",
@"[WorldItem i_pouch]
Serial=04000378A
Cont=040002DFA");
        }

        [TestMethod]
        public void WorldChar_can_contain_assignment_to_region()
        {
            TranspileSaveFileCheck(
@"[WorldItem i_multi_ship_dragon_medium_w]
Serial=04004F760
REGION.Flag_AntiMagic_RecallIn=1",
@"[WorldItem i_multi_ship_dragon_medium_w]
Serial=04004F760
REGION.Flag_AntiMagic_RecallIn=1");
        }

        [TestMethod]
        public void Can_property_list_contain_empty_assignment()
        {
            TranspileSaveFileCheck(
@"[WorldItem i_multi_ship_dragon_medium_w]
Body",
@"[WorldItem i_multi_ship_dragon_medium_w]
Body");
        }

        [TestMethod]
        public void Transpiles_hex_uid_prefixed_with_sharp_in_section_header()
        {
            TranspileSaveFileCheck(
@"[WorldChar #08e000122]
Serial=07F708",
@"[WorldChar 08e000122]
Serial=07F708");
        }

        [TestMethod]
        public void Can_parse_file_with_leading_properties_with_all_supported_sections()
        {
            TranspileSaveFileCheck(
@"Title=""Sphere World Script""
Version=""0.99z8""
Time=535975603
SaveCount=29760

[VarNames]
Wrestling=40

[WorldChar c_drapac_blood]
Serial=02B1E4

[WorldItem i_varGate]
Serial=040009AE2

[Sector 0,0]
LocalLight=3
RainChance=0
ColdChance=0
",
@"Title=""Sphere World Script""
Version=""0.99z8""
Time=535975603
SaveCount=29760

[GLOBALS]
Wrestling=4.0

[WorldChar c_drapac_blood]
Serial=02B1E4

[WorldItem i_varGate]
Serial=040009AE2

[Sector 0,0]
LocalLight=3
RainChance=0
ColdChance=0
");
        }
    }
}
