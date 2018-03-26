using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Runtime
{
    public interface IItem : IHoldTags, IHoldTriggers
    {
        int Color { get; set; }
    }
}
