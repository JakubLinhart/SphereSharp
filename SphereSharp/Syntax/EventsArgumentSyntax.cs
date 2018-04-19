using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public class EventsArgumentSyntax : ArgumentSyntax
    {
        public EventsArgumentSyntax(string name, EventsOperationKind kind)
        {
            EventName = name;
            Kind = kind;
        }

        public string EventName { get; }
        public EventsOperationKind Kind { get; }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitEventsArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
