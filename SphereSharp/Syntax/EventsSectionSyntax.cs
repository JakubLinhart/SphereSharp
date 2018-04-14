using System.Collections.Generic;
using System.Collections.Immutable;

namespace SphereSharp.Syntax
{
    public class EventsSectionSyntax : SectionSyntax
    {
        public ImmutableArray<TriggerSyntax> Triggers { get; }

        public EventsSectionSyntax(string type, string name, ImmutableArray<TriggerSyntax> triggers)
            : base(type, name, null)
        {
            Triggers = triggers;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitEventsSection(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            return Triggers;
        }
    }
}
