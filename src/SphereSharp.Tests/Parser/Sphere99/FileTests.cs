using Antlr4.Runtime.Misc;
using FluentAssertions;
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
            CheckStructure("function fun1;eof;", @"[function fun1]
call1

[eof]");
        }

        [TestMethod]
        public void Can_parse_file_with_all_section_types_and_eof_section_ending_with_remarks()
        {
            CheckStructure("function fun1;eof;", @"[function fun1]//remark1
call1

[itemdef i_dclickME]
name=vain dclicker
id=i_memory
type=t_script

on=@create
attr=010

[typedef t_port_randomvicinty]
on=@step
    call1

[template tmp_ingots]
container=i_pouch

[eof]//remark2");
        }

        [TestMethod]
        public void Can_parse_file_with_comments_at_beginning_and_end()
        {
            CheckStructure("function fun1;eof;", @"//remark1
//remark2
[function fun1]
call1

[eof]
//remark3
//remark4");
        }

        [TestMethod]
        public void Can_parse_file_with_empty_lines_at_beginning_and_end()
        {
            CheckStructure("function fun1;eof;", @"

[function fun1]
call1

[eof]


");
        }

        [TestMethod]
        public void Can_parse_file_with_some_section_without_eof_section()
        {
            CheckStructure("function fun1;", @"[function fun1]
call1");
        }

        [TestMethod]
        [DeploymentItem(@"Parser\Sphere99\example_test_file.scp", @"Parser\Sphere99")]
        public void Can_parse_example_script_file()
        {
            RoundtripCheck(File.ReadAllText(@"Parser\Sphere99\example_test_file.scp"));
        }

        private void ShouldSucceed(string src) => Parse(src, parser =>
        {
            var x = parser.file();
        });

        private void RoundtripCheck(string src)
        {
            sphereScript99Parser.FileContext file = null;
            Parse(src, parser =>
            {
                file = parser.file();
            });

            var roundtripGenerator = new Sphere99RoundtripGenerator();
            roundtripGenerator.Visit(file);

            roundtripGenerator.Output.Should().Be(src);
        }

        private void CheckStructure(string expectedResult, string src)
        {
            Parse(src, parser =>
            {
                var file = parser.file();
                var extractor = new SectionExtractor();
                extractor.Visit(file);
                extractor.Outpout.Should().Be(expectedResult);
            });
        }

        private class SectionExtractor : sphereScript99BaseVisitor<bool>
        {
            private StringBuilder output = new StringBuilder();
            public string Outpout => output.ToString();

            public override bool VisitEofSection([NotNull] sphereScript99Parser.EofSectionContext context)
            {
                output.Append("eof;");

                return true;
            }

            public override bool VisitFunctionSection([NotNull] sphereScript99Parser.FunctionSectionContext context)
            {
                output.Append($"function {context.functionSectionHeader().SYMBOL()};");

                return true;
            }
        }
    }
}
