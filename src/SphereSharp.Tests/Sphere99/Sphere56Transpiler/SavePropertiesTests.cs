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
    public class SavePropertiesTests
    {
        [TestMethod]
        public void Removes_Age_for_WorldItem()
        {
            TranspileSaveFileCheck(
@"[WorldItem i_staff_gnarled_valorite]
SERIAL=040001fc0
Age=028D53E
",
@"[WorldItem i_staff_gnarled_valorite]
SERIAL=040001fc0
");
        }

        [TestMethod]
        public void Translates_Age_to_Create_for_WorldChar()
        {
            TranspileSaveFileCheck(
@"[WorldChar c_drapac_blood]
Age=0F6E92
",
@"[WorldChar c_drapac_blood]
Create=0F6E92
");
        }

        [TestMethod]
        public void Evaluates_specific_flags_to_Flag_property()
        {
            TranspileSaveFileCheck(
@"[WorldChar c_drapac_blood]
Flag_InDoors=1
Flag_Reflection=1
Flag_Spawned=1
Flag_Invul=1
Flag_OnHorse=1",
@"[WorldChar c_drapac_blood]
FLAGS=091000201");
        }

        [TestMethod]
        public void Evaluates_specific_attrs_to_Attr_property()
        {
            TranspileSaveFileCheck(
@"[WorldItem i_staff_gnarled_valorite]
Attr_MoveNever=1
Attr_Newbie=1
Attr_Invis=1
Attr_Magic=1
",
@"[WorldItem i_staff_gnarled_valorite]
ATTR=0000000B4");
        }

        [TestMethod]
        public void Removes_doublequotes_from_MORE1_and_MORE2_property()
        {
            TranspileSaveFileCheck(
@"[WorldItem i_something]
MORE1=""c_drapac_blood""
MORE2=""c_drapac_blood""",
@"[WorldItem i_something]
MORE1=c_drapac_blood
MORE2=c_drapac_blood");
        }

        [TestMethod]
        public void Preserves_MORE1_and_MORE2_value_without_doublequotes()
        {
            TranspileSaveFileCheck(
@"[WorldItem i_something]
MORE1=0B000B
MORE2=0B000C",
@"[WorldItem i_something]
MORE1=0B000B
MORE2=0B000C");
        }

        [TestMethod]
        public void Strips_sharp_from_hexadecimal_tag_values()
        {
            TranspileSaveFileCheck(
@"[worlditem i_something]
Tag.equip1=""#040000D10""",
@"[worlditem i_something]
Tag.equip1=""040000D10""");
        }

        [TestMethod]
        public void Strips_sharp_from_varnames_values()
        {
            TranspileToDataSaveFileCheck(
@"[varnames]
globalvar=#0400009DB
",
@"[GLOBALS]
globalvar=0400009DB");
        }

        [TestMethod]
        public void Transpiles_hex_uid_enclosed_in_doublequotes_and_prefixed_with_sharp()
        {
            TranspileSaveFileCheck(
@"[worlditem i_something]
Tag.combatSource=#076C56",
@"[worlditem i_something]
Tag.combatSource=076C56");
        }


        [TestMethod]
        public void Keeps_sharp_in_nonhexadecimal_tag_value()
        {
            TranspileSaveFileCheck(
@"[worlditem i_something]
Tag.combatSource=#0nonhexasokeepit",
@"[worlditem i_something]
Tag.combatSource=#0nonhexasokeepit");

        }
    }
    
}
