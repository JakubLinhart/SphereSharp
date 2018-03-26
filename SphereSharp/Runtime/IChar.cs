using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Runtime
{
    public interface IChar : IHoldTags
    {
        int Fame { get; set; }
        int Karma { get; set; }
        int Npc { get; set; }

        int MaxHits { get; set; }
        int MaxStam { get; set; }
        int MaxMana { get; set; }

        int Str { get; set; }
        int Dex { get; set; }
        int Int { get; set; }

        int Parrying { get; set; }
        int Tactics { get; set; }
        int Wrestling { get; set; }
        int SpiritSpeak { get; set; }

        int Color { get; set; }
    }
}
