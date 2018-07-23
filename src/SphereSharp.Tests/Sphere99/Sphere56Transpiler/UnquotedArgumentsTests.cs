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
    public class UnquotedArgumentsTests
    {
        [TestMethod]
        public void Unescaped_macro_with_function_call_without_arguments()
        {
            TranspileStatementCheck("sysmessage Some text <fun1>", "sysmessage Some text <fun1>");
        }

        [TestMethod]
        public void Unescaped_macro_with_function_call_with_arguments()
        {
            TranspileStatementCheck("sysmessage Some text <fun1(1,2,3)>", "sysmessage Some text <fun1 1,2,3>");
        }

        [TestMethod]
        public void Escaped_macro_with_function_call_with_arguments()
        {
            TranspileStatementCheck("sysmessage Some text <?fun1(1,2,3)?>", "sysmessage Some text <fun1 1,2,3>");
        }

        [TestMethod]
        public void Unescaped_macro_with_local_variable()
        {
            TranspileStatementCheck("sysmessage Some text <arg(x)>", "sysmessage Some text <local.x>");
        }

        [TestMethod]
        public void Escaped_macro_followed_by_text_without_html_tags()
        {
            TranspileStatementCheck("sysmessage Some text <?fun1?>some text without html tags", "sysmessage Some text <fun1>some text without html tags");
        }

        [TestMethod]
        public void Unescaped_macro_in_variable_assignment()
        {
            TranspileStatementCheck("arg(x,Some text <arg(x)>)", "local.x=Some text <local.x>");
        }

        [TestMethod]
        public void Escaped_macro_in_variable_assignment()
        {
            TranspileStatementCheck("arg(x,Some text <?arg(x)?>)", "local.x=Some text <local.x>");
        }

        [TestMethod]
        public void Ignores_variable_names()
        {
            TranspileFileCheck(@"[function fun1]
arg(sometext,1)
fun2(this is just sometext nothing more! no sometext variable replacement!)",
@"[function fun1]
local.sometext=1
fun2 this is just sometext nothing more! no sometext variable replacement!");
        }

        [TestMethod]
        public void Example_with_multiple_macros_end_numbers_delimited_by_whitespace()
        {
            TranspileStatementCheck(
                "serv.allclients(sendpacket(0c0 <hval(<argv(0)>)> <split4bytes(<xxx>)> <split2bytes(<nid(<argv(1)>)>)>))",
                "serv.allclients sendpacket 0c0 <hval(<argv[0]>)> <split4bytes <xxx>> <split2bytes <nid <argv[1]>>>");
        }
    }
}
