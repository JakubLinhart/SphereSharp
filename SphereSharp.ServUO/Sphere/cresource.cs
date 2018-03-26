using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.ServUO.Sphere._Global;

namespace SphereSharp.ServUO.Sphere
{
    public static partial class _Global
    {
        public const int STAT_BASE_QTY = 3;	// STAT_Str to STAT_Dex
        public const int STAT_REGEN_QTY = 4;
        public const int STAT_QTY = 12;

        public const int DAMAGE_GOD = 0x0001;	// Nothing can block this.
        public const int DAMAGE_HIT_BLUNT = 0x0002;	// Physical hit of some sort.
        public const int DAMAGE_MAGIC = 0x0004;	// Magic blast of some sort. (we can be immune to magic to some extent)
        public const int DAMAGE_POISON = 0x0008;	// Or biological of some sort ? (HARM spell)
        public const int DAMAGE_FIRE = 0x0010;	// Fire damage of course.  (Some creatures are immune to fire)
        public const int DAMAGE_ELECTRIC = 0x0020;	// lightning.
        public const int DAMAGE_DRAIN = 0x0040;	// level drain = negative energy.
        public const int DAMAGE_GENERAL = 0x0080;	// All over damage. As apposed to hitting just one point.
        public const int DAMAGE_ACID = 0x0100;	// Corrosive will destroy armor.
        public const int DAMAGE_COLD = 0x0200;	// Cold freezing damage
        public const int DAMAGE_HIT_SLASH = 0x0400;	// sword
        public const int DAMAGE_HIT_PIERCE = 0x0800;	// spear.
        public const int DAMAGE_HOLY = 0x1000;	// Only does damage to evil
    }

    public class CSpellDef
    {
        public string m_sTargetPrompt;

        public bool IsSpellType(WORD wFlags)

        {

            return ((m_wFlags & wFlags) ? true : false);

        }

        public int SkillReqQty => 20;
        public string m_sRunes => "AM";
        public int m_iCastTime => 1200;
        public WORD m_wFlags = SPELLFLAG_DIR_ANIM | SPELLFLAG_FX_TARG;

        public ITEMID_TYPE m_idEffect => ITEMID_TYPE.ITEMID_FX_CURSE_EFFECT;		// Animation effect ID

    }

    public class CSpellDefPtr : CSpellDef
    {
    }

    public class CSkillDef
    {
        public enum T_TYPE_
        {
            T_Abort,
            T_Fail,
            T_MakeItem,
            T_Select,
            T_Start,
            T_Stroke,
            T_Success,
            T_QTY,
        }

        public BYTE[] m_StatBonus = new BYTE[STAT_BASE_QTY]; // % of each stat toward success at skill, total 100
        public BYTE m_StatPercent;
    }

    public class CSkillDefPtr : CSkillDef
    {
    }

    public struct STAT_LEVEL
    {
        public short Value { get; }

        public STAT_LEVEL(short val)
        {
            Value = val;
        }

        public static implicit operator STAT_LEVEL(short val) => new STAT_LEVEL(val);
        public static implicit operator STAT_LEVEL(int val) => new STAT_LEVEL((short)val);
        public static implicit operator SKILL_LEVEL(STAT_LEVEL val) => new SKILL_LEVEL(val.Value);
        public static implicit operator int(STAT_LEVEL val) => val.Value;
        public static implicit operator bool(STAT_LEVEL val) => val.Value != 0;

        public static STAT_LEVEL operator &(STAT_LEVEL val1, STAT_LEVEL val2) => val1.Value & val2.Value;
    }

    public enum STAT_TYPE  // Standard Character stats. 

    {

        // All chars have all stats. (as opposed to skills which not all will have)

        STAT_Str = 0,

        STAT_Int,

        STAT_Dex,



//#define STAT_BASE_QTY	3	// STAT_Str to STAT_Dex

        STAT_Health,    // How damaged are we ?

        STAT_Mana,      // How drained of mana are we ?

        STAT_Stam,      // How tired are we ?

        STAT_Food,      // Food level (.5 days food level at normal burn)



//#define STAT_REGEN_QTY 4

        STAT_Fame,      // How much hard stuff have u done ? Degrades over time???

        STAT_Karma,     // -10000 to 10000 = how good are you ?



        STAT_MaxHealth,

        STAT_MaxMana,

        STAT_MaxStam,



        // STAT_SpellFail - as a percent.

        // STAT_AttackLo - low attack

        // STAT_AttackHi

        // STAT_AttackSpeed



//        STAT_QTY,

    };


}
