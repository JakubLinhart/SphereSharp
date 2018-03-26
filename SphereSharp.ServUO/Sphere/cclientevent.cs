using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CClient
    {
        void Event_Target(DWORD context, CSphereUID uid, CPointMap pt, ITEMID_TYPE id)

        {

            // XCMD_Target

            // If player clicks on something with the targetting cursor

            // Assume addTarget was called before this.

            // NOTE: Make sure they can actually validly trarget this item !



            ASSERT(m_pChar);

            //if (context != GetTargMode())

            //{

            //    // DEBUG_ERR(( "%x: Unrequested target info ?" LOG_CR, m_Socket.GetSocket()));

            //    WriteString("Unexpected target info");

            //    return;

            //}

            //if (!pt.IsValidXY() && !uid.IsValidObjUID())

            //{

            //    // canceled

            //    SetTargMode();

            //    return;

            //}



            //pt.m_mapplane = m_pChar.GetTopMap();



            CLIMODE_TYPE prevmode = GetTargMode();

            ClearTargMode();



            CObjBasePtr pObj = g_World.ObjFind(uid);

            //if (IsPrivFlag(PRIV_GM))

            //{

            //    if (uid.IsValidObjUID() && pObj == NULL)

            //    {

            //        addObjectRemoveCantSee(uid, "the target");

            //        return;

            //    }

            //}

            //else

            //{

            //    if (uid.IsValidObjUID())

            //    {

            //        if (!m_pChar.CanSee(pObj))

            //        {

            //            addObjectRemoveCantSee(uid, "the target");

            //            return;

            //        }

            //    }

            //    else

            //    {

            //        // The point must be valid.

            //        if (m_pChar.GetTopDist(pt) > SPHEREMAP_VIEW_SIZE)

            //        {

            //            return;

            //        }

            //    }

            //}



            //if (pObj)

            //{

            //    // Point inside a container is not really meaningful here.

            //    pt = pObj.GetTopLevelObj().GetTopPoint();

            //}



            bool fSuccess = false;



            switch (prevmode)

            {

                // GM stuff.



                //case CLIMODE_TYPE.CLIMODE_TARG_OBJ_SET: fSuccess = OnTarg_Obj_Command(pObj, m_Targ.m_sText); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_OBJ_INFO: fSuccess = OnTarg_Obj_Info(pObj, pt, id); break;



                //case CLIMODE_TYPE.CLIMODE_TARG_UNEXTRACT: fSuccess = OnTarg_UnExtract(pObj, pt); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_ADDITEM: fSuccess = OnTarg_Item_Add(pObj, pt); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_LINK: fSuccess = OnTarg_Item_Link(pObj); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_TILE: fSuccess = OnTarg_Tile(pObj, pt); break;



                // Player stuff.



                //case CLIMODE_TYPE.CLIMODE_TARG_SKILL: fSuccess = OnTarg_Skill(pObj); break;

                case CLIMODE_TYPE.CLIMODE_TARG_SKILL_MAGERY: fSuccess = OnTarg_Skill_Magery(pObj, pt); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_SKILL_HERD_DEST: fSuccess = OnTarg_Skill_Herd_Dest(pObj, pt); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_SKILL_POISON: fSuccess = OnTarg_Skill_Poison(pObj); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_SKILL_PROVOKE: fSuccess = OnTarg_Skill_Provoke(pObj); break;



                //case CLIMODE_TYPE.CLIMODE_TARG_REPAIR: fSuccess = m_pChar->Use_Repair(g_World.ItemFind(uid)); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_PET_CMD: fSuccess = OnTarg_Pet_Command(pObj, pt); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_PET_STABLE: fSuccess = OnTarg_Pet_Stable(g_World.CharFind(uid)); break;



                //case CLIMODE_TYPE.CLIMODE_TARG_USE_ITEM: fSuccess = OnTarg_Use_Item(pObj, pt, id); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_STONE_RECRUIT: fSuccess = OnTarg_Stone_Recruit(g_World.CharFind(uid)); break;

                //case CLIMODE_TYPE.CLIMODE_TARG_PARTY_ADD: fSuccess = OnTarg_Party_Add(g_World.CharFind(uid)); break;

            }

        }

    }
}
