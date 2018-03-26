using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CClient
    {
        private readonly Mobile mobile;

        public CClient(Mobile mobile)
        {
            this.mobile = mobile;
            this.m_pChar = new CCharPtr(mobile);
        }

        public bool WriteString(string pszMsg) // System message (In lower left corner)

        {
            // TODO:
            this.mobile.SendAsciiMessage(pszMsg);

            return true;
        }
    }
}
