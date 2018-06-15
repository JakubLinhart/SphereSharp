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
    public class SaveFileSectionTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_WorldItem_section_with_uid_name()
        {
            CheckSaveStructure("props:1;",
@"[WorldItem 01b]
Serial=040061260");
        }

        [TestMethod]
        public void Can_parse_WorldChar_section_with_uid_name()
        {
            CheckSaveStructure("props:1;",
@"[WorldChar #08e000122]
Serial=07F708");
        }

        private void CheckSaveStructure(string expectedResult, string src)
        {
            sphereScript99Parser.SaveFileSectionContext section = null;

            Parse(src, parser =>
            {
                section = parser.saveFileSection();
            });

            var extractor = new PropertyAndTriggerExtractor();
            extractor.Visit(section);

            extractor.Output.Should().Be(expectedResult);
        }

    }
}
