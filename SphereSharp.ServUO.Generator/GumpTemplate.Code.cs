using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Generator
{
    public partial class GumpTemplate
    {
        public string NameSpace { get; }
        public string ClassName { get; }

        public GumpTemplate(string ns, GumpDef gumpDef)
        {
            NameSpace = ns;
            ClassName = gumpDef.DefName;
        }
    }
}
