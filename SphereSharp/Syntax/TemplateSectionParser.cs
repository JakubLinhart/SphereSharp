using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class TemplateSectionParser
    {
        public static Parser<SectionSyntax> ParseTemplate(string sectionType, string sectionName) =>
            from properties in PropertyParser.Property.AtLeastOnce()
            select new TemplateSectionSyntax(sectionType, sectionName, properties.ToImmutableArray());
    }
}
