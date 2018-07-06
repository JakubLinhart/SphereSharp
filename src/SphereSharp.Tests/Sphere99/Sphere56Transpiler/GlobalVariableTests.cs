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
    public class GlobalVariableTests
    {
        [TestMethod]
        public void Can_recognize_chained_global_variable_declaration()
        {
            TranspileCodeBlockCheck(
@"var.v1=1
var.v1=<v1>*15",
@"var.v1=1
var.v1=<var.v1>*15");
        }

        [TestMethod]
        public void Ignores_chained_call_on_global_variable_access_with_arguments()
        {
            TranspileCodeBlockCheck(
@"var(x).more1=1
more1=2",
@"uid.<var0.x>.more1=1
more1=2");
        }

        [TestMethod]
        public void Can_recognize_self_reference_sharp_replacement_for_chained_global_variable_declaration()
        {
            TranspileCodeBlockCheck(
@"var.x=1
var.x=#+1",
@"var.x=1
var.x=<var.x>+1");
        }
    }
}
