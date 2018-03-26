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
    public class DialogButtonsSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_numbered_trigger()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_classes Button]
on=0
dialogclose(D_RACEclass_background,0)").Should().BeOfType<DialogButtonsSectionSyntax>().Which;

            syntax.Triggers.Should().HaveCount(1);
            syntax.Triggers[0].Name.Should().Be("0");
            syntax.Triggers[0].CodeBlock.Statements.Should().HaveCount(1);
        }
        [TestMethod]
        public void Can_parse_named_trigger()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_classes Button]
on=@anybutton
dialogclose(D_RACEclass_background,0)").Should().BeOfType<DialogButtonsSectionSyntax>().Which;

            syntax.Triggers.Should().HaveCount(1);
            syntax.Triggers[0].Name.Should().Be("anybutton");
            syntax.Triggers[0].CodeBlock.Statements.Should().HaveCount(1);

        }

        [TestMethod]
        public void Can_parse_mutliple_triggers()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_classes Button]
on=0
dialogclose(D_RACEclass_background,0)

on=@anybutton
dialogclose(D_RACEclass_background,0)").Should().BeOfType<DialogButtonsSectionSyntax>().Which;

            syntax.Triggers.Should().HaveCount(2);
            syntax.Triggers[0].Name.Should().Be("0");
            syntax.Triggers[0].CodeBlock.Statements.Should().HaveCount(1);
            syntax.Triggers[1].Name.Should().Be("anybutton");
            syntax.Triggers[1].CodeBlock.Statements.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_mutliple_numbered_triggers()
        {
            var syntax = SectionSyntax.Parse(@"[DIALOG D_RACEclass_classes Button]
on=0
dialogclose(D_RACEclass_background,0)

on=1
dialogclose(D_RACEclass_background,0)").Should().BeOfType<DialogButtonsSectionSyntax>().Which;

            syntax.Triggers.Should().HaveCount(2);
            syntax.Triggers[0].Name.Should().Be("0");
            syntax.Triggers[0].CodeBlock.Statements.Should().HaveCount(1);
            syntax.Triggers[1].Name.Should().Be("1");
            syntax.Triggers[1].CodeBlock.Statements.Should().HaveCount(1);
        }
    }
}
