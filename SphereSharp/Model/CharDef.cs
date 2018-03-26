using System;
using System.Collections.Immutable;

namespace SphereSharp.Model
{
    public class CharDef
    {
        private ushort? id;

        public ushort Id
        {
            get
            {
                if (id.HasValue) return id.Value;

                if (BaseCharDef == null)
                    throw new InvalidOperationException("Character without id and BaseCharDef");

                return BaseCharDef.Id;
            }

            set => id = value;
        }

        public string Name { get; set; }
        public int MoveRate { get; set; }
        public int Armor { get; set; }
        public int Attack { get; set; }

        public string DefName { get; set; }
        public CharDef BaseCharDef { get; set; }
        public ImmutableDictionary<string, TriggerDef> Triggers { get; set; } = ImmutableDictionary<string, TriggerDef>.Empty;

        public bool IsBase => BaseCharDef == null;
    }
}
