using Server;
using Server.Gumps;
using SphereSharp.Interpreter;
using SphereSharp.Model;
using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CChar : CItem, IChar, IClient
    {
        public readonly Mobile mobile;

        public int Fame { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Karma { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Npc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MaxHits { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MaxStam { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MaxMana { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Str { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Dex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Int { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Parrying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Tactics { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Wrestling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SpiritSpeak { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CChar(Mobile mobile) : base(mobile.Serial.Value)
        {
            this.m_Skill = new SkillsArray(this);
            this.m_Stat = new StatsArray(this);
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

        // TODO: cleanup, remove MobileAdapter, move IClient stuff to client
        private readonly IHoldTags tagHolder = new StandardTagHolder();
        private readonly IHoldTriggers triggerHolder = new StandardTriggerHolder(name => null, SphereSharpRuntime.Current.RunCodeBlock);

        public void Tag(string key, string value) => tagHolder.Tag(key, value);
        public string Tag(string key) => tagHolder.Tag(key);
        public void RemoveTag(string key) => tagHolder.RemoveTag(key);

        public void SubscribeEvents(EventsDef eventsDef) => triggerHolder.SubscribeEvents(eventsDef);
        public void UnsubscribeEvents(EventsDef eventsDef) => triggerHolder.UnsubscribeEvents(eventsDef);
        public string RunTrigger(string triggerName, EvaluationContext context) => triggerHolder.RunTrigger(triggerName, context);

        public void Dialog(string defName, Arguments arguments)
        {
            var gumpDef = SphereSharpRuntime.Current.CodeModel.GetGumpDef(defName);
            var gumpType = SphereSharpRuntime.Current.GetServUOType(defName);

            var gump = (Gump)Activator.CreateInstance(gumpType);
            SphereSharpRuntime.Current.InitializeDialog(gump, mobile, defName, arguments);

            this.mobile.SendGump(gump);
        }

        public void CloseDialog(string defName, int buttonId)
        {
            var gumpDef = SphereSharpRuntime.Current.CodeModel.GetGumpDef(defName);
            var gumpType = SphereSharpRuntime.Current.GetServUOType(defName);

            mobile.CloseGump(gumpType);
        }

        public void SysMessage(string message)
        {
            this.mobile.SendMessage(message);
        }
    }
}
