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
    public class WhileTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_while()
        {
            CheckStructure("while(2);endwhile", @"while (1==1)
    call1
    call2
endwhile");
        }

        [TestMethod]
        public void Can_parse_empty_while()
        {
            CheckStructure("while(0);endwhile", @"while (1==1)
endwhile");
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
