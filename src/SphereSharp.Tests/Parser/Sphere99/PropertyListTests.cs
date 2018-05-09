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
    public class PropertyListTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_property_list()
        {
            CheckStructure("props:3;", @"prop1=value1
prop2=value2
prop3=value3");
        }

        [TestMethod]
        public void Can_parse_property_list_with_whitespace_between_assign_operator()
        {
            CheckStructure("props:3;", @"prop1 = value1
prop2=  value2
prop3  =value3");
        }

        [TestMethod]
        public void Can_parse_property_list_with_empty_lines_and_comments()
        {
            CheckStructure("props:3;", @"
// first line is empty
prop1=value1//comment right after property
prop2=value2 // comment with whitespace

// empty line
prop3=value3");
        }

        [TestMethod]
        public void Can_parse_property_with_unquoted_literal_as_value()
        {
            CheckStructure("props:3;", @"prop1=this can contain anything - as unquoted literal
prop2=value2
prop3=value3");
        }

        [TestMethod]
        public void Can_parse_property_with_special_chars()
        {
            CheckStructure("props:1;", @"prop1=@");
        }

        private void CheckStructure(string expectedResult, string src)
        {
            sphereScript99Parser.PropertyListContext propertyList = null;

            Parse(src, parser =>
            {
                propertyList = parser.propertyList();
            });

            var extractor = new PropertyAndTriggerExtractor();
            extractor.Visit(propertyList);

            extractor.Output.Should().Be(expectedResult);
        }
    }
}
