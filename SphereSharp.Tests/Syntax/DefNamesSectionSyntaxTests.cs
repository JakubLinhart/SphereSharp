using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class DefNamesSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_defnames_with_LValue_and_RValue_separated_by_whitespace()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES colors_class]
color_war	1234
color_necro	6543
");

            syntax.Type.Should().Be("DEFNAMES");
            syntax.Name.Should().Be("colors_class");
            syntax.Should().BeOfType<DefNamesSectionSyntax>().Which.DefNames.Length.Should().Be(2);
            syntax.As<DefNamesSectionSyntax>().DefNames[0].LValue.Should().Be("color_war");
            syntax.As<DefNamesSectionSyntax>().DefNames[0].RValue.Should().Be("1234");
            syntax.As<DefNamesSectionSyntax>().DefNames[1].LValue.Should().Be("color_necro");
            syntax.As<DefNamesSectionSyntax>().DefNames[1].RValue.Should().Be("6543");
        }

        [TestMethod]
        public void Can_parse_defname_with_LValue_and_RValue_separated_by_assignment_operator()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES zaklad_skilly]
base_necro_Alchemy=300
base_necro_EI=200").Should().BeOfType<DefNamesSectionSyntax>().Which;

            syntax.DefNames.Should().HaveCount(2);
            syntax.DefNames[0].LValue.Should().Be("base_necro_Alchemy");
            syntax.DefNames[0].RValue.Should().Be("300");
            syntax.DefNames[1].LValue.Should().Be("base_necro_EI");
            syntax.DefNames[1].RValue.Should().Be("200");
        }

        [TestMethod]
        public void Can_parse_defnames_section_with_hex_numbers()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES colors_class]
color_war	0DEAD
color_necro	0BEEF
");

            syntax.Type.Should().Be("DEFNAMES");
            syntax.Name.Should().Be("colors_class");
            syntax.Should().BeOfType<DefNamesSectionSyntax>().Which.DefNames.Length.Should().Be(2);
            syntax.As<DefNamesSectionSyntax>().DefNames[0].LValue.Should().Be("color_war");
            syntax.As<DefNamesSectionSyntax>().DefNames[0].RValue.Should().Be("0DEAD");
            syntax.As<DefNamesSectionSyntax>().DefNames[1].LValue.Should().Be("color_necro");
            syntax.As<DefNamesSectionSyntax>().DefNames[1].RValue.Should().Be("0BEEF");
        }

        [TestMethod]
        public void Can_parse_defnames_section_with_comments()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES colors_class] // comment
// comment
color_war	1234 // comment
// comment
color_necro	6543
").Should().BeOfType<DefNamesSectionSyntax>().Which;

            syntax.Should().NotBeNull();
            syntax.Type.Should().Be("DEFNAMES");
            syntax.DefNames[0].LValue.Should().Be("color_war");
            syntax.DefNames[0].RValue.Should().Be("1234");
            syntax.DefNames[1].LValue.Should().Be("color_necro");
            syntax.DefNames[1].RValue.Should().Be("6543");
        }

        [TestMethod]
        public void Can_parse_defnames_section_with_empty_line_and_comment()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES colors_class]
color_war	1234
// comment

color_necro	6543
");

            syntax.Should().NotBeNull();
            syntax.Type.Should().Be("DEFNAMES");
            syntax.As<DefNamesSectionSyntax>().DefNames[0].LValue.Should().Be("color_war");
            syntax.As<DefNamesSectionSyntax>().DefNames[0].RValue.Should().Be("1234");
        }

        [TestMethod]
        public void Can_parse_indexed_defname()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES tituly]
titul_necro[0]	hrobar");

            syntax.Should().NotBeNull();
            syntax.Type.Should().Be("DEFNAMES");
            syntax.Name.Should().Be("tituly");

            syntax.Should().BeOfType<DefNamesSectionSyntax>().Which.DefNames.Should().HaveCount(1);
            syntax.As<DefNamesSectionSyntax>().DefNames[0].LValue.Should().Be("titul_necro[0]");
            syntax.As<DefNamesSectionSyntax>().DefNames[0].RValue.Should().Be("hrobar");
        }

        [TestMethod]
        public void Can_parse_literal_defname()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES nation_homes]
home_Gondoran	""5678 3871 -80""");


        }

        [TestMethod]
        public void Can_parse_defname_with_digit_in_lvalue()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES test]
name1   value1").Should().BeOfType<DefNamesSectionSyntax>().Which;

            syntax.DefNames.Should().HaveCount(1);
            syntax.DefNames[0].LValue.Should().Be("name1");
            syntax.DefNames[0].RValue.Should().Be("value1");
        }

        [TestMethod]
        public void Can_trim_trailing_whitespace()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES test]
name   value               ").Should().BeOfType<DefNamesSectionSyntax>().Which;

            syntax.DefNames.Should().HaveCount(1);
            syntax.DefNames[0].LValue.Should().Be("name");
            syntax.DefNames[0].RValue.Should().Be("value");
        }

        [TestMethod]
        public void Can_parse_defname_with_indexed_lvalue()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES test]
def_class[1]	Mag").Should().BeOfType<DefNamesSectionSyntax>().Which;

            syntax.DefNames.Should().HaveCount(1);
            syntax.DefNames[0].LValue.Should().Be("def_class[1]");
            syntax.DefNames[0].RValue.Should().Be("Mag");
        }

        [TestMethod]
        public void Can_parse_defname_with_spaces_in_section_name()
        {
            var syntax = SectionSyntax.Parse(@"[DEFNAMES classes to take]
lvalue	rvalue").Should().BeOfType<DefNamesSectionSyntax>().Which;

            syntax.Name.Should().Be("classes to take");
        }
    }
}
