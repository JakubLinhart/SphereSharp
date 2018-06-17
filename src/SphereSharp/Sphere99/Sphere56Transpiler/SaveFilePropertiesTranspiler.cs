using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class ItemSaveFilePropertiesTranspiler : SaveFilePropertiesTranspiler
    {
        private static HashSet<string> forbiddenProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Age" };

        public ItemSaveFilePropertiesTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentVisitor)
            : base(builder, parentVisitor, forbiddenProperties)
        {
        }
    }

    internal sealed class CharSaveFilePropertiesTranspiler : SaveFilePropertiesTranspiler
    {
        private static HashSet<string> forbiddenProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { };

        public CharSaveFilePropertiesTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentVisitor)
            : base(builder, parentVisitor, forbiddenProperties)
        {
        }
    }

    internal abstract class SaveFilePropertiesTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor parentVisitor;
        private readonly ISet<string> forbiddenProperties;
        private readonly Dictionary<string, uint> specificAttrValues = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase)
        {
            { "attr_identified",        0x00000001 },
            { "attr_decay",             0x00000002 },
            { "attr_newbie",            0x00000004 },
            { "attr_movealways",       0x00000008 },
            { "Attr_movenever",        0x00000010 },
            { "attr_magic",             0x00000020 },
            { "attr_owned",             0x00000040 },
            { "attr_invis",             0x00000080 },
            { "attr_cursed",            0x00000100 },
            { "attr_cursed2",           0x00000200 },
            { "attr_blessed",           0x00000400 },
            { "attr_blessed2",          0x00000800 },
            { "attr_forsale",           0x00001000 },
            { "attr_stolen",            0x00002000 },
            { "Attr_candecay",         0x00004000 },
            { "attr_static",            0x00008000 },
        };

        private readonly Dictionary<string, uint> specificFlagValues = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase)
        {
            { "Flag_invul",            0x000000001 },
            { "Flag_dead",             0x000000002 },
            { "Flag_freeze",           0x000000004 },
            { "Flag_invisible",        0x000000008 },
            { "Flag_sleeping",         0x000000010 },
            { "Flag_war",              0x000000020 },
            { "Flag_reactive",         0x000000040 },
            { "Flag_poisoned",         0x000000080 },
            { "Flag_nightsight",       0x000000100 },
            { "Flag_reflection",       0x000000200 },
            { "Flag_polymorph",        0x000000400 },
            { "Flag_incognito",        0x000000800 },
            { "Flag_spiritspeak",      0x000001000 },
            { "Flag_insubstantial",    0x000002000 },
            { "Flag_emoteaction",      0x000004000 },
            { "Flag_commcrystal",      0x000008000 },
            { "Flag_hasshield",        0x000010000 },
            { "Flag_archercanmove",    0x000020000 },
            { "Flag_stone",            0x000040000 },
            { "Flag_hovering",         0x000080000 },
            { "Flag_fly",              0x000100000 },
            { "Flag_hallucinating",    0x000400000 },
            { "Flag_hidden",           0x000800000 },
            { "Flag_indoors",          0x001000000 },
            { "Flag_criminal",         0x002000000 },
            { "Flag_conjured",         0x004000000 },
            { "Flag_pet",              0x008000000 },
            { "Flag_spawned",          0x010000000 },
            { "Flag_saveparity",       0x020000000 },
            { "Flag_ridden",           0x040000000 },
            { "Flag_onhorse",          0x080000000 },
        };

        public SaveFilePropertiesTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentVisitor,
            ISet<string> forbiddenProperties)
        {
            this.builder = builder;
            this.parentVisitor = parentVisitor;
            this.forbiddenProperties = forbiddenProperties;
        }

        public override bool VisitPropertyList([NotNull] sphereScript99Parser.PropertyListContext context)
        {
            if (context.NEWLINE() != null)
                builder.Append(context.NEWLINE().GetText());

            if (context.propertyAssignment() == null)
                return true;

            uint? flagValue = null;
            uint? attrValue = null;

            foreach (var assignment in context.propertyAssignment())
            {
                var name = assignment.propertyName().GetText();
                var value = assignment.propertyValue()?.GetText();

                if (specificFlagValues.TryGetValue(name, out uint specificFlagValue))
                {
                    if (!value.Equals("0", StringComparison.OrdinalIgnoreCase))
                        flagValue = flagValue.HasValue ? flagValue.Value | specificFlagValue : specificFlagValue;
                }
                else if (specificAttrValues.TryGetValue(name, out uint specificAttrValue))
                {
                    if (!value.Equals("0", StringComparison.OrdinalIgnoreCase))
                        attrValue = attrValue.HasValue ? attrValue.Value | specificAttrValue : specificAttrValue;
                }
                else if (name.Equals("MORE1", StringComparison.OrdinalIgnoreCase))
                {
                    parentVisitor.AppendPropertyAssignment(assignment, value.Trim('"'));
                }
                else if (!forbiddenProperties.Contains(name))
                {
                    parentVisitor.AppendPropertyAssignment(assignment);
                }
            }

            builder.EnsureNewline();

            if (flagValue.HasValue)
                builder.AppendLine($"FLAGS=0{flagValue.Value:X8}");
            if (attrValue.HasValue)
                builder.AppendLine($"ATTR=0{attrValue.Value:X8}");

            return true;
        }
    }
}
