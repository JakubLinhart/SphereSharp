using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public partial class ItemDefSectionTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_ItemDef_section_with_properties_and_triggers()
        {
            CheckStructure("props:3;triggers:3;", @"[itemdef i_dclickME]
name=vain dclicker
id=i_memory
type=t_script

on=@create
attr=010
timer=1

on=@UserDClick
return 1

on=@timer
remove");
        }

        [TestMethod]
        public void Can_parse_ItemDef_without_triggers()
        {
            CheckStructure("props:3;triggers:0;", @"[itemdef i_dclickME]
name=vain dclicker
id=i_memory
type=t_script");
        }

        [TestMethod]
        public void Can_parse_ItemDef_with_comments_and_empty_lines_in_property_list()
        {
            CheckStructure("props:3;triggers:0;", @"[itemdef i_dclickME] // comment after header
// comment as the first property line
name=vain dclicker // comment after property

// empty line before this line is important for this test
id=i_memory
type=t_script");
        }

        private void CheckStructure(string expectedResult, string src)
        {
            sphereScript99Parser.ItemDefSectionContext itemDef = null;

            Parse(src, parser =>
            {
                itemDef = parser.itemDefSection();
            });

            var extractor = new PropertyAndTriggerExtractor();
            extractor.Visit(itemDef);

            extractor.Output.Should().Be(expectedResult);
        }
    }
}
