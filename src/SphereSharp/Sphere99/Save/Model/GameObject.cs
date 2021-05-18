using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Save.Model
{
    public abstract class GameObject
    {
        protected readonly ValuesHolder properties;
        protected readonly ValuesHolder tags;

        public uint Serial { get; }
        public string DefName { get; }

        public GameObject(string defName, ValuesHolder properties, ValuesHolder tags)
        {
            this.properties = properties;
            this.tags = tags;
            DefName = defName;
            Serial = properties.GetHexUInt("Serial");
        }

        public bool IsDefname(params string[] defNames)
            => defNames.Contains(DefName, StringComparer.OrdinalIgnoreCase);

        public bool HasTag(string name) => tags.IsDefined(name);
        public string GetTag(string name) => tags.GetSingle(name);
        public bool TryGetTag(string name, out string result) => tags.TryGetSingle(name, out result);

        public bool IsPropertyDefined(string name) => properties.IsDefined(name);
        public abstract bool IsPlayer { get; }
        public abstract bool IsNpc { get; }
        public abstract uint Amount { get; }
    }
}
