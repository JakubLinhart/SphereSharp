using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var extractor = new StatementExtractor();
                extractor.Visit(block);
            });
        }
    }
}
