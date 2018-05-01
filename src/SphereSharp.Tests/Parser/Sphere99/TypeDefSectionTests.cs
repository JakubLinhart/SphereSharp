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
    public class TypeDefSectionTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_TypeDef_section_with_triggers()
        {
            StructureCheck("triggers:2;", @"[typedef t_port_randomvicinty]
on=@step
  call1

on=@timer
  call2
");
        }

        private void StructureCheck(string expectedResult, string src)
        {
            sphereScript99Parser.TypeDefSectionContext itemDef = null;

            Parse(src, parser =>
            {
                itemDef = parser.typeDefSection();
            });

            var extractor = new PropertyAndTriggerExtractor();
            extractor.Visit(itemDef);

            extractor.Output.Should().Be(expectedResult);
        }
    }
}
