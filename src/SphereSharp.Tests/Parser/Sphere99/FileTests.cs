using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class FileTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_file_with_all_section_types_and_eof_section()
        {
            ShouldSucceed(@"[function fun1]
call1

[eof]");
        }

        [TestMethod]
        public void Can_parse_file_with_some_section_without_eof_section()
        {
            ShouldSucceed(@"[function fun1]
call1");
        }

        [TestMethod]
        [DeploymentItem(@"Parser\Sphere99\example_test_file.scp", @"Parser\Sphere99")]
        public void Can_parse_example_script_file()
        {
            ShouldSucceed(File.ReadAllText(@"Parser\Sphere99\example_test_file.scp"));
        }

        private void ShouldSucceed(string src) => Parse(src, parser =>
        {
            var x = parser.file();
        });
    }
}
