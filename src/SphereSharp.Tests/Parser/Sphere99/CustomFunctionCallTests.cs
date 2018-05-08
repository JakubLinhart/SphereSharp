using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class CustomFunctionCallTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_basic_calls()
        {
            ShouldSucceed("fun1");
            ShouldSucceed("fun1()");
            ShouldSucceed(@"fun1(1)");
            ShouldSucceed("fun1_2");
            ShouldSucceed("fun1_2(3)");
        }

        [TestMethod]
        public void Can_parse_chained_calls()
        {
            ShouldSucceed("src.isdead");
            ShouldSucceed("src.targ.type");
            ShouldSucceed("src.findid(i_vyrobce_svitku)");
            ShouldSucceed("src.findid(i_vyrobce_svitku).remove");
            ShouldSucceed("src.findid(i_vyrobce_svitku).somefun(1)");
        }

        [TestMethod]
        public void Can_parse_parameters_as_chained_calls()
        {
            ShouldSucceed("findlayer.2");
            ShouldSucceed("findlayer.2.uid");
            ShouldSucceed("findlayer.2+2.uid");
            ShouldSucceed("findlayer.some unquoted literal.uid");
            ShouldSucceed("findlayer.\"some quoted literal\".uid");
            ShouldSucceed("lastnew.tag.myfood");
        }

        [TestMethod]
        public void Can_parse_functions_with_parametrized_names()
        {
            ShouldSucceed("fun1<param1>");
            ShouldSucceed("fun1_<param1>");
            ShouldSucceed("fun1<param1>(1)");
            ShouldSucceed("fun1_<param1>(1)");
            ShouldSucceed("fun1<param1><param2>");
            ShouldSucceed("fun1_<param1>_<param2>");
            ShouldSucceed("fun1<param1><param2>(1)");
            ShouldSucceed("fun1_<param1>_<param2>(1)");
            ShouldSucceed("fun1_<param1>_<param2>(1)");
            ShouldSucceed("<parametrized3>n1(123)");
            ShouldSucceed("fu<parametrized3>(123)");
            ShouldSucceed("fu<parametrized<param>>(123)");
        }

        [TestMethod]
        public void Can_parse_expression_argument_calls()
        {
            ShouldSucceed("fun1(321)");
            ShouldSucceed("fun1(123,321)");
            ShouldSucceed("fun1(123,321,987)");

            ShouldSucceed("fun1(1+1)");
            ShouldSucceed("fun1(1+1+1)");

            ShouldSucceed("fun1((1+1),(2+2),(3+3))");

            ShouldSucceed("fun1(123,<fun2(2)>,123)");
            ShouldSucceed("fun1(<fun2(1)>,<fun3(1)>)");
        }

        [TestMethod]
        public void Can_parse_unquoted_argument_calls()
        {
            ShouldSucceed("fun1(many words separated by space)");
            ShouldSucceed("fun1(many words separated by space,and other words as second argument)");
            ShouldSucceed("fun1(many <fun1(1)> separated <fun2(2)> space,and <fun3(3)> words as <fun4(4)> argument)");
            ShouldSucceed("fun1(f_statistikapovolani(<arg(dny)>))");
            ShouldSucceed("fun1(text1<macro(1)>text2,text3<macro(2)>text4)");
        }

        protected void ShouldSucceed(string src)
        {
            Parse(src, parser =>
            {
                var statement = parser.statement();
            });
        }
    }
}
