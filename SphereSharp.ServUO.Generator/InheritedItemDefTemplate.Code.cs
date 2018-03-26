using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Generator
{
    public partial class InheritedItemDefTemplate
    {
        public string NameSpace { get; }
        public string ClassName { get; }
        public string BaseClassName { get; }
        public string Id { get; }

        public InheritedItemDefTemplate(string ns, ItemDef itemDef)
        {
            if (itemDef.IsBase)
            {
                throw new ArgumentException($"{nameof(InheritedItemDefTemplate)} cannot generate C# code for base item def.",
                    nameof(itemDef));
            }

            NameSpace = ns;
            ClassName = itemDef.DefName;
            BaseClassName = itemDef.BaseItemDef.DefName;
            Id = $"0x{itemDef.Id:X4}";
        }
    }
}
