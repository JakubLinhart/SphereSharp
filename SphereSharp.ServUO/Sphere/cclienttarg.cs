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
        public bool OnTarg_Skill_Magery(CObjBase pObj, CPointMap pt)

        {

            // The client player has targeted a spell.

            // CLIMODE_TARG_SKILL_MAGERY



            if (m_Targ.m_tmSkillMagery.m_Spell == SPELL_TYPE.SPELL_Polymorph)

            {

                throw new NotImplementedException();
                //HRESULT hRes = Cmd_Skill_Menu(g_Cfg.ResourceGetIDType(RES_SkillMenu, "sm_polymorph"));

                //return (hRes > 0);

            }



            m_pChar.m_Act.m_atMagery.m_Spell = m_Targ.m_tmSkillMagery.m_Spell;

            m_pChar.m_Act.m_atMagery.m_SummonID = m_Targ.m_tmSkillMagery.m_SummonID;

            m_pChar.m_Act.m_atMagery.m_fSummonPet = m_Targ.m_tmSkillMagery.m_fSummonPet;



            m_pChar.m_Act.m_TargPrv = (CSphereUID)m_Targ.m_PrvUID; // Source (wand or you?)

            m_pChar.m_Act.m_Targ = pObj != null ? (CSphereUID)pObj.GetUID() : UID_INDEX_CLEAR;

            m_pChar.m_Act.m_pt = pt;



            return (m_pChar.Skill_Start (SKILL_TYPE.SKILL_MAGERY));

        }

    }
}
