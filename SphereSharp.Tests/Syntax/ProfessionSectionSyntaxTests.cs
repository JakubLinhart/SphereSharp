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
    public class ProfessionSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_profession_with_properties_and_events()
        {
            var syntax = SectionSyntax.Parse(@"[profession 1]
DEFNAME=class_necro
NAME=Necro

on=@login
events e_character
events e_allplayers
").Should().BeOfType<ProfessionSectionSyntax>().Which;

            syntax.Id.Should().Be(1);
            syntax.Properties.Should().HaveCount(2);
            syntax.Triggers.Should().HaveCount(1);
            syntax.Triggers[0].CodeBlock.Statements.Should().HaveCount(2);
        }
    }
}
