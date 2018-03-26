using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public class CCharActState
    {
        public class AtMagery
        {


            public SPELL_TYPE m_Spell;     // ACTARG1=Currently casting spell.

            public CREID_TYPE m_SummonID;      // ACTARG2=A sub arg of the skill. (summoned type ?)

            public bool m_fSummonPet;          // ACTARG3=

        }

        public class AtTaming
        {
            public DWORD m_junk1;
            public WORD m_Stroke_Count;
        }

        public AtTaming m_atTaming = new AtTaming();
        public AtMagery m_atMagery = new AtMagery();
        public CSphereUID m_TargPrv;		// Targeted bottle for alchemy or previous beg target.
        public CSphereUID m_Targ;			// Current combat/action target
        public CPointMap m_pt;          // Moving to this location. or location of forge we are working on.
        public SKILL_TYPE m_SkillCurrent;       // Currently using a skill. Could be combat skill.
        public SKILL_LEVEL m_Difficulty;	// -1 = fail skill. (0-100) for skill advance calc.
    }

    public enum SPELL_TYPE
    {
        SPELL_NONE = 0,



        SPELL_Clumsy = 1,       // just reduces the Dex.

        SPELL_Create_Food,

        SPELL_Feeblemind,

        SPELL_Heal,

        SPELL_Magic_Arrow,

        SPELL_Night_Sight,  // 6

        SPELL_Reactive_Armor,

        SPELL_Weaken,



        // 2nd

        SPELL_Agility,  // 9

        SPELL_Cunning,

        SPELL_Cure,

        SPELL_Harm,

        SPELL_Magic_Trap,

        SPELL_Magic_Untrap,

        SPELL_Protection,

        SPELL_Strength, // 16



        // 3rd

        SPELL_Bless,        // 17

        SPELL_Fireball,

        SPELL_Magic_Lock,

        SPELL_Poison,

        SPELL_Telekin,

        SPELL_Teleport,

        SPELL_Unlock,

        SPELL_Wall_of_Stone,    // 24



        // 4th

        SPELL_Arch_Cure,    // 25

        SPELL_Arch_Prot,

        SPELL_Curse,

        SPELL_Fire_Field,

        SPELL_Great_Heal,

        SPELL_Lightning,    // 30

        SPELL_Mana_Drain,

        SPELL_Recall,



        // 5th

        SPELL_Blade_Spirit,

        SPELL_Dispel_Field,

        SPELL_Incognito,        // 35

        SPELL_Magic_Reflect,

        SPELL_Mind_Blast,

        SPELL_Paralyze,

        SPELL_Poison_Field,

        SPELL_Summon,           // 40



        // 6th

        SPELL_Dispel,

        SPELL_Energy_Bolt,

        SPELL_Explosion,

        SPELL_Invis,

        SPELL_Mark,

        SPELL_Mass_Curse,

        SPELL_Paralyze_Field,

        SPELL_Reveal,



        // 7th

        SPELL_Chain_Lightning,

        SPELL_Energy_Field,

        SPELL_Flame_Strike,

        SPELL_Gate_Travel,

        SPELL_Mana_Vamp,        // 53

        SPELL_Mass_Dispel,

        SPELL_Meteor_Swarm, // 55

        SPELL_Polymorph,



        // 8th

        SPELL_Earthquake,

        SPELL_Vortex,

        SPELL_Resurrection, // 59

        SPELL_Air_Elem,

        SPELL_Daemon,

        SPELL_Earth_Elem,

        SPELL_Fire_Elem,

        SPELL_Water_Elem,   // 64

        SPELL_BOOK_QTY = 65,        // Thats all that is in the standard spell book.



        // -------------------------------

        // spells and effects not in the client spellbook.



        // Necro

        SPELL_Summon_Undead = 65,

        SPELL_Animate_Dead,

        SPELL_Bone_Armor,

        SPELL_Light,

        SPELL_Fire_Bolt,        // 69

        SPELL_Hallucination,    // 70

        SPELL_BASE_QTY,     // monsters can use these.



        // Extra special spells. (can be used as potion effects as well)

        SPELL_Stone = 71,       // 71 // Turn to stone. (permanent)

        SPELL_Shrink,           // 72 // turn pet into icon.

        SPELL_Refresh,          // 73 // stamina

        SPELL_Restore,          // 74 // This potion increases both your hit points and your stamina.

        SPELL_Mana,             // 75 // restone mana

        SPELL_Sustenance,       // 76 // serves to fill you up. (Remember, healing rate depends on how well fed you are!)

        SPELL_Chameleon,        // 77 // makes your skin match the colors of whatever is behind you.

        SPELL_BeastForm,        // 78 // polymorphs you into an animal for a while.

        SPELL_Monster_Form,     // 79 // polymorphs you into a monster for a while.

        SPELL_Forget,           // 80 // permanently lowers one skill.

        SPELL_Gender_Swap,      // 81 // permanently changes your gender.

        SPELL_Trance,           // 82 // temporarily increases your meditation skill.

        SPELL_Particle_Form,    // 83 // turns you into an immobile, but untargetable particle system for a while.

        SPELL_Shield,           // 84 // erects a temporary force field around you. Nobody approaching will be able to get within 1 tile of you, though you can move close to them if you wish.

        SPELL_Steelskin,        // 85 // turns your skin into steel, giving a boost to your AR.

        SPELL_Stoneskin,        // 86 // turns your skin into stone, giving a boost to your AR.

        SPELL_Regenerate,       // 87 // regen hitpoints at a fast rate.

        SPELL_Enchant,          // 88 // Enchant an item (weapon or armor)

        SPELL_Water,            // 89 // all liquids can act as spells/potions.

        SPELL_Ale,              // 90 = drunkeness ?

        SPELL_Wine,             // 91 = mild drunkeness ?

        SPELL_Liquor,           // 92 = extreme drunkeness ?

        SPELL_Blood,

        SPELL_Milk,

        SPELL_Oil,

        SPELL_Criminal,         // Makes one a criminal.

        SPELL_Targetted,        // Makes one a universal target.

        SPELL_Peaceful,         // Makes you very peaceful.

        SPELL_Scare,            // Make you fearful.

        SPELL_SpiritSpeak,

        SPELL_Confused,         // May hit others ?

        SPELL_Beserk,           // May hit all around you.



        SPELL_Qty,

    }

    public static class g_Cfg
    {
        internal static CSpellDefPtr GetSpellDef(SPELL_TYPE iSpell)
        {
            return new CSpellDefPtr();
        }

        private static string[] runes = new[]
        {
            "Ruth",
            "Er",
            "Mor",
            "Curu",
            "Dol",
            "Ruin",
            "Esgal",
            "Sul",
            "Anna",
            "Del",
            "Heru",
            "Fuin",
            "Aina",
            "Sereg",
            "Morgul",
            "Kel",
            "Gor",
            "Faroth",
            "Tir",
            "Barad",
            "Ril",
            "Beleg",
            "Loth",
            "Val",
            "Kemen",
            "Fea",
        };

        internal static string GetRune(char ch)
        {
            int ich = char.ToUpper(ch) - 'A';

            if (ich > runes.Length || ich < 0)
                return "?";

            return runes[ich];
        }

        public static CSkillDefPtr GetSkillDef(SKILL_TYPE skill)
        {
            var skillDef = new CSkillDefPtr();

            skillDef.m_StatBonus[(int)STAT_TYPE.STAT_Str] = 0;
            skillDef.m_StatBonus[(int)STAT_TYPE.STAT_Int] = 0;
            skillDef.m_StatBonus[(int)STAT_TYPE.STAT_Dex] = 0;
            skillDef.m_StatPercent = 0;

            return skillDef;
        }

        internal static int GetSpellEffect(SPELL_TYPE spell, int iSkillLevel)
        {
            // TODO:
            return 10;
        }
    }

    public class CCharPtr : CChar
    {
        public CCharPtr(Server.Mobile mobile) : base(mobile)
        {
        }
    }

    public partial class CClient
    {
        public CCharPtr m_pChar;			// What char are we playing ?

        public HRESULT Cmd_Skill_Magery(SPELL_TYPE iSpell, CObjBase pSrc)

        {

            // start casting a spell. prompt for target.

            // pSrc = you the char.

            // pSrc = magic object is source ?



            string sm_Txt_Summon = "Where would you like to summon the creature ?";



            CSpellDefPtr pSpellDef = g_Cfg.GetSpellDef(iSpell);

            if (pSpellDef == NULL)

                return (HRES_INVALID_INDEX);



            // Do we have the regs ? etc.

            ASSERT(m_pChar);

            if (!m_pChar.Spell_CanCast(iSpell, true, pSrc, true))

                return HRES_INVALID_HANDLE;



            // DEBUG_TRACE(("%x:Cast Spell %d='%s'" LOG_CR, m_Socket.GetSocket(), iSpell, (LPCTSTR)pSpellDef->GetName()));



            //if (g_Cfg.m_iPreCastTime)

            //{



            //}



            SetTargMode();

            m_Targ.m_tmSkillMagery.m_Spell = iSpell;    // m_Act.m_atMagery.m_Spell

            m_Targ.m_UID = m_pChar.GetUID();   // default target = self

            m_Targ.m_PrvUID = pSrc.GetUID();   // source of the spell.



            string pPrompt = "Select Target";

            switch (iSpell)

            {

                case SPELL_TYPE.SPELL_Recall:

                    pPrompt = "Select rune to recall from.";

                    break;

                case SPELL_TYPE.SPELL_Blade_Spirit:

                    pPrompt = sm_Txt_Summon;

                    break;

                case SPELL_TYPE.SPELL_Summon:
                    throw new NotImplementedException();

                    // return (Cmd_Skill_Menu(g_Cfg.ResourceGetIDType(RES_SkillMenu, "sm_summon")));



                case SPELL_TYPE.SPELL_Mark:

                    pPrompt = "Select rune to mark.";

                    break;

                case SPELL_TYPE.SPELL_Gate_Travel: // gate travel

                    pPrompt = "Select rune to gate from.";

                    break;

                case SPELL_TYPE.SPELL_Polymorph:

                    throw new NotImplementedException();
                    // polymorph creature menu.

                    //if (IsPrivFlag(PRIV_GM))

                    //{

                    //    pPrompt = "Select creature to polymorph.";

                    //    break;

                    //}

                    //return (Cmd_Skill_Menu(g_Cfg.ResourceGetIDType(RES_SkillMenu, "sm_polymorph")));



                case SPELL_TYPE.SPELL_Earthquake:

                    throw new NotImplementedException();
                    // cast immediately with no targeting.

                    //m_pChar->m_Act.m_atMagery.m_Spell = SPELL_Earthquake;

                    //m_pChar->m_Act.m_Targ = m_Targ.m_UID;

                    //m_pChar->m_Act.m_TargPrv = m_Targ.m_PrvUID;

                    //m_pChar->m_Act.m_pt = m_pChar->GetTopPoint();

                    //if (!m_pChar->Skill_Start(SKILL_MAGERY))

                    //{

                    //    return HRES_INVALID_HANDLE;

                    //}

                    //return NO_ERROR;



                case SPELL_TYPE.SPELL_Resurrection:

                    pPrompt = "Select ghost to resurrect.";

                    break;

                case SPELL_TYPE.SPELL_Vortex:

                case SPELL_TYPE.SPELL_Air_Elem:

                case SPELL_TYPE.SPELL_Daemon:

                case SPELL_TYPE.SPELL_Earth_Elem:

                case SPELL_TYPE.SPELL_Fire_Elem:

                case SPELL_TYPE.SPELL_Water_Elem:

                    pPrompt = sm_Txt_Summon;

                    break;



                // Necro spells

                case SPELL_TYPE.SPELL_Summon_Undead: // Summon an undead

                    pPrompt = sm_Txt_Summon;

                    break;

                case SPELL_TYPE.SPELL_Animate_Dead: // Corpse to zombie

                    pPrompt = "Choose a corpse";

                    break;

                case SPELL_TYPE.SPELL_Bone_Armor: // Skeleton corpse to bone armor

                    pPrompt = "Chose a skeleton";

                    break;

            }



            if (!string.IsNullOrEmpty(pSpellDef.m_sTargetPrompt))

            {

                pPrompt = pSpellDef.m_sTargetPrompt;

            }



            addTarget(CLIMODE_TYPE.CLIMODE_TARG_SKILL_MAGERY, pPrompt,

                !pSpellDef.IsSpellType(SPELLFLAG_TARG_OBJ | SPELLFLAG_TARG_CHAR),

                pSpellDef.IsSpellType(SPELLFLAG_HARM));

            return (NO_ERROR);

        }

    }
}
