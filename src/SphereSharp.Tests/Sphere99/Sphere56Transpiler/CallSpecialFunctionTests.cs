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
    public class CallSpecialFunctionTests
    {
        [TestMethod]
        public void Strcmpi_with_two_argv()
        {
            TranspileConditionCheck("strcmpi(<argv(0)>,<argv(1)>)", "strcmpi(<argv[0]>,<argv[1]>)");
        }

        [TestMethod]
        public void Strcmp_with_two_argv()
        {
            TranspileConditionCheck("strcmp(<argv(0)>,<argv(1)>)", "strcmp(<argv[0]>,<argv[1]>)");
        }
    }
}
