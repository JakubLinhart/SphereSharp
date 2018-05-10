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
    public class DoRandTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_dorand_with_one_statement()
        {
            CheckStructure("dorand(1);enddo;", @"dorand 1+1
    call1
enddo");
        }

        [TestMethod]
        public void Can_parse_dorand_with_multiple_statements()
        {
            CheckStructure("dorand(3);enddo;", @"dorand 1+1
    call1
    call2
    call3
enddo");
        }

        [TestMethod]
        public void Can_parse_indented_dorand()
        {
            CheckStructure("dorand(2);enddo;", @" dorand 1+1
        call1
        call2
    enddo");
        }

        private void CheckStructure(string expectedResult, string src)
        {
            Parse(src, parser =>
            {
                var block = parser.statement();
                var extractor = new StatementExtractor();
                extractor.Visit(block);

                extractor.Result.Should().Be(expectedResult);
            });
        }
    }
}
