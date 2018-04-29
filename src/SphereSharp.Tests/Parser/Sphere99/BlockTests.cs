using Antlr4.Runtime.Misc;
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
        public void Can_parse_all_statement_types()
        {
            string expectedResult = "call:call1;assignment:x=y;";
            CheckStatements(expectedResult, @"call1
x=y");
        }

        public void CheckStatements(string expectedResult, string src)
        {
            Parse(src, parser =>
            {
                var block = parser.codeBlock();

                var extractor = new StatementExtractor();
                extractor.Visit(block);

                extractor.Result.Should().Be(expectedResult);
            });
        }

        private class StatementExtractor : sphereScript99BaseVisitor<bool>
        {
            private StringBuilder result = new StringBuilder();

            public string Result => result.ToString();

            public override bool VisitCall([NotNull] sphereScript99Parser.CallContext context)
            {
                result.Append("call:");
                result.Append(context.GetText());
                result.Append(";");

                return true;
            }

            public override bool VisitAssignment([NotNull] sphereScript99Parser.AssignmentContext context)
            {
                result.Append("assignment:");
                result.Append(context.GetText());
                result.Append(";");

                return true;
            }
        }
    }
}
