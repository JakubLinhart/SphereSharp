using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Model
{
    public class ItemDef
    {
        private ushort? id;

        public ushort Id
        {
            get
            {
                if (id.HasValue) return id.Value;

                if (BaseItemDef == null)
                    throw new InvalidOperationException("Item without id and BaseItemDef");

                return BaseItemDef.Id;
            }

            set => id = value;
        }
        public string DefName { get; set; }
        public ItemDef BaseItemDef { get; set; }
        public ImmutableDictionary<string, TriggerDef> Triggers { get; set; } = ImmutableDictionary<string, TriggerDef>.Empty;

        public bool IsBase => BaseItemDef == null;
    }
}
