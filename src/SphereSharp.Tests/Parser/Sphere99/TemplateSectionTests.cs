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
    public class TemplateSectionTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_Template_section_with_property()
        {
            CheckStructure("props:1;", @"[template tmp_ingots]
container=i_pouch
");
        }

        private void CheckStructure(string expectedResult, string src)
        {
            sphereScript99Parser.TemplateSectionContext itemDef = null;

            Parse(src, parser =>
            {
                itemDef = parser.templateSection();
            });

            var extractor = new PropertyAndTriggerExtractor();
            extractor.Visit(itemDef);

            extractor.Output.Should().Be(expectedResult);
        }
    }
}
