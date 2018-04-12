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
    public class EventsSectionSyntaxTests
    {
        [TestMethod]
        public void Can_parse_events_with_multiple_triggers()
        {
            var syntax = SectionSyntax.Parse(@"[events e_meditation]
on=@triggA
call1

on=@triggB
call2
").Should().BeOfType<EventsSectionSyntax>().Which;

            syntax.Name.Should().Be("e_meditation");
            syntax.Triggers.Should().HaveCount(2);
            syntax.Triggers[0].Name.Should().Be("triggA");
            syntax.Triggers[1].Name.Should().Be("triggB");
        }

        [TestMethod]
        public void Can_parse_triggers_with_empty_bodies()
        {
            var syntax = SectionSyntax.Parse(@"[events e_meditation]
on=@triggA

on=@triggB

").Should().BeOfType<EventsSectionSyntax>().Which;

            syntax.Name.Should().Be("e_meditation");
            syntax.Triggers.Should().HaveCount(2);
            syntax.Triggers[0].Name.Should().Be("triggA");
            syntax.Triggers[1].Name.Should().Be("triggB");
        }
    }
}
