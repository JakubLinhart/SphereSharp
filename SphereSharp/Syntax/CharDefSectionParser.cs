using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class CharDefSectionParser
    {
        public static Parser<SectionSyntax> ParseCharDef(string sectionType, string sectionName) =>
            from properties in PropertyParser.Property.AtLeastOnce()
            from triggers in TriggerParser.NamedTrigger.Many()
            select new CharDefSectionSyntax(sectionType, sectionName, properties.ToImmutableArray(), triggers.ToImmutableArray());
    }
}
