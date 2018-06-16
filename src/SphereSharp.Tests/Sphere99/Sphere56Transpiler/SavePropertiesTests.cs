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
    }
}
