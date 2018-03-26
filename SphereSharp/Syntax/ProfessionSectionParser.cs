using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class ProfessionSectionParser
    {
        public static Parser<SectionSyntax> ParseProfession(string sectionType, string sectionName) =>
            from properties in PropertyParser.Property.AtLeastOnce()
            from triggers in TriggerParser.NamedTrigger.Many()
            select new ProfessionSectionSyntax(sectionType, sectionName, properties.ToImmutableArray(), triggers.ToImmutableArray());
    }
}
