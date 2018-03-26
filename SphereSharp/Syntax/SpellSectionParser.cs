using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class SpellSectionParser
    {
        public static Parser<SectionSyntax> ParseSpell(string sectionType, string sectionName) =>
            from properties in PropertyParser.Property.AtLeastOnce()
            select new SpellSectionSyntax(sectionType, sectionName, properties.ToImmutableArray());
    }
}
