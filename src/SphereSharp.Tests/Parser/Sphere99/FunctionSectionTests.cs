using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class FunctionSectionTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_function_section()
        {
            ShouldSucceed(@"[function fun1]
call1
call2
call3");
        }

        private void ShouldSucceed(string src)
        {
            Parse(src, parser =>
            {
                parser.functionSection();
            });
        }
    }
}
