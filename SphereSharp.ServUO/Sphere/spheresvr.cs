using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public class CClientTargModeContext
    {
        public CLIMODE_TYPE m_Mode = CLIMODE_TYPE.CLIMODE_NORMAL; // Type of async operation under way.

        // CLIMODE_TARG_SKILL_MAGERY

        public class TmSkillTarg
        {
            public SKILL_TYPE m_Skill = SKILL_TYPE.SKILL_NONE;         // targeting what spell ?
        }

        public class TmSkillMagery


        {


            public SPELL_TYPE m_Spell = SPELL_TYPE.SPELL_NONE;         // targeting what spell ?

            public CREID_TYPE m_SummonID;

            public bool m_fSummonPet;

        }

        public TmSkillMagery m_tmSkillMagery = new TmSkillMagery();
        public TmSkillTarg m_tmSkillTarg = new TmSkillTarg();

        public CSphereUIDBase m_UID = new CSphereUID(0);
        internal CSphereUIDBase m_PrvUID = new CSphereUID(0);
    }

    public partial class CClient
    {

        private CClientTargModeContext m_Targ = new CClientTargModeContext();


        private CLIMODE_TYPE GetTargMode()

	    {

		    return(m_Targ.m_Mode );

	    }
    }
}
