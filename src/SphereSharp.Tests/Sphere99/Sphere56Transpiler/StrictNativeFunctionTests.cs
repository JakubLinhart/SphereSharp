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
    public class StrictNativeFunctionTests
    {
        [TestMethod]
        public void P_function()
        {
            TranspileStatementCheck("p(5918,109,10)", "p 5918,109,10");
        }
    }
}
