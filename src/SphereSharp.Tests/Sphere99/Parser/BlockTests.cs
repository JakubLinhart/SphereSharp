using Antlr4.Runtime.Misc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Sphere99.Parser
{
    [TestClass]
    public class BlockTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_empty_lines()
        {
            string expectedResult = "call:call1;call:call2;";
            CheckStatements(expectedResult, @"call1

call2
");
        }

        [TestMethod]
        public void Can_parse_with_comment_on_empty_lines()
        {
            string expectedResult = "call:call1;call:call2;";
            CheckStatements(expectedResult, @"call1
// comment 1
// comment 2
call2
");
        }

        [TestMethod]
        public void Can_parse_statement_with_leading_whitespace()
        {
            string expectedResult = "call:call1;call:call2;call:leadingtab;call:call3;";
            CheckStatements(expectedResult, @"call1
    call2
	leadingtab
call3");
        }

        [TestMethod]
        public void Can_parse_statement_with_leading_whitespace_ending_with_remarks()
        {
            string expectedResult = "call:call1;call:call2;call:leadingtab;call:call3;";
            CheckStatements(expectedResult, @"call1 // remark1
    call2//remark2
	leadingtab           //remark3
call3");
        }

        [TestMethod]
        public void Can_parse_all_statement_types()
        {
            string expectedResult = "call:call1;assignment:x=y;if:(1);";
            CheckStatements(expectedResult, @"call1
x=y
if (1)
    call2
endif");
        }

        public void CheckStatements(string expectedResult, string src)
        {
            var extractor = new StatementExtractor();

            Parse(src, parser =>
            {
                var block = parser.codeBlock();

                extractor.Visit(block);
            });

            extractor.Result.Should().Be(expectedResult);
        }

        private class StatementExtractor : sphereScript99BaseVisitor<bool>
        {
            private StringBuilder result = new StringBuilder();

            public string Result => result.ToString();

            private bool Process(string name, string text)
            {
                result.Append(name);
                result.Append(":");
                result.Append(text);
                result.Append(";");

                return true;
            }

            public override bool VisitIfStatement([NotNull] sphereScript99Parser.IfStatementContext context)
                => Process("if", context.evalExpression().GetText().Trim());

            public override bool VisitCall([NotNull] sphereScript99Parser.CallContext context)
                => Process("call", context.GetText());

            public override bool VisitAssignment([NotNull] sphereScript99Parser.AssignmentContext context)
                => Process("assignment", context.GetText());
        }
    }
}
