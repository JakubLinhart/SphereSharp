using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class ItemDefSectionParser
    {
        public static Parser<SectionSyntax> ParseItemDef(string sectionType, string sectionName) =>
            from properties in PropertyParser.Property.AtLeastOnce()
            from triggers in TriggerParser.NamedTrigger.Many()
            select new ItemDefSectionSyntax(sectionType, sectionName, properties.ToImmutableArray(), triggers.ToImmutableArray());
    }
}
