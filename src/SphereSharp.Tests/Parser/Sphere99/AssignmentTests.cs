using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class AssignmentTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_basic_assignments()
        {
            ShouldSucceed("x=1");
            ShouldSucceed("x=(1+1)");
            ShouldSucceed("x=(1+1),(1+1)");
            ShouldSucceed("x=(1+1),(1+1)");
            ShouldSucceed("x=1,2,3");
            ShouldSucceed("x=x,y,z");
            ShouldSucceed("x=<macro1><macro2>");
            ShouldSucceed("x=<macro1>,<macro2>");
            ShouldSucceed("x=<macro1><macro2>,<macro3><macro4>");
            ShouldSucceed("x=and now some, unquoted fun");
            ShouldSucceed("myDclicker.p=<p>");
            ShouldSucceed("type=t_potion_bomba");
            ShouldSucceed("src.fun1(something).fun2(1+1).link=src.fun1(something).fun3(2)");
            ShouldSucceed("flags=<eval <asdf>&~2>");
        }

        [TestMethod]
        public void Can_parse_assignment_with_whitespace_around_assign_operator()
        {
            ShouldSucceed("x = 1");
        }

        [TestMethod]
        public void Can_parse_empty_assignment()
        {
            ShouldSucceed("x=");
        }

        private void ShouldSucceed(string src)
        {
            Parse(src, parser =>
            {
                parser.assignment();
            });
        }
    }
}
