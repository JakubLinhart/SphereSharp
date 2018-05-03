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
    public class DoSwitchTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_doswitch_with_one_statement()
        {
            CheckStructure("doswitch(1);enddo;", @"doswitch 1+1
    call1
enddo");
        }

        [TestMethod]
        public void Can_parse_doswitch_with_multiple_statements()
        {
            CheckStructure("doswitch(3);enddo;", @"doswitch 1+1
    call1
    call2
    call3
enddo");
        }

        [TestMethod]
        public void Can_parse_indented_doswitch()
        {
            CheckStructure("doswitch(3);enddo;", @" doswitch 1+1
        call1
        call2
        call3
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
