using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public struct SKILL_LEVEL
    {
        public short Value { get; }

        public SKILL_LEVEL(short value)
        {
            Value = value;
        }

        public static implicit operator SKILL_LEVEL(short val) => new SKILL_LEVEL(val);
        public static implicit operator SKILL_LEVEL(int val) => new SKILL_LEVEL((short)val);
        public static implicit operator int(SKILL_LEVEL val) => (int)val.Value;
        public static implicit operator bool(SKILL_LEVEL val) => val.Value != 0;

    }
}
