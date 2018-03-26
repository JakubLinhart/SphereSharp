using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public class CClientTargModeContext
    {
        public CLIMODE_TYPE m_Mode;	// Type of async operation under way.

        // CLIMODE_TARG_SKILL_MAGERY

        public class TmSkillMagery


        {


            public SPELL_TYPE m_Spell;         // targeting what spell ?

            public CREID_TYPE m_SummonID;

            public bool m_fSummonPet;

        }

        public TmSkillMagery m_tmSkillMagery = new TmSkillMagery();
        public CSphereUIDBase m_UID;
        internal CSphereUIDBase m_PrvUID;
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
