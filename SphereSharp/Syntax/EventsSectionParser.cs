using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class EventsSectionParser
    {
        public static Parser<SectionSyntax> ParseEvents(string sectionType, string sectionName) =>
            from triggers in TriggerParser.NamedTrigger.Many()
            select new EventsSectionSyntax(sectionType, sectionName, triggers.ToImmutableArray());
    }
}
