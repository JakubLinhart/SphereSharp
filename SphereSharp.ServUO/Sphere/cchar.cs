using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CChar
    {
        public int Action
        {
            get => (int)Skill_GetActive();
            set => Skill_Start((SKILL_TYPE)value);
        }

        public void Skill(int skill)
        {
            Skill_Start((SKILL_TYPE)skill);
        }
    }
}
