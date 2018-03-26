using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Generator
{
    public partial class BaseItemDefTemplate
    {
        public string NameSpace { get; }
        public string ClassName { get; }
        public string Id { get; }

        public BaseItemDefTemplate(string ns, ItemDef itemDef)
        {
            NameSpace = ns;
            ClassName = itemDef.DefName;
            Id = $"0x{itemDef.Id:X4}";
        }
    }
}
