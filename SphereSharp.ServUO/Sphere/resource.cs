using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public static partial class _Global
    {
        public static int SPELLFLAG_DIR_ANIM = 0x0001;	// Evoke type cast or directed. (animation)

        public static int SPELLFLAG_TARG_OBJ = 0x0002;	// Need to target an object or char ?

        public static int SPELLFLAG_TARG_CHAR = 0x0004;	// Needs to target a living thing

        public static int SPELLFLAG_TARG_XYZ = 0x0008;	// Can just target a location.

        public static int SPELLFLAG_HARM		= 0x0010;	// The spell is in some way harmfull.

        public static int SPELLFLAG_FX_BOLT = 0x0020;	// Effect is a bolt to the target.

        public static int SPELLFLAG_FX_TARG = 0x0040;	// Effect is at the target.

        public static int SPELLFLAG_FIELD = 0x0080;	// create a field of stuff. (fire,poison,wall)

        public static int SPELLFLAG_SUMMON = 0x0100;	// summon a creature.

        public static int SPELLFLAG_GOOD = 0x0200;	// The spell is a good spell. u intend to help to receiver.

        public static int SPELLFLAG_RESIST = 0x0400;	// Allowed to resist this.

        public static int SPELLFLAG_DISABLED = 0x8000;

    }
}
