using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class FileSyntaxTests
    {
        [TestMethod]
        public void Can_parse_multiple_sections_with_comments()
        {
            var syntax = FileSyntax.Parse("myfile.scp", @"
// comment1
[ITEMDEF I_stone_raceclass]
NAME=Stone of the Beginning

on=@userdclick
return 1

// comment2
[defnames basestats]
basestats_mag_str_min		50
// comment3

[DIALOG D_RACEclass_background] // comment4
argo.SetLocation(0,0) // comment 5
");

            syntax.Sections.Should().HaveCount(3);
            syntax.Sections[0].Should().BeOfType<ItemDefSectionSyntax>();
            syntax.Sections[1].Should().BeOfType<DefNamesSectionSyntax>();
            syntax.Sections[2].Should().BeOfType<DialogSectionSyntax>();
        }

        [TestMethod]
        public void Can_parse_two_consecutive_defnames_sections()
        {
            var syntax = FileSyntax.Parse("myfile.scp", @"
[defnames basestatsA]
basestats_mag_str_minA		50

[defnames basestatsB]
basestats_mag_str_minB		50
");

            syntax.Sections.Should().HaveCount(2);
            syntax.Sections[0].Should().BeOfType<DefNamesSectionSyntax>();
            syntax.Sections[1].Should().BeOfType<DefNamesSectionSyntax>();
        }

        [TestMethod]
        public void Can_parse_section_with_space_after_header()
        {
            var syntax = FileSyntax.Parse("myfile.scp", @"
[defnames basestatsA] 
basestats_mag_str_minA		50

[defnames basestatsB]
basestats_mag_str_minB		50
");

            syntax.Sections.Should().HaveCount(2);
            syntax.Sections[0].Should().BeOfType<DefNamesSectionSyntax>();
            syntax.Sections[1].Should().BeOfType<DefNamesSectionSyntax>();
        }

        [TestMethod]
        public void Can_parse_complex_file()
        {
            var syntax = FileSyntax.Parse("tests.scp", @"
[DIALOG D_RACEclass_backgroundA] // comment4
argo.SetLocation(0,0) // comment 5

[DIALOG D_RACEclass_backgroundA TEXT]
<BASEFONT SIZE=""+5"" COLOR=""#000080""><CENTER>Vitej na Morii!<br><br>Vesel jsi do tohoto sveta,<br>je cas Volby. Tvuj osud Te ocekava.<br>Vybirej moudre, a mozna se na tebe usmeje stesti.<br><br>Toto je kniha Pravdy.<br>Vyber si zde na jake strane zacnes,<br>kde skoncis je na Tobe.</CENTER></BASEFONT>

[ITEMDEF I_stone_raceclass]
NAME=Stone of the Beginning

on=@userdclick
return 1

[defnames basestatsA]
basestats_mag_str_minA		50

[function test]
call1

[DIALOG D_RACEclass_backgroundB] // comment4
argo.SetLocation(0,0) // comment 5

[DIALOG D_RACEclass_backgroundB TEXT]
<BASEFONT SIZE=""+5"" COLOR=""#000080""><CENTER>Vitej na Morii!<br><br>Vesel jsi do tohoto sveta,<br>je cas Volby. Tvuj osud Te ocekava.<br>Vybirej moudre, a mozna se na tebe usmeje stesti.<br><br>Toto je kniha Pravdy.<br>Vyber si zde na jake strane zacnes,<br>kde skoncis je na Tobe.</CENTER></BASEFONT>

[EOF]
");

            syntax.Sections.Should().HaveCount(8);
        }

        [TestMethod]
        public void Fails_when_parsing_invalid_statement()
        {
            var testedAction = (Action)(() => FileSyntax.Parse("test", @"[FUNCTION dialogclose]
@!#$bullshit"));

            testedAction.Should().Throw<Exception>();
        }
    }
}
