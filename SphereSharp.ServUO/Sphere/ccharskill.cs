using SphereSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public partial class CChar
    {
        public bool Skill_Wait(SKILL_TYPE skilltry)

        {

            // Some sort of push button skill.

            // We want to do some new skill. Can we ?

            // If this is the same skill then tell them to wait.


            // TODO:
            //if (IsStatFlag(STATF_DEAD | STATF_Sleeping | STATF_Freeze | STATF_Stone))

            //{

            //    WriteString("You can't do much in your current state.");

            //    return (true);

            //}



            SKILL_TYPE skill = Skill_GetActive();

            if (skill == SKILL_TYPE.SKILL_NONE)    // not currently doing anything.

            {
                // TODO:
                // Reveal();

                return (false);

            }



            // What if we are in combat mode ?

            // TODO:
            //if (IsStatFlag(STATF_War))

            //{

            //    WriteString("You are preoccupied with thoughts of battle.");

            //    return (true);

            //}



            // Passive skills just cancel.

            // SKILL_SPIRITSPEAK ?

            if (skilltry != skill)

            {

                if (skill == SKILL_TYPE.SKILL_MEDITATION ||

                    skill == SKILL_TYPE.SKILL_HIDING ||

                    skill == SKILL_TYPE.SKILL_Stealth)

                {

                    Skill_Fail(true);

                    return (false);

                }

            }



            WriteString("You must wait to perform another action");

            return (true);

        }

        public bool Skill_Start(SKILL_TYPE skill, int iDifficulty = 0)

        {

            // We have all the info we need to do the skill. (targeting etc)

            // Set up how long we have to wait before we get the desired results from this skill.

            // Set up any animations/sounds in the mean time.

            // Calc if we will succeed or fail.

            // ARGS:

            //  iDifficulty = 0-100

            // RETURN:

            //  false = failed outright with no wait. "You have no chance of taming this"



            //if (g_Serv.IsLoading())

            //{

            //    if (skill != SKILL_NONE &&

            //        !IsSkillBase(skill) &&

            //        !IsSkillNPC(skill))

            //    {

            //        DEBUG_ERR(("UID:0%x Bad Skill %d for '%s'" LOG_CR, GetUID(), skill, (LPCTSTR)GetName()));

            //        return (false);

            //    }

            //    m_Act.m_SkillCurrent = skill;

            //    return (true);

            //}



            if (Skill_GetActive() != SKILL_TYPE.SKILL_NONE)

            {

                Skill_Fail(true);   // Fail previous skill unfinished. (with NO skill gain!)

            }



            if (skill != SKILL_TYPE.SKILL_NONE)

            {

                m_Act.m_SkillCurrent = skill;   // Start using a skill.

                m_Act.m_Difficulty = iDifficulty;


                // TODO:
                // ASSERT(IsSkillBase(skill) || IsSkillNPC(skill));



                // Some skill can start right away. Need no targeting.

                // 0-100 scale of Difficulty

                m_Act.m_Difficulty = Skill_Stage(CSkillDef.T_TYPE_.T_Start);

                if (m_Act.m_Difficulty < 0)

                {

                    Skill_Cleanup();

                    return (false);

                }



                if (IsSkillBase(skill))

                {
                    CSkillDefPtr pSkillDef = g_Cfg.GetSkillDef(skill);

                    ASSERT(pSkillDef);

                    int iWaitTime = pSkillDef.m_Delay.GetLinear(Skill_GetBase(skill));
                    // TODO: just a workaround for IsTimerExpired
                    iWaitTime = iWaitTime > 0 ? iWaitTime : 1;

                    if (iWaitTime != 0)

                    {

                        // How long before complete skill.

                        SetTimeout(iWaitTime);

                    }

                }

                // TODO:
                //if (IsTimerExpired())

                //{

                //    // the skill should have set it's own delay!?

                //    SetTimeout(1);

                //}

                if (m_Act.m_Difficulty > 0)

                {
                    if (!Skill_CheckSuccess(skill, m_Act.m_Difficulty))

                        m_Act.m_Difficulty = -m_Act.m_Difficulty; // will result in Failure ?

                }

            }


            // TODO:
            // emote the action i am taking.

            //if (g_Cfg.m_wDebugFlags & DEBUGF_NPC_EMOTE)

            //{

            //    Emote(Skill_GetName(true));

            //}



            return (true);

        }

        public int Skill_Stage(CSkillDef.T_TYPE_ stage)

        {

            // Call Triggers



            SKILL_TYPE skill = Skill_GetActive();

            if (skill == SKILL_TYPE.SKILL_NONE)

                return -1;


            var context = new EvaluationContext();
            context.Src = SphereSharpRuntime.Current.GetAdapter(this.mobile);
            context.Default = this;

            string triggerName = null;

            switch (stage)
            {
                case CSkillDef.T_TYPE_.T_Start:
                    triggerName = "start";
                    break;
                case CSkillDef.T_TYPE_.T_Stroke:
                    triggerName = "stroke";
                    break;
                case CSkillDef.T_TYPE_.T_Success:
                    triggerName = "success";
                    break;
            }

            if (!string.IsNullOrEmpty(triggerName))
                SphereSharpRuntime.Current.RunSkillTrigger(this, skill, triggerName);

            // <TODO>
            //if (iTrigRet == TRIGRET_RET_VAL)

            //{

            //    return execArgs.m_vValRet;

            //}



            //CSkillDefWPtr pSkillDef = g_Cfg.GetSkillDefW(skill);

            //if (pSkillDef != null)

            //{

            //    // res_skill

            //    //ccharactstate savestate = m_act;

            //    itrigret = pskilldef->ontriggerscript(execargs, stage, cskilldef::sm_triggers[stage].m_pszname);

            //    // m_act = savestate;

            //    if (itrigret == trigret_ret_val)

            //    {

            //        // they handled success, just clean up, don't do skill experience

            //        return execargs.m_vvalret;

            //    }

            //}
            // </TODO>


            switch (skill)

            {

                case SKILL_TYPE.SKILL_NONE:    // idling.

                    return 0;

                //case SKILL_TYPE.SKILL_ALCHEMY:

                //    return Skill_Alchemy(stage);

                //case SKILL_TYPE.SKILL_ANATOMY:

                //case SKILL_TYPE.SKILL_ANIMALLORE:

                //case SKILL_TYPE.SKILL_ITEMID:

                //case SKILL_TYPE.SKILL_ARMSLORE:

                //    return Skill_Information(stage);

                //case SKILL_TYPE.SKILL_PARRYING:

                //    return 0;

                //case SKILL_TYPE.SKILL_BEGGING:

                //    return Skill_Begging(stage);

                //case SKILL_TYPE.SKILL_BLACKSMITHING:

                //    return Skill_Blacksmith(stage);

                //case SKILL_TYPE.SKILL_BOWCRAFT:

                //    return Skill_Bowcraft(stage);

                //case SKILL_TYPE.SKILL_PEACEMAKING:

                //    return Skill_Peacemaking(stage);

                //case SKILL_TYPE.SKILL_CAMPING:

                //    return 0;

                //case SKILL_TYPE.SKILL_CARPENTRY:

                //    return Skill_Carpentry(stage);

                //case SKILL_TYPE.SKILL_CARTOGRAPHY:

                //    return Skill_Cartography(stage);

                //case SKILL_TYPE.SKILL_COOKING:

                //    return Skill_Cooking(stage);

                //case SKILL_TYPE.SKILL_DETECTINGHIDDEN:

                //    return Skill_DetectHidden(stage);

                //case SKILL_TYPE.SKILL_ENTICEMENT:

                //    return Skill_Enticement(stage);

                //case SKILL_TYPE.SKILL_EVALINT:

                //    return Skill_Information(stage);

                //case SKILL_TYPE.SKILL_HEALING:

                //    return Skill_Healing(stage);

                //case SKILL_TYPE.SKILL_FISHING:

                //    return Skill_Fishing(stage);

                //case SKILL_TYPE.SKILL_FORENSICS:

                //    return Skill_Information(stage);

                //case SKILL_TYPE.SKILL_HERDING:

                //    return Skill_Herding(stage);

                //case SKILL_TYPE.SKILL_HIDING:

                //    return Skill_Hiding(stage);

                //case SKILL_TYPE.SKILL_PROVOCATION:

                //    return Skill_Provocation(stage);

                //case SKILL_TYPE.SKILL_INSCRIPTION:

                //    return Skill_Inscription(stage);

                //case SKILL_TYPE.SKILL_LOCKPICKING:

                //    return Skill_Lockpicking(stage);

                case SKILL_TYPE.SKILL_MAGERY:

                    return Skill_Magery(stage);

                //case SKILL_TYPE.SKILL_MAGICRESISTANCE:

                //    return 0;

                //case SKILL_TYPE.SKILL_TACTICS:

                //    return 0;

                //case SKILL_TYPE.SKILL_SNOOPING:

                //    return Skill_Snooping(stage);

                //case SKILL_TYPE.SKILL_MUSICIANSHIP:

                //    return Skill_Musicianship(stage);

                //case SKILL_TYPE.SKILL_POISONING:   // 30

                //    return Skill_Poisoning(stage);

                //case SKILL_TYPE.SKILL_ARCHERY:

                //    return Skill_Fighting(stage);

                //case SKILL_TYPE.SKILL_SPIRITSPEAK:

                //    return Skill_SpiritSpeak(stage);

                //case SKILL_TYPE.SKILL_STEALING:

                //    return Skill_Stealing(stage);

                //case SKILL_TYPE.SKILL_TAILORING:

                //    return Skill_Tailoring(stage);

                //case SKILL_TYPE.SKILL_TAMING:

                //    return Skill_Taming(stage);

                //case SKILL_TYPE.SKILL_TASTEID:

                //    return Skill_Information(stage);

                //case SKILL_TYPE.SKILL_TINKERING:

                //    return Skill_MakeItem(stage);

                //case SKILL_TYPE.SKILL_TRACKING:

                //    return Skill_Tracking(stage);

                //case SKILL_TYPE.SKILL_VETERINARY:

                //    return Skill_Healing(stage);

                //case SKILL_TYPE.SKILL_SWORDSMANSHIP:

                //case SKILL_TYPE.SKILL_MACEFIGHTING:

                //case SKILL_TYPE.SKILL_FENCING:

                //case SKILL_TYPE.SKILL_WRESTLING:

                //    return Skill_Fighting(stage);

                //case SKILL_TYPE.SKILL_LUMBERJACKING:

                //    return Skill_Lumberjack(stage);

                //case SKILL_TYPE.SKILL_MINING:

                //    return Skill_Mining(stage);

                case SKILL_TYPE.SKILL_MEDITATION:

                    return Skill_Meditation(stage);

                //case SKILL_TYPE.SKILL_Stealth:

                //    return Skill_Hiding(stage);

                //case SKILL_TYPE.SKILL_RemoveTrap:

                //    return Skill_RemoveTrap(stage);

                //case SKILL_TYPE.SKILL_NECROMANCY:

                //    return Skill_Magery(stage);



                //case SKILL_TYPE.NPCACT_BREATH:

                //    return Skill_Act_Breath(stage);

                //case SKILL_TYPE.NPCACT_LOOTING:

                //    return Skill_Act_Looting(stage);

                //case SKILL_TYPE.NPCACT_THROWING:

                //    return Skill_Act_Throwing(stage);

                //case SKILL_TYPE.NPCACT_TRAINING:

                //    return Skill_Act_Training(stage);

                //case SKILL_TYPE.NPCACT_Napping:

                //    return Skill_Act_Napping(stage);



                default:
                    throw new NotImplementedException();

                    // TODO:
                    //if (!IsSkillBase(skill))

                    //{

                    //    DEBUG_CHECK(IsSkillNPC(skill));

                    //    if (stage == CSkillDef::T_Stroke)

                    //        return (-CSkillDef::T_Stroke); // keep these active. (NPC modes)

                    //    return 0;

                    //}
                    break;

            }


            throw new NotImplementedException();
            //WriteString("Skill not implemented!");

            //return -CSkillDef::T_QTY;

        }

        public int Skill_Magery(CSkillDef.T_TYPE_ stage)

        {

            // SKILL_MAGERY

            //  m_Act.m_pt = location to cast to.

            //  m_Act.m_TargPrv = the source of the spell.

            //  m_Act.m_Targ = target for the spell.

            //  m_atMagery.m_Spell = the spell.



            switch (stage)

            {

                case CSkillDef.T_TYPE_.T_Start:

                    // NOTE: this should call SetTimeout();

                    return Spell_CastStart();

                case CSkillDef.T_TYPE_.T_Stroke:

                    return (0);

                case CSkillDef.T_TYPE_.T_Fail:
                    throw new NotImplementedException();

                    //Spell_CastFail();

                    // return (0);

                case CSkillDef.T_TYPE_.T_Success:

                    if (!Spell_CastDone())

                    {

                        return (-(int)CSkillDef.T_TYPE_.T_Abort);

                    }

                    return (0);

            }



            ASSERT(0);

            return (-(int)CSkillDef.T_TYPE_.T_Abort);

        }

        public void Skill_Cleanup()

        {

            // We are done with the skill.

            // We may have succeeded, failed, or cancelled.

            m_Act.m_Difficulty = 0;

            m_Act.m_SkillCurrent = SKILL_TYPE.SKILL_NONE;

            //SetTimeout(m_pPlayer.IsValidNewObj() ? -1 : TICKS_PER_SEC); // we should get a brain tick next time.

        }

        public int Skill_Done()

        {

            // We just finished using a skill. ASYNC timer expired.

            // m_Act_Skill = the skill.

            // Consume resources that have not already been consumed.

            // Confer the benefits of the skill.

            // calc skill gain based on this.

            //

            // RETURN: Did we succeed or fail ?

            //   0 = success

            //	 -CSkillDef::T_Stroke = stay in skill. (stroke)

            //   -CSkillDef::T_Fail = we must print the fail msg. (credit for trying)

            //   -CSkillDef::T_Abort = we must print the fail msg. (But get no credit, canceled )

            //   -CSkillDef::T_QTY = special failure. clean up the skill but say nothing. (no credit)



            SKILL_TYPE skill = Skill_GetActive();

            if (skill == SKILL_TYPE.SKILL_NONE)    // we should not be coming here (timer should not have expired)

                return -(int)CSkillDef.T_TYPE_.T_QTY;



            // multi stroke tried stuff here first.

            // or stuff that never really fails.

            int iRet = Skill_Stage(CSkillDef.T_TYPE_.T_Stroke);

            if (iRet < 0)

                return (iRet);

            if (m_Act.m_Difficulty < 0 && !IsGM())

            {

                // Was Bound to fail. But we had to wait for the timer anyhow.

                return -(int)CSkillDef.T_TYPE_.T_Fail;

            }



            // Success for the skill.

            iRet = Skill_Stage(CSkillDef.T_TYPE_.T_Success);

            if (iRet < 0)

                return iRet;



            // Success = Advance the skill

            // TODO:
            // Skill_Experience(skill, m_Act.m_Difficulty);

            Skill_Cleanup();

            return (-(int)CSkillDef.T_TYPE_.T_Success);

        }

        public void Stat_Change(STAT_TYPE type, int iChange, int iLimit = 0)

        {

            // type = STAT_Health, STAT_Stam, and STAT_Mana

            switch (type)
            {
                case STAT_TYPE.STAT_Health:
                    if (iChange < 0)
                        mobile.Damage(-iChange);
                    else
                        throw new NotImplementedException();
                    break;
                case STAT_TYPE.STAT_Mana:
                    mobile.Mana += iChange;
                    break;
                default:
                    throw new NotImplementedException();
            }

            // TODO:
            //ASSERT(type >= STAT_TYPE.STAT_Health && type <= STAT_TYPE.STAT_Stam);

            //int iVal = m_Stat[(int)type];

            //STAT_TYPE typeLimit = (STAT_TYPE)(STAT_TYPE.STAT_MaxHealth + (type - STAT_TYPE.STAT_Health));



            //if (iChange != 0)

            //{

            //    if (iLimit == 0)

            //    {

            //        iLimit = Stat_Get(typeLimit);

            //    }

            //    if (iChange < 0)

            //    {

            //        iVal += iChange;

            //    }

            //    else if (iVal > iLimit)

            //    {

            //        iVal -= iChange;

            //        if (iVal < iLimit) iVal = iLimit;

            //    }

            //    else

            //    {

            //        iVal += iChange;

            //        if (iVal > iLimit) iVal = iLimit;

            //    }

            //    if (iVal < 0) iVal = 0;



            //    // Stat_Set()

            //    m_Stat[type] = iVal;

            //}



            //iLimit = Stat_Get(typeLimit);

            //if (iLimit < 0)

            //    iLimit = 0;



            //CUOCommand cmd;

            //cmd.StatChng.m_Cmd = XCMD_StatChngStr + (type - STAT_Health);

            //cmd.StatChng.m_UID = GetUID();



            //cmd.StatChng.m_max = iLimit;

            //cmd.StatChng.m_val = iVal;



            //if (IsClient()) // send this just to me

            //{

            //    m_pClient->xSendPkt(&cmd, sizeof(cmd.StatChng));

            //}



            //if (type == STAT_Health)    // everyone sees my health

            //{

            //    cmd.StatChng.m_max = 100;

            //    cmd.StatChng.m_val = GetHealthPercent();



            //    UpdateCanSee(&cmd, sizeof(cmd.StatChng), m_pClient);

            //}

        }

        public int Skill_Meditation(CSkillDef.T_TYPE_ stage)

        {

            // SKILL_MEDITATION

            // Try to regen your mana even faster than normal.

            // Give experience only when we max out.



            if (stage == CSkillDef.T_TYPE_.T_Fail || stage == CSkillDef.T_TYPE_.T_Abort)

            {

                return 0;

            }



            if (stage == CSkillDef.T_TYPE_.T_Start)

            {

                if (m_StatMana >= m_StatMaxMana)

                {

                    WriteString("You are at peace.");

                    return (-(int)CSkillDef.T_TYPE_.T_QTY);

                }

                m_Act.m_atTaming.m_Stroke_Count = 0;



                WriteString("You attempt a meditative trance.");



                return Calc_GetRandVal(100);    // how hard to get started ?

            }

            if (stage == CSkillDef.T_TYPE_.T_Stroke)

            {

                return 0;

            }

            if (stage == CSkillDef.T_TYPE_.T_Success)

            {

                if (m_StatMana >= m_StatMaxMana)

                {

                    WriteString("You are at peace.");

                    return (0); // only give skill credit now.

                }



                if (m_Act.m_atTaming.m_Stroke_Count == 0)

                {

                    Sound(0x0f9);

                }

                m_Act.m_atTaming.m_Stroke_Count++;



                Stat_Change(STAT_TYPE.STAT_Mana, 1);



                // next update. (depends on skill)

                Skill_SetTimeout();



                // Set a new possibility for failure ?

                // iDifficulty = Calc_GetRandVal(100);

                return (-(int)CSkillDef.T_TYPE_.T_Stroke);

            }



            DEBUG_CHECK(0);

            return (-(int)CSkillDef.T_TYPE_.T_QTY);

        }

        public void Skill_SetTimeout()

        {

            SKILL_TYPE skill = Skill_GetActive();

            ASSERT(IsSkillBase(skill));

            int iSkillLevel = Skill_GetBase(skill);

            int iDelay = g_Cfg.GetSkillDef(skill).m_Delay.GetLinear(iSkillLevel);

            SetTimeout(iDelay);
        }

        public bool Skill_CheckSuccess(SKILL_TYPE skill, int difficulty)

        {

            // PURPOSE:

            //  Check a skill for success or fail.

            //  DO NOT give experience here.

            // ARGS:

            //  difficulty = 0-100 = The point at which the equiv skill level has a 50% chance of success.

            // RETURN:

            //	true = success in skill.

            //



#if _0

// WESTY MOD

	// In the following, the return vlaue from the script is the success or fail,

	// not weather to override default processing or not



	// RES_Skill

	CSkillDefPtr pSkillDef = g_Cfg.GetSkillDef(skill);

	TRIGRET_TYPE iTrigRet = TRIGRET_RET_VAL;



	CSphereExpContext exec(this,this);



	if ( ! IsSkillBase(skill) || IsGM())

	{

		if( pSkillDef )

		{

			// Run the script and use the return value for our succeess or failure

			// RES_Skill



			CCharActState SaveState = m_Act;

			iTrigRet = pSkillDef->OnTriggerScript( exec, CSkillDef::T_CHECKSUCCESS, CSkillDef::sm_Triggers[CSkillDef::T_CHECKSUCCESS][0] );

			m_Act = SaveState;



			// GM's ALWAYS succeed, no matter what

			if ( IsGM())

				return( true );

			if ( pSkillDef->HasTrigger( CSkillDef::T_CHECKSUCCESS ))

			{

				if( iTrigRet == TRIGRET_RET_FALSE )

					return( false );

			}

		}

		return( true );

	}



	// RES_Skill



	if ( pSkillDef )

	{

		CCharActState SaveState = m_Act;

		iTrigRet = pSkillDef->OnTriggerScript( exec, CSkillDef::T_CHECKSUCCESS, CSkillDef::sm_Triggers[CSkillDef::T_CHECKSUCCESS][0] );

		m_Act = SaveState;

	}



	if( pSkillDef )

	{

		// return whatever the trigger returned

		if( iTrigRet == TRIGRET_RET_VAL )

			return( true );

		if( pSkillDef->HasTrigger( CSkillDef::T_CHECKSUCCESS ) && iTrigRet == TRIGRET_RET_FALSE )

			return( false );

		// fall through for default (end of script, no return???)

	}

// WESTY MOD

#endif



            // Either no script, or TRIGRET_RET_DEFAULT was returned, procede with default

            bool fResult = g_Cfg.Calc_SkillCheck(Skill_GetAdjusted(skill), difficulty);

            if (fResult)

            {

                return (true);

            }

            return false;

        }

    }
}
