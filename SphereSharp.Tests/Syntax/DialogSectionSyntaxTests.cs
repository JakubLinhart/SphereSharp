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
    public class DialogSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_dialog_section()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_background]
argo.SetLocation(0,0)
resizepic 0 0 2600 640 480
gumppic 510 110 5536
gumppic 35 110 5536
HTMLGUMP(20,20,600,200,0,0,0)
").Should().BeOfType<DialogSectionSyntax>().Which;

            syntax.InitCodeBlock.Statements.Should().HaveCount(5);
        }
    }
}
