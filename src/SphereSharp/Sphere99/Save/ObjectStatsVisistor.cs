using Antlr4.Runtime.Misc;
using SphereSharp.Sphere99;
using System.Collections.Generic;
using static SphereSharp.sphereScript99Parser;

namespace SphereSharp.Sphere99.Save
{
    public class ObjectStatsVisistor : sphereScript99BaseVisitor<string>
    {
        private Dictionary<string, ObjectStats> stats = new Dictionary<string, ObjectStats>();
        private PropertyValueExtractor propertyValueExtractor = new PropertyValueExtractor();
        private string name;

        public IEnumerable<ObjectStats> Stats => stats.Values;

        public override string VisitWorldItemSection([NotNull] sphereScript99Parser.WorldItemSectionContext context)
        {
            name = null;
            var result = base.VisitWorldItemSection(context);
            if (name != null)
                CollectStats(name, context.propertyList());

            return result;
        }

        public override string VisitWorldCharSection([NotNull] sphereScript99Parser.WorldCharSectionContext context)
        {
            name = null;
            var result = base.VisitWorldCharSection(context);
            if (name != null)
                CollectStats(name, context.propertyList());

            return result;
        }

        public override string VisitWorldCharSectionHeader([NotNull] sphereScript99Parser.WorldCharSectionHeaderContext context)
        {
            name = context.sectionName().GetText();
            return base.VisitWorldCharSectionHeader(context);
        }

        public override string VisitWorldItemSectionHeader([NotNull] WorldItemSectionHeaderContext context)
        {
            name = context.sectionName().GetText();
            return base.VisitWorldItemSectionHeader(context);
        }

        private void CollectStats(string name, PropertyListContext propertyListContext)
        {
            int amount = 1;
            if (propertyValueExtractor.TryExtract("Amount", propertyListContext, out var amountText))
                amount = int.Parse(amountText);

            if (!stats.TryGetValue(name, out var stat))
            {
                stat = new ObjectStats(name);
                stats.Add(name, stat);
            }
            stat.AddInstance(amount);
        }
    }
}
