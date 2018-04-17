using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class TypeDefSectionParser
    {
        public static Parser<SectionSyntax> ParseTypeDef(string sectionType, string sectionName) =>
            from triggers in TriggerParser.Trigger.AtLeastOnce()
            select new TypeDefSectionSyntax(sectionType, sectionName, triggers);
    }
}
