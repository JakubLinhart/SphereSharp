using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public enum CLIMODE_TYPE   // What mode is the client to server connection in ? (waiting for input ?)

    {

        CLIMODE_PROXY,

        CLIMODE_TELNET_USERNAME,

        CLIMODE_TELNET_PASS,

        CLIMODE_TELNET_READY,   // logged in.



        // setup events ------------------------------------------------



        CLIMODE_SETUP_CONNECTING,

        CLIMODE_SETUP_SERVERS,      // client has received the servers list.

        CLIMODE_SETUP_RELAY,        // client has been relayed to the game server. wait for new login.

        CLIMODE_SETUP_CHARLIST, // client has the char list and may (play a char, delete a char, create a new char)



        // Capture the user input for this mode. ------------------------------------------

        CLIMODE_NORMAL,     // No targeting going on. we are just walking around etc.



        // asyc events enum here. --------------------------------------------------------

        CLIMODE_DRAG,           // I'm dragging something. (not quite a targeting but similar)

        CLIMODE_DEATH,          // The death menu is up.

        CLIMODE_DYE,            // The dye dialog is up.

        CLIMODE_INPVAL,     // special text input dialog (for setting item attrib)



        // Some sort of general gump dialog ----------------------------------------------

        CLIMODE_DIALOG,     // from RES_Dialog



        // Hard-coded (internal) gumps.

        CLIMODE_DIALOG_ADMIN,

        CLIMODE_DIALOG_EMAIL,   // Force the user to enter their emial address. (if they have not yet done so)

        CLIMODE_DIALOG_GUILD,   // reserved.

        CLIMODE_DIALOG_HAIR_DYE, // Using hair dye



        // Making a selection from a menu. ----------------------------------------------

        CLIMODE_MENU,       // RES_Menu



        // Hard-coded (internal) menus.

        CLIMODE_MENU_SKILL,     // result of some skill. tracking, tinkering, blacksmith, etc.

        CLIMODE_MENU_SKILL_TRACK_SETUP,

        CLIMODE_MENU_SKILL_TRACK,

        CLIMODE_MENU_GM_PAGES,      // show me the gm pages .

        CLIMODE_MENU_EDIT,      // edit the contents of a container.



        // promting for text input.------------------------------------------------------

        // CLIMODE_PROMPT,					// Some sort of text prompt input.

        CLIMODE_PROMPT_NAME_RUNE,

        CLIMODE_PROMPT_NAME_KEY,        // naming a key.

        CLIMODE_PROMPT_NAME_SIGN,       // name a house sign

        CLIMODE_PROMPT_NAME_SHIP,

        CLIMODE_PROMPT_GM_PAGE_TEXT,    // allowed to enter text for page.

        CLIMODE_PROMPT_VENDOR_PRICE,    // What would you like the price to be ?

        CLIMODE_PROMPT_TARG_VERB,       // Send a msg to another player.

        CLIMODE_PROMPT_STONE_NAME,      // prompt for text.

        CLIMODE_PROMPT_STONE_SET_ABBREV,

        CLIMODE_PROMPT_STONE_SET_TITLE,

        CLIMODE_PROMPT_STONE_GRANT_TITLE,



        // Targeting mouse cursor. -------------------------------------------------------------

        CLIMODE_MOUSE_TYPE, // Greater than this = mouse type targeting.



        // GM targeting command stuff.

        CLIMODE_TARG_OBJ_SET,       // Set some attribute of the item i will show.

        CLIMODE_TARG_OBJ_INFO,      // what item do i want props for ?



        CLIMODE_TARG_UNEXTRACT,     // Break out Multi items

        CLIMODE_TARG_ADDITEM,       // "ADDITEM" command.

        CLIMODE_TARG_LINK,          // "LINK" command

        CLIMODE_TARG_TILE,          // "TILE" command.



        // Normal user stuff. (mouse targeting)

        CLIMODE_TARG_SKILL,             // targeting a skill or spell.

        CLIMODE_TARG_SKILL_MAGERY,

        CLIMODE_TARG_SKILL_HERD_DEST,

        CLIMODE_TARG_SKILL_POISON,

        CLIMODE_TARG_SKILL_PROVOKE,



        CLIMODE_TARG_USE_ITEM,          // target for using the selected item

        CLIMODE_TARG_PET_CMD,           // targetted pet command

        CLIMODE_TARG_PET_STABLE,        // Pick a creature to stable.

        CLIMODE_TARG_REPAIR,        // attempt to repair an item.

        CLIMODE_TARG_STONE_RECRUIT,     // Recruit members for a stone	(mouse select)

        CLIMODE_TARG_PARTY_ADD,



        // WESTY MOD (MULTI CONFIRM)

        CLIMODE_PROMPT_MULTI_CONFIRM,

        // END WESTY MOD



        CLIMODE_TARG_QTY,

    };


    public partial class CClient
    {
        void SetTargMode(CLIMODE_TYPE targmode = CLIMODE_TYPE.CLIMODE_NORMAL, string pPrompt = null)

        {

            // Keep track of what mode the client should be in.

            // ??? Get rid of menu stuff if previous targ mode.

            // Can i close a menu ?

            // Cancel a cursor input.



            if (GetTargMode() == targmode)

                return;



            if (GetTargMode() != CLIMODE_TYPE.CLIMODE_NORMAL && targmode != CLIMODE_TYPE.CLIMODE_NORMAL)

            {

                // Just clear the old target mode

                WriteString("Previous Targeting Cancelled");

            }



            m_Targ.m_Mode = targmode;

            WriteString((targmode == CLIMODE_TYPE.CLIMODE_NORMAL) ? "Targeting Cancelled" : pPrompt);

        }

        bool addTarget(CLIMODE_TYPE targmode, string pPrompt, bool fAllowGround, bool fCheckCrime) // Send targeting cursor to client

        {

            // Give client a mouse targeting cursor.

            // will this be selective for us ? objects only or chars only ? not on the ground (statics) ?

            // NOTE: 

            //  Expect XCMD_Target back.

            //  This should result in Event_Target()



            if (m_pChar == NULL)

                return false;



            SetTargMode(targmode, pPrompt);

            var flags = fCheckCrime ? TargetFlags.Harmful : TargetFlags.None;
            SphereSharpRuntime.Current.Target(fAllowGround, flags, mobile);

            return true;

            //CUOCommand cmd;

            //memset(&(cmd.Target), 0, sizeof(cmd.Target));

            //cmd.Target.m_Cmd = XCMD_Target;

            //cmd.Target.m_TargType = fAllowGround; // fAllowGround;	// 1=allow xyz, 0=objects only.

            //cmd.Target.m_context = targmode;    // my id code for action.

            //cmd.Target.m_fCheckCrime = fCheckCrime;



            //xSendPkt(&cmd, sizeof(cmd.Target));

            //return true;

        }


    }
}
