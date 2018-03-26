using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CObjBase
    {
        public CObjBase(int serial) : base(serial)
        {
        }

        public void Effect(EFFECT_TYPE motion, ITEMID_TYPE id, CObjBase pSource, BYTE bSpeedSeconds, BYTE bLoop, bool fExplode = false )

        {
            switch (motion)
            {
                case EFFECT_TYPE.EFFECT_OBJ:
                    Effects.SendTargetEffect(((CChar)pSource).mobile, (int)id, bSpeedSeconds, bLoop);
                    break;
            }
        }
    }
}
