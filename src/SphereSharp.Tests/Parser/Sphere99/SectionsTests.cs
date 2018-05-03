﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class SectionsTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_itemdef_section_with_properties_and_triggers()
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
        public void Can_parse_itemdef_without_triggers()
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

        [TestMethod]
        public void Can_parse_events_section()
        {
            CheckStructure("triggers:2;", @"[events e_something]
on=@step
  call1

on=@timer
  call2
");
        }

        [TestMethod]
        public void Can_parse_chardef_section()
        {
            CheckStructure("props:2;triggers:1;", @"[chardef c_dummy]
id=c_man
name=dummy

on=@create
flag_insubstantial=1
");
        }

        [TestMethod]
        public void Can_parse_template_section_with_property()
        {
            CheckStructure("props:1;", @"[template tmp_ingots]
container=i_pouch
");
        }

        [TestMethod]
        public void Can_parse_TypeDef_section_with_triggers()
        {
            CheckStructure("triggers:2;", @"[typedef t_port_randomvicinty]
on=@step
  call1

on=@timer
  call2
");
        }

        private void CheckStructure(string expectedResult, string src)
        {
            sphereScript99Parser.SectionContext section = null;

            Parse(src, parser =>
            {
                section = parser.section();
            });

            var extractor = new PropertyAndTriggerExtractor();
            extractor.Visit(section);

            extractor.Output.Should().Be(expectedResult);
        }
    }
}