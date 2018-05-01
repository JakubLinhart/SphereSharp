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
            CheckStructure("while(1);endwhile", @"while (1)
    call1
endwhile");
        }

        private void CheckStructure(string expectedResult, string src)
        {
            Parse(src, parser =>
            {
                var block = parser.statement();
                var extractor = new StatementExtractor();
                extractor.Visit(block);
            });
        }
    }
}
