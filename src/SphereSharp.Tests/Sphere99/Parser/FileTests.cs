using Antlr4.Runtime.Misc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Sphere99;
using System;
using System.IO;
using System.Text;

namespace SphereSharp.Tests.Sphere99.Parser
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
            CheckStructure("function fun1;typedef t_port_randomvicinty;eof;", @"[function fun1]//remark1
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
        public void Can_parse_file_with_section_headers_prefixed_with_whitespace()
        {
            CheckStructure("function fun1;", @"
     [function fun1]
call1");
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
        public void Can_parse_section_after_dialog_text_section()
        {
            CheckStructure("dialogText d_dialog;function fun1;eof;", @"[DIALOG d_dialog TEXT]
Jake ma byt tve druhe jmeno? 
(vybirej peclive, zustane ti naporad...)
OK

[function fun1]
call1

[eof]
");
        }

        [TestMethod]
        public void Can_parse_section_after_book_page_section()
        {
            CheckStructure("bookPage book_1 1;function fun1;eof;", @"[book book_1 1]
text1
text2

[function fun1]
call1

[eof]
");
        }

        [TestMethod]
        public void Can_handle_weird_problem_with_greedy_macro_grammer()
        {
            // the issue showed up only when both showbowtype_<src.tag(clienttype)> and [eof] were present
            // otherwise parsing was ok
            // the same problem with arg(x,<y>)
            CheckStructure("function fun1;eof;", @"[function fun1]
showbowtype_<src.tag(clienttype)>//important!!!
arg(x,<y>)//important!!!
x=<y>

[eof]//important!!!
");
        }

        [TestMethod]
        [DeploymentItem(@"Sphere99\Parser\example_test_file.scp", @"Sphere99\Parser")]
        public void Can_parse_example_script_file()
        {
            RoundtripCheck(File.ReadAllText(@"Sphere99\Parser\example_test_file.scp"));
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

            var roundtripGenerator = new RoundtripGenerator();
            roundtripGenerator.Visit(file);

            var srcWithoutEofTail = src.Substring(0, src.IndexOf("[eof]") + 5);
            roundtripGenerator.Output.Should().Be(srcWithoutEofTail);
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

            public override bool VisitTypeDefSection([NotNull] sphereScript99Parser.TypeDefSectionContext context)
            {
                output.Append($"typedef {context.typeDefSectionHeader().SYMBOL()};");

                return true;
            }

            public override bool VisitDialogTextSection([NotNull] sphereScript99Parser.DialogTextSectionContext context)
            {
                output.Append($"dialogText {context.dialogTextSectionHeader().dialogName.Text};");

                return true;
            }

            public override bool VisitBookPageSection([NotNull] sphereScript99Parser.BookPageSectionContext context)
            {
                output.Append($"bookPage {context.bookPageSectionHeader().bookName.Text} {context.bookPageSectionHeader().pageNumber.Text};");

                return true;
            }
        }
    }
}
