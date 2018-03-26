using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class DialogButtonsSectionParser
    {
        public static Parser<TriggerSyntax> Trigger =>
            TriggerParser.NumberedTrigger.Or(TriggerParser.NamedTrigger);

        public static Parser<DialogButtonsSectionSyntax> ParseButtonSection(string type, string name, string sectionSubType) =>
            from triggers in Trigger.Many()
            select new DialogButtonsSectionSyntax(type, name, sectionSubType, triggers.ToImmutableArray());
    }
}
