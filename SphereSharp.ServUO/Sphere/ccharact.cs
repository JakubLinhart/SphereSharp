using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CChar : CItem
    {
        public readonly Mobile mobile;

        public CChar(Mobile mobile) : base(mobile.Serial.Value)
        {
            this.m_Skill = new SkillsArray(this);
            this.mobile = mobile;
        }

        public bool WriteString(string pMsg)   // Push a message back to the client if there is one.

        {

            // TODO:
            //if (!IsClient())

            //    return false;

            this.mobile.SendAsciiMessage(pMsg);

            // TODO:
            return true;
        }

        // TODO: move to baseobj
        public void Sound(SOUND_TYPE id, int iOnce = 1) // Play sound effect for player

        {

            // play for everyone near by.



            if (id <= 0)

                return;


            // TODO:
            Effects.PlaySound(mobile.Location, mobile.Map, id);

            //for (CClientPtr pClient = g_Serv.GetClientHead(); pClient; pClient = pClient->GetNext())

            //{

            // if ( ! pClient->CanHear(this, TALKMODE_OBJ ))

            //  continue;

            // pClient->addSound(id, this, iOnce );

            //   }

        }


        public void Speak(string pszText) => Speak(pszText, HUE_CODE.HUE_TEXT_DEF, TALKMODE_TYPE.TALKMODE_SAY, FONT_TYPE.FONT_NORMAL);

        public override void Speak(string pszText, HUE_TYPE wHue, TALKMODE_TYPE mode, FONT_TYPE font)

        {

            // Speak to all clients in the area.

            // Ignore the font argument here !


            // TODO:
            //if (IsStatFlag(STATF_Stone | STATF_Squelch))

            //    return;

            //Reveal(STATF_Hidden | STATF_Sleeping);

            //if (mode == TALKMODE_YELL && GetPrivLevel() >= PLEVEL_Counsel)

            //{   // Broadcast yell.

            //    mode = TALKMODE_BROADCAST;  // GM Broadcast (Done if a GM yells something)

            //}

            //base.Speak(pszText, m_SpeechHue, mode, m_fonttype);

            this.mobile.Say(pszText);
        }

        protected override void OnTimeout()
        {
            OnTick_Timeout();
        }

        public void OnTick_Timeout()
        {
            // My turn to do some action.

            var result = Skill_Done();

            if (result == -(int)CSkillDef.T_TYPE_.T_Abort)

            {
                Skill_Fail(true); // fail with message but no credit.
            }
            else if (result == -(int)CSkillDef.T_TYPE_.T_Fail)
            {
                Skill_Fail(false);
            }
            else if (result == -(int)CSkillDef.T_TYPE_.T_QTY)
            {
                Skill_Cleanup();
            }
        }

        public void Skill_Fail(bool fCancel)

        {

            // This is the normal skill check failure.

            // Other types of failure don't come here.

            //

            // ARGS:

            //	fCancel = no credt.

            //  else We still get some credit for having tried.



            SKILL_TYPE skill = Skill_GetActive();

            if (skill == SKILL_TYPE.SKILL_NONE)

                return;



            if (!IsSkillBase(skill))

            {

                // TODO:
                // DEBUG_CHECK(IsSkillNPC(skill));

                Skill_Cleanup();

                return;

            }



            if (m_Act.m_Difficulty > 0)

            {

                m_Act.m_Difficulty = -m_Act.m_Difficulty;

            }



            if (Skill_Stage(CSkillDef.T_TYPE_.T_Fail) >= 0)

            {

                // Get some experience for failure ?

                // TODO:
                // Skill_Experience(skill, m_Act.m_Difficulty);

            }



            Skill_Cleanup();

        }

    }
}
