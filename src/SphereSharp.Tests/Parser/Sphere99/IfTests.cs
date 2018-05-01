using Antlr4.Runtime.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class IfTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_if_without_else()
        {
            CheckStructure("if(1);endif", @"if 1
    call1
endif");
        }

        [TestMethod]
        public void Can_parse_if_with_else()
        {
            CheckStructure("if(1);else(1);endif;", @"if 1
    call1
else
    call2
endif");
        }

        [TestMethod]
        public void Can_parse_one_elseif_without_else()
        {
            CheckStructure("if(1);elseif(1);endif;", @"if 1
    call1
elseif 2
    call2
endif");
        }

        [TestMethod]
        public void Can_parse_one_elseif_with_else()
        {
            CheckStructure("if(1);elseif(1);else(1);endif;", @"if 1
    call1
elseif 2
    call2
else
    call3
endif");
        }

        [TestMethod]
        public void Can_parse_three_elseif_without_else()
        {
            CheckStructure("if(1);elseif(1);elseif(1);elseif(1);endif;", @"if 1
    call1
elseif 2
    call2
elseif 3
    call3
elseif 4
    call4
endif");
        }

        [TestMethod]
        public void Can_parse_three_elseif_with_else()
        {
            CheckStructure("if(1);elseif(1);elseif(1);elseif(1);else(1);endif;", @"if 1
    call1
elseif 2
    call2
elseif 3
    call3
elseif 4
    call4
else
    call3
endif");
        }

        [TestMethod]
        public void Can_parse_empty_if()
        {
            CheckStructure("if(0);else(1);endif;", @"if 1
else
    call1
endif");
        }

        [TestMethod]
        public void Can_parse_empty_if_with_comment()
        {
            CheckStructure("if(1);else(1);endif;", @"if 1
    // call1
else
    call2
endif");
        }

        [TestMethod]
        public void Can_parse_empty_elseif()
        {
            CheckStructure("if(1);elseif(0);else(1);endif;", @"if 1
    call1
elseif 2
else
    call2
endif");
        }

        [TestMethod]
        public void Can_parse_empty_elseif_with_comment()
        {
            CheckStructure("if(1);elseif(0);else(1);endif;", @"if 1
    call1
elseif 2
    // call2
else
    call3
endif");
        }

        [TestMethod]
        public void Can_parse_empty_else()
        {
            CheckStructure("if(1);else(0);endif;", @"if 1
    call1
else
endif");
        }

        [TestMethod]
        public void Can_parse_empty_else_with_comment()
        {
            CheckStructure("if(1);else(0);endif;", @"if 1
    call1
else
    // call2
endif");
        }

        private void ShouldSucceed(string src) => Parse(src, parser => parser.ifStatement());

        private void CheckStructure(string expectedResult, string src)
        {
            Parse(src, parser =>
            {
                var block = parser.ifStatement();
                var extractor = new IfExtractor();
                extractor.Visit(block);
            });
        }

        private class IfExtractor : sphereScript99BaseVisitor<bool>
        {
            private StringBuilder result = new StringBuilder();
            public string Result => result.ToString();

            public override bool VisitIfStatement([NotNull] sphereScript99Parser.IfStatementContext context)
            {
                if (context.codeBlock() != null)
                    result.Append($"if({context.codeBlock().statement().Length});");
                else
                    result.Append($"if(0);");

                base.VisitIfStatement(context);
                result.Append("endif;");

                return false;
            }

            public override bool VisitElseStatement([NotNull] sphereScript99Parser.ElseStatementContext context)
            {
                if (context.codeBlock() != null)
                    result.Append($"else({context.codeBlock().statement().Length});");
                else
                    result.Append($"else(0);");

                return false;
            }

            public override bool VisitElseIfStatement([NotNull] sphereScript99Parser.ElseIfStatementContext context)
            {
                if (context.codeBlock() != null)
                    result.Append($"elseif({context.codeBlock().statement().Length});");
                else
                    result.Append($"elseif(0);");

                return false;
            }
        }
    }
}
