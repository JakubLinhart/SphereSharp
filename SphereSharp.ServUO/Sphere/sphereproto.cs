using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public enum TALKMODE_TYPE  // Modes we can talk/bark in.

    {

        TALKMODE_SYSTEM = 0,    // normal system message at bottom of screen.

        TALKMODE_PROMPT,        // 1= Display as system prompt

        TALKMODE_EMOTE,         // 2= :	*smiles* at object

        TALKMODE_SAY,           // 3= A chacter speaking.

        TALKMODE_OBJ,           // 4= Text At Object. Ussually text on myself.

        TALKMODE_NOTHING,       // 5= Does not display

        TALKMODE_ITEM,          // 6= text labeling an item. Preceeded by "You see"

        TALKMODE_NOSCROLL,      // 7= As a status msg. Does not scroll when next message appears.

        TALKMODE_WHISPER,       // 8= ;	only those close can here.

        TALKMODE_YELL,          // 9= ! can be heard 2 screens away.

        TALKMODE_CLIENT_MAX = TALKMODE_YELL,



        TALKMODE_TOKENIZED = 0xc0,

        TALKMODE_BROADCAST = 0xFF, // Server Internal only

    }

    public enum EFFECT_TYPE

    {

        EFFECT_BOLT = 0,    // a targetted bolt

        EFFECT_LIGHTNING,   // lightning bolt.

        EFFECT_XYZ, // Stay at current xyz ??? not sure about this.

        EFFECT_OBJ, // effect at single Object.

    }

}
