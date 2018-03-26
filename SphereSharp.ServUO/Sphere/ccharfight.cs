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
        public int OnTakeDamage(int iDmg, CChar pSrc, DAMAGE_TYPE uType)

        {

            // Someone or something hit us.

            // NOTE: There is NO reciprocation here.

            // Pre- armor absorb calc.

            //

            // uType =

            //	DAMAGE_GOD		0x01	// Nothing can block this.

            //	DAMAGE_HIT_BLUNT		0x02	// Physical hit of some sort.

            //	DAMAGE_MAGIC	0x04	// Magic blast of some sort. (we can be immune to magic to some extent)

            //	DAMAGE_POISON	0x08	// Or biological of some sort ? (HARM spell)

            //	DAMAGE_FIRE		0x10	// Fire damage of course.  (Some creatures are immune to fire)

            //	DAMAGE_ELECTRIC 0x20	// lightning.

            //  DAMAGE_DRAIN

            //	DAMAGE_GENERAL	0x80	// All over damage. As apposed to hitting just one point.

            //

            // RETURN: 

            //  health points damage actually done to me (not to my armor).

            //  -1 = already dead = invalid target. 

            //   0 = no damage. 

            //  INT_MAX = killed.



            if (iDmg <= 0)

                return (0);

            if (pSrc == NULL)   // done by myself i suppose.

                pSrc = this;


            // TODO:
            //if (pSrc != this)   // this could be an infinite loop if called in @GetHit

            //{

            //    CSphereExpArgs execArgs(this, pSrc, iDmg, uType, NULL );

            //    if (OnTrigger(CCharDef::T_GetHit, execArgs) == TRIGRET_RET_VAL)

            //        return (0);

            //}



            //if (uType & (DAMAGE_ELECTRIC | DAMAGE_HIT_BLUNT | DAMAGE_HIT_PIERCE | DAMAGE_HIT_SLASH | DAMAGE_FIRE | DAMAGE_MAGIC))

            //{

            //    StatFlag_Clear(STATF_Freeze);   // remove paralyze. OnFreezeCheck();

            //}



            if ((uType & DAMAGE_MAGIC) && pSrc != this)

            {

                // The damage gets scaled based on the mage�s Evaluate Intelligence

                // and the victim�s Resisting Spells skill. The scaling can range in intensity

                // based on the difference between these two skills. If Evaluate Intelligence is

                // greater than Resisting Spells, then full damage is taken.

                int iSrcEvalInt = pSrc.Skill_GetAdjusted(SKILL_TYPE.SKILL_EVALINT);

                int iMyResist = Skill_GetAdjusted(SKILL_TYPE.SKILL_MAGICRESISTANCE);

                int iDelta = iSrcEvalInt - iMyResist;

                int iDivisor = iDelta > 0 ? 5000 : 2000;

                double dPercent = (double)iDelta / (double)iDivisor;

                iDmg = iDmg + (int)(iDmg * dPercent);

            }



            //CCharDefPtr pCharDef = Char_GetDef();

            //ASSERT(pCharDef);



            if (uType & (DAMAGE_HIT_BLUNT | DAMAGE_HIT_PIERCE | DAMAGE_HIT_SLASH))

            {

                // A physical blow of some sort.

                // Try to allow the armor or shield to take some damage.

                //Reveal();



                // Check for reactive armor.

                //if (IsStatFlag(STATF_Reactive) && !(uType & DAMAGE_GOD))

                //{

                //    // reflect some damage back.

                //    if (pSrc && GetTopDist3D(pSrc) <= 2)

                //    {

                //        // ???

                //        // Reactive armor Spell strength is NOT the same as MAGERY !!!???

                //        int iSkillVal = Skill_GetAdjusted(SKILL_MAGERY);

                //        int iEffect = g_Cfg.GetSpellEffect(SPELL_Reactive_Armor, iSkillVal);

                //        int iRefDam = Calc_GetRandVal(IMULDIV(iDmg, iEffect, 1000));

                //        iDmg -= iRefDam;

                //        pSrc->OnTakeDamage(iRefDam, this, uType);

                //        pSrc->Effect(EFFECT_OBJ, ITEMID_FX_CURSE_EFFECT, this, 9, 6);

                //    }

                //}



                // absorbed by armor ?

                //if (!(uType & DAMAGE_GENERAL))

                //{

                //    iDmg = OnTakeDamageHitPoint(iDmg, pSrc, uType);

                //    iDmg -= pCharDef->m_armor.GetRandom();

                //}

                //else if (!(uType & DAMAGE_GOD))

                //{

                //    // general overall damage.

                //    iDmg -= Calc_GetRandVal(m_ArmorDisplay);

                //    // ??? take some random damage to my equipped items.

                //}

            }



            //if (IsStatFlag(STATF_INVUL))

            //{

            //    effect_bounce:

            //    if (iDmg)

            //    {

            //        Effect(EFFECT_OBJ, ITEMID_FX_GLOW, this, 9, 30, false);

            //    }

            //    iDmg = 0;

            //}

            //else if (!(uType & DAMAGE_GOD))

            //{

            //    if (m_pArea.IsValidRefObj())

            //    {

            //        if (m_pArea->IsFlag(REGION_FLAG_SAFE))

            //            goto effect_bounce;

            //        if (m_pArea->IsFlag(REGION_FLAG_NO_PVP) &&

            //            m_pPlayer.IsValidNewObj() &&

            //            pSrc &&

            //            pSrc->m_pPlayer.IsValidNewObj())

            //            goto effect_bounce;

            //    }

            //    if (IsStatFlag(STATF_Stone))    // can't hurt us anyhow.

            //    {

            //        goto effect_bounce;

            //    }

            //}



            //if (m_StatHealth <= 0)  // Already dead.

            //    return (-1);



            //if (uType & DAMAGE_FIRE)

            //{

            //    if (pCharDef->Can(CAN_C_FIRE_IMMUNE)) // immune to the fire part.

            //    {

            //        // If there is any other sort of damage then dish it out as well.

            //        if (!(uType & (DAMAGE_HIT_BLUNT | DAMAGE_HIT_PIERCE | DAMAGE_HIT_SLASH | DAMAGE_POISON | DAMAGE_ELECTRIC)))

            //            return (0); // No effect.

            //        iDmg /= 2;

            //    }

            //}



            //// defend myself. (even though it may not have hurt me.)

            //if (!OnAttackedBy(pSrc, iDmg, false))

            //    return (0);



            // Did it hurt ?

            if (iDmg <= 0)

                return (0);



            // Make blood depending on hit damage. assuming the creature has blood

            ITEMID_TYPE id = ITEMID_TYPE.ITEMID_NOTHING;

            //if (pCharDef->m_wBloodHue != (HUE_TYPE) - 1)

            //{

            //    if (iDmg > 10)

            //    {

            //        id = (ITEMID_TYPE)(ITEMID_BLOOD1 + Calc_GetRandVal(ITEMID_BLOOD6 - ITEMID_BLOOD1));

            //    }

            //    else if (Calc_GetRandVal(iDmg) > 5)

            //    {

            //        id = ITEMID_BLOOD_SPLAT;    // minor hit.

            //    }

            //    if (id &&

            //        !IsStatFlag(STATF_Conjured) &&

            //        (uType & (DAMAGE_HIT_PIERCE | DAMAGE_HIT_SLASH)))   // A blow of some sort.

            //    {

            //        CItemPtr pBlood = CItem::CreateBase(id);

            //        ASSERT(pBlood);

            //        pBlood->SetHue(pCharDef->m_wBloodHue);

            //        pBlood->MoveToDecay(GetTopPoint(), 7 * TICKS_PER_SEC);

            //    }

            //}



            Stat_Change(STAT_TYPE.STAT_Health, -iDmg);

            //if (m_StatHealth <= 0)

            //{

            //    // We will die from this...make sure the killer is set correctly...if we don't do this, the person we are currently

            //    // attacking will get credit for killing us.

            //    // Killed by a guard looks here !

            //    if (pSrc)

            //    {

            //        m_Act.m_Targ = pSrc->GetUID();

            //    }

            //    return (-1);    // INT_MAX ?

            //}



            //SoundChar(CRESND_GETHIT);

            //if (m_Act.m_atFight.m_War_Swing_State != WAR_SWING_SWINGING)    // Not interrupt my swing.

            //{

            //    UpdateAnimate(ANIM_GET_HIT);

            //}

            return (iDmg);

        }

    }
}
