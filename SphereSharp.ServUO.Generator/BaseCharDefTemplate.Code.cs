using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Generator
{
    public partial class BaseCharDefTemplate
    {
        public string NameSpace { get; }
        public string ClassName { get; }
        public string Id { get; }
        public CharDef CharDef { get; }
        public string MoveRate { get; }

        public BaseCharDefTemplate(string ns, CharDef charDef)
        {
            NameSpace = ns;
            ClassName = charDef.DefName;
            Id = $"0x{charDef.Id:X4}";
            CharDef = charDef;
            MoveRate = ((double)charDef.MoveRate / 15f).ToString("G1", CultureInfo.InvariantCulture);
        }
    }
}
