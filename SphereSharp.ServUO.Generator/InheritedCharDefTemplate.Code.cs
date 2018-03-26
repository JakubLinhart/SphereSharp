using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Generator
{
    public partial class InheritedCharDefTemplate
    {
        public string NameSpace { get; }
        public string ClassName { get; }
        public string BaseClassName { get; }
        public string Id { get; }
        public CharDef CharDef { get; }

        public InheritedCharDefTemplate(string ns, CharDef charDef)
        {
            if (charDef.IsBase)
            {
                throw new ArgumentException($"{nameof(InheritedCharDefTemplate)} cannot generate C# code for base char def.",
                    nameof(charDef));
            }

            CharDef = charDef;
            NameSpace = ns;
            ClassName = charDef.DefName;
            BaseClassName = charDef.BaseCharDef.DefName;
            Id = $"0x{charDef.Id:X4}";
        }
    }
}
