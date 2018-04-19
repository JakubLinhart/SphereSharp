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
    public class EventsTests
    {
        [TestMethod]
        public void Can_parse_events_subscription()
        {
            var syntax = CallSyntax.Parse("events +e_meditation");

            syntax.Arguments.Arguments.Should().HaveCount(1);
            var argument = syntax.Arguments.Arguments[0].Should().BeOfType<EventsArgumentSyntax>().Which;
            argument.Kind.Should().Be(EventsOperationKind.Subscribe);
            argument.EventName.Should().Be("e_meditation");
        }

        [TestMethod]
        public void Can_parse_events_subscription_without_sign()
        {
            var syntax = CallSyntax.Parse("events e_meditation");

            syntax.Arguments.Arguments.Should().HaveCount(1);
            var argument = syntax.Arguments.Arguments[0].Should().BeOfType<EventsArgumentSyntax>().Which;
            argument.Kind.Should().Be(EventsOperationKind.Subscribe);
            argument.EventName.Should().Be("e_meditation");
        }

        [TestMethod]
        public void Can_parse_events_unsubscription()
        {
            var syntax = CallSyntax.Parse("events -e_meditation");

            syntax.Arguments.Arguments.Should().HaveCount(1);
            var argument = syntax.Arguments.Arguments[0].Should().BeOfType<EventsArgumentSyntax>().Which;
            argument.Kind.Should().Be(EventsOperationKind.Unsubscribe);
            argument.EventName.Should().Be("e_meditation");
        }

        [TestMethod]
        public void Can_parse_chained_events_subscription()
        {
            var syntax = CallSyntax.Parse("src.events +e_meditation");
            syntax.ChainedCall.Should().NotBeNull();

            var chainedCall = syntax.ChainedCall;
            chainedCall.Arguments.Arguments.Should().HaveCount(1);
            var argument = chainedCall.Arguments.Arguments[0].Should().BeOfType<EventsArgumentSyntax>().Which;
            argument.Kind.Should().Be(EventsOperationKind.Subscribe);
            argument.EventName.Should().Be("e_meditation");
        }

        [TestMethod]
        public void Can_parse_events_subscription_as_assignement()
        {
            var syntax = AssignmentSyntax.Parse("events=+e_meditation");

            syntax.LValue.MemberName.Should().Be("events");
            syntax.RValue.Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("+e_meditation");
        }

        [TestMethod]
        public void Can_parse_events_unsubscription_as_assignment()
        {
            var syntax = AssignmentSyntax.Parse("events=-e_meditation");

            syntax.LValue.MemberName.Should().Be("events");
            syntax.RValue.Should().BeOfType<LiteralArgumentSyntax>().Which.Literal.Text.Should().Be("-e_meditation");
        }
    }
}
