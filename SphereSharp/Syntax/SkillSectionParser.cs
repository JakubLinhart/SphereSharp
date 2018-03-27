using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class SkillSectionParser
    {
        public static Parser<SectionSyntax> ParseSkillDef(string sectionType, string sectionName) =>
            from properties in PropertyParser.Property.AtLeastOnce()
            from triggers in TriggerParser.NamedTrigger.Many()
            select new SkillSectionSyntax(sectionType, sectionName, properties.ToImmutableArray(), triggers.ToImmutableArray());
    }
}
