using SphereSharp.Model;
using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO
{
    public interface ISphereItem : IItem
    {
        ItemDef Def { get; }
    }
}
