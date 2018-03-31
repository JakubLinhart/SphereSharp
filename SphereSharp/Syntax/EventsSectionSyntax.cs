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
    }
}
