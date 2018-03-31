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
    public class EventsStatementSyntaxTests
    {
        [TestMethod]
        public void Can_parse_events_subscription()
        {
            var syntax = EventsStatementSyntax.Parse("events +e_meditation");

            syntax.EventName.Should().Be("e_meditation");
            syntax.Kind.Should().Be(EventsOperationKind.Subscribe);
        }

        [TestMethod]
        public void Can_parse_events_subscription_as_assignement()
        {
            var syntax = EventsStatementSyntax.Parse("events=+e_meditation");

            syntax.EventName.Should().Be("e_meditation");
            syntax.Kind.Should().Be(EventsOperationKind.Subscribe);
        }

        [TestMethod]
        public void Can_parse_events_subscription_without_sign()
        {
            var syntax = EventsStatementSyntax.Parse("events e_meditation");

            syntax.EventName.Should().Be("e_meditation");
            syntax.Kind.Should().Be(EventsOperationKind.Subscribe);
        }

        [TestMethod]
        public void Can_parse_events_unsubscription()
        {
            var syntax = EventsStatementSyntax.Parse("events -e_meditation");

            syntax.EventName.Should().Be("e_meditation");
            syntax.Kind.Should().Be(EventsOperationKind.Unsubscribe);
        }

        [TestMethod]
        public void Can_parse_events_unsubscription_as_assignment()
        {
            var syntax = EventsStatementSyntax.Parse("events=-e_meditation");

            syntax.EventName.Should().Be("e_meditation");
            syntax.Kind.Should().Be(EventsOperationKind.Unsubscribe);
        }
    }
}
