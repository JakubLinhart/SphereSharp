using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CClient
    {
        void ClearTargMode()

        {

            // done with the last mode.

            m_Targ.m_Mode = CLIMODE_TYPE.CLIMODE_NORMAL;

        }
    }
}
