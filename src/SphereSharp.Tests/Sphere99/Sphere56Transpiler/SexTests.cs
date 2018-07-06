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
    public class SexTests
    {
        [TestMethod]
        public void With_two_unquoted_arguments()
        {
            TranspileStatementCheck("fun1(\"<sex(he,she)>\")", "fun1 \"<sex he/she>\"");
        }

        [TestMethod]
        public void Strips_doublequotes_from_sex_arguments()
        {
            TranspileStatementCheck("fun1(<sex(\"he\",\"she\")>)", "fun1 <sex he/she>");
        }

        [TestMethod]
        public void With_multiple_words()
        {
            TranspileStatementCheck("fun1(<sex(he he he,she she she)>)", "fun1 <sex he he he/she she she>");
        }

        [TestMethod]
        public void With_one_free_unquoted_argument()
        {
            TranspileStatementCheck("fun1(<sex he she>)", "fun1 <sex he/she>");
        }
    }
}
