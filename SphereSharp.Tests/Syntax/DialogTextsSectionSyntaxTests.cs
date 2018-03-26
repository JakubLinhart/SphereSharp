using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class DialogTextsSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_one_by_default_safe_literal()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_background TEXT]
<BASEFONT SIZE=""+5"" COLOR=""#000080""><CENTER>Vitej na Morii!<br><br>Vesel jsi do tohoto sveta,<br>je cas Volby. Tvuj osud Te ocekava.<br>Vybirej moudre, a mozna se na tebe usmeje stesti.<br><br>Toto je kniha Pravdy.<br>Vyber si zde na jake strane zacnes,<br>kde skoncis je na Tobe.</CENTER></BASEFONT>
").Should().BeOfType<DialogTextsSectionSyntax>().Which;

            syntax.Texts.Should().HaveCount(1);
        }

        [TestMethod]
        public void Skips_empty_lines()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_background TEXT]

<BASEFONT SIZE=""+5"" COLOR=""#000080""><CENTER>Vitej na Morii!<br><br>Vesel jsi do tohoto sveta,<br>je cas Volby. Tvuj osud Te ocekava.<br>Vybirej moudre, a mozna se na tebe usmeje stesti.<br><br>Toto je kniha Pravdy.<br>Vyber si zde na jake strane zacnes,<br>kde skoncis je na Tobe.</CENTER></BASEFONT>

").Should().BeOfType<DialogTextsSectionSyntax>().Which;

            syntax.Texts.Should().HaveCount(1);
        }

        [TestMethod]
        public void Terminates_at_section_start()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_background TEXT]
<BASEFONT SIZE=""+5"" COLOR=""#000080""><CENTER>Vitej na Morii!<br><br>Vesel jsi do tohoto sveta,<br>je cas Volby. Tvuj osud Te ocekava.<br>Vybirej moudre, a mozna se na tebe usmeje stesti.<br><br>Toto je kniha Pravdy.<br>Vyber si zde na jake strane zacnes,<br>kde skoncis je na Tobe.</CENTER></BASEFONT>

[dialog d_other_dialog]
").Should().BeOfType<DialogTextsSectionSyntax>().Which;

            syntax.Texts.Should().HaveCount(1);
        }
    }
}
