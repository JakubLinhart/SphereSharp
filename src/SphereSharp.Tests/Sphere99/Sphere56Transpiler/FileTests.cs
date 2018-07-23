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
    public class FileTests
    {
        [TestMethod]
        public void Empty_file()
        {
            TranspileFileCheck("", "");
        }

        [TestMethod]
        public void File_with_EOF_only()
        {
            TranspileFileCheck("[EOF]", "[EOF]");
        }

        [TestMethod]
        public void File_with_EOF_and_leading_whitespace()
        {
            TranspileFileCheck(
@"   
[EOF]",
@"   
[EOF]");
        }

        [TestMethod]
        public void File_with_EOF_and_trailing_whitespace()
        {
            TranspileFileCheck(
@"[EOF]   
   ",
@"[EOF]   
   ");
        }
    }
}
