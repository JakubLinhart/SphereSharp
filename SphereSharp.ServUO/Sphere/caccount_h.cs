using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public partial class _Global
    {
        public static int PRIV_SERVER = 0x0001;	// This is a server account. CServerDef - Same name

        public static int PRIV_GM = 0x0002;	// Acts as a GM (dif from having PLEVEL_GM)



        public static int PRIV_GM_PAGE = 0x0008;	// can Listen to GM pages or not.

        public static int PRIV_HEARALL = 0x0010;// I can hear everything said by people of lower plevel

        public static int PRIV_ALLMOVE = 0x0020;	// I can move all things. (GM only)

        public static int PRIV_DETAIL = 0x0040;	// Show combat/script error detail messages

        public static int PRIV_DEBUG = 0x0080;	// Show all objects as boxes and chars as humans.

        public static int PRIV_EMAIL_VALID = 0x0100;	// The email address has been validated.

        public static int PRIV_PRIV_HIDE = 0x0200;	// Show the GM title and Invul flags.



        public static int PRIV_JAILED = 0x0800;	// Must be /PARDONed from jail.

        public static int PRIV_BLOCKED = 0x2000;	// The account is blocked.

        public static int PRIV_ALLSHOW = 0x4000;	// Show even the offline chars.

        public static int PRIV_TEMPORARY = 0x8000;	// Delete this account when logged out.

    }

    public partial class CAccount
    {
        public WORD m_PrivFlags;			// optional privileges for char (bit-mapped)

        public bool IsPrivFlag(WORD wPrivFlags)

	    {	// PRIV_GM

		    return((m_PrivFlags & wPrivFlags ) ? true : false);

	    }

    }

    public class CAccountPtr : CAccount
    {

    }
}
