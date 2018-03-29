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
        public bool Spell_CanCast(SPELL_TYPE spell, bool fTest, CObjBase pSrc, bool fFailMsg)

        {

            // ARGS:

            //  pSrc = possible scroll or wand source.

            // Do we have enough mana to start ?

            //if (spell <= SPELL_NONE ||

            //    pSrc == NULL)

            //    return (false);



            CSpellDefPtr pSpellDef = g_Cfg.GetSpellDef(spell);

            if (pSpellDef == NULL)

                return (false);

            //if (pSpellDef->IsSpellType(SPELLFLAG_DISABLED))

            //    return (false);



            //// if ( ! fTest || m_pNPC.IsValidRefObj() )

            //{

            //    if (!IsGM() &&

            //        m_pArea.IsValidRefObj() &&

            //        m_pArea->CheckAntiMagic(spell))

            //    {

            //        if (fFailMsg)

            //            WriteString("An anti-magic field disturbs the spells.");

            //        m_Act.m_Difficulty = -1;    // Give very little credit for failure !

            //        return (false);

            //    }

            //}



            int wManaUse = pSpellDef.m_wManaUse;



            //// The magic item must be on your person to use.

            //if (pSrc != this)

            //{

            //    CItemPtr pItem = PTR_CAST(CItem, pSrc);

            //    if (pItem == NULL)

            //    {

            //        DEBUG_CHECK(0);

            //        return (false); // where did it go ?

            //    }

            //    if (!pItem->IsAttr(ATTR_MAGIC))

            //    {

            //        if (fFailMsg)

            //            WriteString("This item lacks any enchantment.");

            //        return (false);

            //    }

            //    CObjBasePtr pObjTop = pSrc->GetTopLevelObj();

            //    if (pObjTop != this)

            //    {

            //        if (fFailMsg)

            //            WriteString("Magic items must be on your person to activate.");

            //        return (false);

            //    }

            //    if (pItem->IsType(IT_WAND))

            //    {

            //        // Must have charges.

            //        if (pItem->m_itWeapon.m_spellcharges <= 0)

            //        {

            //            // ??? May explode !!

            //            if (fFailMsg)

            //                WriteString("It seems to be out of charges");

            //            return false;

            //        }

            //        wManaUse = 0;   // magic items need no mana.

            //        if (!fTest && pItem->m_itWeapon.m_spellcharges != 255)

            //        {

            //            pItem->m_itWeapon.m_spellcharges--;

            //        }

            //    }

            //    else    // Scroll

            //    {

            //        wManaUse /= 2;

            //        if (!fTest)

            //        {

            //            pItem->ConsumeAmount();

            //        }

            //    }

            //}

            //else

            //{

            //    // Raw cast from spellbook.



            //    if (IsGM())

            //        return (true);



            //    if (IsStatFlag(STATF_DEAD | STATF_Sleeping) ||

            //        !pSpellDef->m_SkillReq.IsResourceMatchAll(this))

            //    {

            //        if (fFailMsg)

            //            WriteString("This is beyond your ability.");

            //        return (false);

            //    }



            //    if (m_pPlayer.IsValidNewObj())

            //    {

            //        // check the spellbook for it.

            //        CItemPtr pBook = ContentFind(CSphereUID(RES_TypeDef, IT_SPELLBOOK), spell, 20);

            //        if (pBook == NULL)

            //        {

            //            if (fFailMsg)

            //                WriteString("You don't know that spell.");

            //            return (false);

            //        }

            //    }

            //}



            //if (m_StatMana < wManaUse)

            //{

            //    if (fFailMsg)

            //        WriteString("You lack sufficient mana for this spell");

            //    return (false);

            //}



            if (!fTest && wManaUse > 0)

            {

                // Consume mana.

                if (m_Act.m_Difficulty < 0) // use diff amount of mana if we fail.

                {

                    wManaUse = wManaUse / 2 + Calc_GetRandVal(wManaUse / 2 + wManaUse / 4);

                }

                Stat_Change(STAT_TYPE.STAT_Mana, -wManaUse);

            }



            //if (m_pNPC.IsValidNewObj() ||   // NPC's don't need regs.

            //    pSrc != this)   // wands and scrolls have there own reags source.

            //    return (true);



            //// Check for regs ?

            //if (g_Cfg.m_fReagentsRequired)

            //{

            //    CItemContainerPtr pPack = GetPack();

            //    if (pPack)

            //    {

            //        const CResourceQtyArray* pRegs = &(pSpellDef->m_Reags);

            //        int iMissing = pPack->ResourceConsumePart(pRegs, 1, 100, fTest);

            //        if (iMissing >= 0)

            //        {

            //            if (fFailMsg)

            //            {

            //                CResourceDefPtr pReagDef = g_Cfg.ResourceGetDef(pRegs->ConstElementAt(iMissing).GetResourceID());

            //                Printf("You lack %s for this spell", pReagDef ? (LPCTSTR)pReagDef->GetName() : "reagents");

            //            }

            //            return (false);

            //        }

            //    }

            //}

            return (true);

        }

        public int Spell_CastStart()

        {

            // Casting time goes up with difficulty

            // but down with skill, int and dex

            // ARGS:

            //  m_Act.m_pt = location to cast to.

            //  m_Act.m_atMagery.m_Spell = the spell.

            //  m_Act.m_TargPrv = the source of the spell.

            //  m_Act.m_Targ = target for the spell.

            // RETURN:

            //  0-100

            //  -1 = instant failure.



            // TODO:
            //if (!g_Cfg.m_iPreCastTime && IsClient())

            //{

            //    if (!Spell_TargCheck())

            //        return (-1);

            //}



            // Animate casting.

            CSpellDefPtr pSpellDef = g_Cfg.GetSpellDef(m_Act.m_atMagery.m_Spell);

            if (pSpellDef == NULL)

                return (-1);



            //UpdateAnimate((pSpellDef->IsSpellType(SPELLFLAG_DIR_ANIM)) ? ANIM_CAST_DIR : ANIM_CAST_AREA);


            bool fWOP = true;
            // TODO:
            //bool fWOP = (GetPrivLevel() >= PLEVEL_Seer) ?

            //    g_Cfg.m_fWordsOfPowerStaff :

            //    g_Cfg.m_fWordsOfPowerPlayer;



            //if (!NPC_CanSpeak() || IsStatFlag(STATF_Insubstantial))

            //{

            //    fWOP = false;

            //}


            int iDifficulty = pSpellDef.SkillReqQty / 10;



            //CSphereUID uid(m_Act.m_TargPrv );

            //CItemPtr pItem = g_World.ItemFind(uid);

            //if (pItem != NULL)

            //{

            //    if (pItem->IsType(IT_WAND))

            //    {

            //        // Wand use no words of power. and require no magery.

            //        fWOP = false;

            //        iDifficulty = 1;

            //    }

            //    else

            //    {

            //        // Scroll

            //        iDifficulty /= 2;

            //    }

            //}



            //if (!g_Cfg.m_fEquippedCast && fWOP)

            //{

            //    // Attempt to Unequip stuff before casting.

            //    // Except not wands and spell books !

            //    if (!Spell_Unequip(LAYER_HAND1))

            //        return (-1);

            //    if (!Spell_Unequip(LAYER_HAND2))

            //        return (-1);

            //}



            if (fWOP)

            {

                int len = 0;

                var szTemp = new StringBuilder();

                int i = 0;
                for (; i < pSpellDef.m_sRunes.Length; i++)

                {

                    char ch = pSpellDef.m_sRunes[i];

                    szTemp.Append(g_Cfg.GetRune(ch));

                    szTemp.Append(' ');

                }

                if (i > 0)

                {

                    Speak(szTemp.ToString());

                }

            }



            //CSphereExpArgs Args(this, this, (int) m_Act.m_atMagery.m_Spell, iDifficulty, pItem );

            //if (OnTrigger(CCharDef::T_SpellCast, Args) == TRIGRET_RET_VAL)

            //    return (-1);



            int iWaitTime;

            //if (IsGM())

            //    iWaitTime = 1;

            //else

            iWaitTime = pSpellDef.m_iCastTime;



            SetTimeout(iWaitTime);



            return (iDifficulty);

        }

        public bool Spell_CastDone()

        {

            // Spell_CastDone

            // Ready for the spell effect.

            // m_Act.m_TargPrv = spell was magic item or scroll ?

            // RETURN:

            //  false = fail.

            // ex. magery skill goes up FAR less if we use a scroll or magic device !

            //


            // TODO:
            //if (!Spell_TargCheck()) // check targ one last time.

            //    return (false);



            CObjBasePtr pObj = g_World.ObjFind(m_Act.m_Targ);   // dont always need a target.

            CObjBasePtr pObjSrc = g_World.ObjFind(m_Act.m_TargPrv);



            SPELL_TYPE spell = m_Act.m_atMagery.m_Spell;

            CSpellDefPtr pSpellDef = g_Cfg.GetSpellDef(spell);

            if (pSpellDef == NULL)

                return (false);



            int iSkillLevel;

            //if (pObjSrc != this)

            //{

            //    // Get the strength of the item. IT_SCROLL or IT_WAND

            //    CItemPtr pItem = REF_CAST(CItem, pObjSrc);

            //    if (pItem == NULL)

            //        return (false);

            //    if (!pItem->m_itWeapon.m_spelllevel)

            //        iSkillLevel = Calc_GetRandVal(500);

            //    else

            //        iSkillLevel = pItem->m_itWeapon.m_spelllevel;

            //    if (pItem->IsAttr(ATTR_CURSED | ATTR_CURSED2))

            //    {

            //        // do something bad.

            //        spell = SPELL_Curse;

            //        pSpellDef = g_Cfg.GetSpellDef(SPELL_Curse);

            //        pItem->SetAttr(ATTR_IDENTIFIED);

            //        pObj = this;

            //        WriteString("Cursed Magic!");

            //    }

            //}

            //else

            //{

            iSkillLevel = Skill_GetAdjusted(SKILL_TYPE.SKILL_MAGERY);

            //}



            // Consume the reagents/mana/scroll/charge

            if (!Spell_CanCast(spell, false, pObjSrc, true))

                return (false);



            switch (spell)

            {

                // 1st

                case SPELL_TYPE.SPELL_Create_Food:

                    // Create object. Normally food.

                    //        if (pObj == NULL)

                    //        {

                    //            static const ITEMID_TYPE sm_Item_Foods[] =  // possible foods.

                    //            {

                    //    ITEMID_FOOD_BACON,

                    //    ITEMID_FOOD_SAUSAGE,

                    //    ITEMID_FOOD_HAM,

                    //    ITEMID_FOOD_CAKE,

                    //    ITEMID_FOOD_BREAD,

                    //};



                    //            CItemPtr pItem = CItem::CreateScript(sm_Item_Foods[Calc_GetRandVal(COUNTOF(sm_Item_Foods))], this);

                    //            pItem->SetType(IT_FOOD);    // should already be set .

                    //            pItem->MoveToCheck(m_Act.m_pt, this);

                    //        }

                    break;



                case SPELL_TYPE.SPELL_Magic_Arrow:

                    throw new NotImplementedException();
                    //Spell_Effect_Bolt(pObj, ITEMID_FX_MAGIC_ARROW, iSkillLevel);

                    break;



                case SPELL_TYPE.SPELL_Heal:

                case SPELL_TYPE.SPELL_Night_Sight:

                case SPELL_TYPE.SPELL_Reactive_Armor:



                case SPELL_TYPE.SPELL_Clumsy:

                case SPELL_TYPE.SPELL_Feeblemind:

                case SPELL_TYPE.SPELL_Weaken:

                    simple_effect:

                    if (pObj == NULL)

                        return (false);

                    pObj.OnSpellEffect(spell, this, iSkillLevel, REF_CAST<CItem>(pObjSrc));

                    break;



                // 2nd

                case SPELL_TYPE.SPELL_Agility:

                case SPELL_TYPE.SPELL_Cunning:

                case SPELL_TYPE.SPELL_Cure:

                case SPELL_TYPE.SPELL_Protection:

                case SPELL_TYPE.SPELL_Strength:



                case SPELL_TYPE.SPELL_Harm:

                    goto simple_effect;



                case SPELL_TYPE.SPELL_Magic_Trap:

                case SPELL_TYPE.SPELL_Magic_Untrap:

                    // Create the trap object and link it to the target. ???

                    // A container is diff from door or stationary object

                    break;



                // 3rd

                case SPELL_TYPE.SPELL_Bless:



                case SPELL_TYPE.SPELL_Poison:

                    goto simple_effect;

                case SPELL_TYPE.SPELL_Fireball:

                    throw new NotImplementedException();
                    //Spell_Effect_Bolt(pObj, ITEMID_FX_FIRE_BALL, iSkillLevel);

                    break;



                case SPELL_TYPE.SPELL_Magic_Lock:

                case SPELL_TYPE.SPELL_Unlock:

                    goto simple_effect;



                case SPELL_TYPE.SPELL_Telekin:

                    // Act as dclick on the object.
                    throw new NotImplementedException();

                //Use_Obj(pObj, false);

                //break;

                case SPELL_TYPE.SPELL_Teleport:

                    throw new NotImplementedException();
                //Spell_Effect_Teleport(m_Act.m_pt);

                //break;

                case SPELL_TYPE.SPELL_Wall_of_Stone:

                    throw new NotImplementedException();
                //Spell_Effect_Field(m_Act.m_pt, ITEMID_STONE_WALL, ITEMID_STONE_WALL, iSkillLevel);

                //break;



                // 4th

                case SPELL_TYPE.SPELL_Arch_Cure:

                case SPELL_TYPE.SPELL_Arch_Prot:

                    {

                        throw new NotImplementedException();
                        //Spell_Effect_Area(m_Act.m_pt, 5, iSkillLevel);

                        //break;

                    }

                case SPELL_TYPE.SPELL_Great_Heal:

                case SPELL_TYPE.SPELL_Curse:

                case SPELL_TYPE.SPELL_Lightning:

                    goto simple_effect;

                case SPELL_TYPE.SPELL_Fire_Field:

                    throw new NotImplementedException();

                //Spell_Effect_Field(m_Act.m_pt, ITEMID_FX_FIRE_F_EW, ITEMID_FX_FIRE_F_NS, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Recall:

                    throw new NotImplementedException();
                //if (!Spell_Effect_Recall(REF_CAST(CItem, pObj), false, iSkillLevel))

                //    return (false);

                //break;



                // 5th



                case SPELL_TYPE.SPELL_Blade_Spirit:

                    m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_BLADES;

                    m_Act.m_atMagery.m_fSummonPet = true;

                    goto summon_effect;



                case SPELL_TYPE.SPELL_Dispel_Field:

                    {
                        throw new NotImplementedException();

                        //CItemPtr pItem = REF_CAST(CItem, pObj);

                        //if (pItem == NULL)

                        //{

                        //    WriteString("That is not a field!");

                        //    return (false);

                        //}

                        //pItem->OnSpellEffect(SPELL_Dispel_Field, this, iSkillLevel, NULL);

                    }

                    break;



                case SPELL_TYPE.SPELL_Mind_Blast:

                    throw new NotImplementedException();
                //if (pObj->IsChar())

                //{

                //    CCharPtr pChar = REF_CAST(CChar, pObj);

                //    ASSERT(pChar);

                //    int iDiff = (m_StatInt - pChar->m_StatInt) / 2;

                //    if (iDiff < 0)

                //    {

                //        pChar = this;   // spell revereses !

                //        iDiff = -iDiff;

                //    }

                //    int iMax = pChar->m_StatMaxHealth / 4;

                //    pChar->OnSpellEffect(spell, this, MIN(iDiff, iMax), NULL);

                //}

                //break;



                case SPELL_TYPE.SPELL_Magic_Reflect:



                case SPELL_TYPE.SPELL_Paralyze:

                case SPELL_TYPE.SPELL_Incognito:

                    goto simple_effect;



                case SPELL_TYPE.SPELL_Poison_Field:

                    throw new NotImplementedException();
                //Spell_Effect_Field(m_Act.m_pt, ITEMID_FX_POISON_F_1, ITEMID_FX_POISON_F_NS, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Summon:

                    summon_effect:

                    throw new NotImplementedException();
                //Spell_Effect_Summon(m_Act.m_atMagery.m_SummonID, m_Act.m_pt, m_Act.m_atMagery.m_fSummonPet);

                //break;



                // 6th



                case SPELL_TYPE.SPELL_Invis:



                case SPELL_TYPE.SPELL_Dispel:

                    goto simple_effect;



                case SPELL_TYPE.SPELL_Energy_Bolt:

                    throw new NotImplementedException();
                //Spell_Effect_Bolt(pObj, ITEMID_FX_ENERGY_BOLT, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Explosion:

                    throw new NotImplementedException();
                //Spell_Effect_Area(m_Act.m_pt, 2, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Mark:

                    goto simple_effect;



                case SPELL_TYPE.SPELL_Mass_Curse:

                    throw new NotImplementedException();
                //Spell_Effect_Area(m_Act.m_pt, 5, iSkillLevel);

                //break;

                case SPELL_TYPE.SPELL_Paralyze_Field:

                    throw new NotImplementedException();
                //Spell_Effect_Field(m_Act.m_pt, ITEMID_FX_PARA_F_EW, ITEMID_FX_PARA_F_NS, iSkillLevel);

                //break;

                case SPELL_TYPE.SPELL_Reveal:

                    throw new NotImplementedException();
                //Spell_Effect_Area(m_Act.m_pt, SPHEREMAP_VIEW_SIGHT, iSkillLevel);

                //break;



                // 7th



                case SPELL_TYPE.SPELL_Chain_Lightning:

                    throw new NotImplementedException();
                //Spell_Effect_Area(m_Act.m_pt, 5, iSkillLevel);

                //break;

                case SPELL_TYPE.SPELL_Energy_Field:

                    throw new NotImplementedException();
                //Spell_Effect_Field(m_Act.m_pt, ITEMID_FX_ENERGY_F_EW, ITEMID_FX_ENERGY_F_NS, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Flame_Strike:

                    throw new NotImplementedException();
                //// Display spell.

                //if (pObj == NULL)

                //{

                //    CItemPtr pItem = CItem::CreateBase(ITEMID_FX_FLAMESTRIKE);

                //    ASSERT(pItem);

                //    pItem->SetType(IT_SPELL);

                //    pItem->m_itSpell.m_spell = SPELL_Flame_Strike;

                //    pItem->MoveToDecay(m_Act.m_pt, 2 * TICKS_PER_SEC);

                //}

                //else

                //{

                //    pObj->Effect(EFFECT_OBJ, ITEMID_FX_FLAMESTRIKE, pObj, 6, 15);

                //    // Burn person at location.

                //    goto simple_effect;

                //}

                //break;



                case SPELL_TYPE.SPELL_Gate_Travel:

                    throw new NotImplementedException();
                //if (!Spell_Effect_Recall(REF_CAST(CItem, pObj), true, iSkillLevel))

                //    return (false);

                //break;



                case SPELL_TYPE.SPELL_Mana_Drain:

                case SPELL_TYPE.SPELL_Mana_Vamp:

                    throw new NotImplementedException();
                //// Take the mana from the target.

                //if (pObj->IsChar() && this != pObj)

                //{

                //    CCharPtr pChar = REF_CAST(CChar, pObj);

                //    ASSERT(pChar);

                //    if (!pChar->IsStatFlag(STATF_Reflection))

                //    {

                //        int iMax = pChar->m_StatInt;

                //        int iDiff = m_StatInt - iMax;

                //        if (iDiff < 0)

                //            iDiff = 0;

                //        else

                //            iDiff = Calc_GetRandVal(iDiff);

                //        iDiff += Calc_GetRandVal(25);

                //        pChar->OnSpellEffect(spell, this, iDiff, NULL);

                //        if (spell == SPELL_Mana_Vamp)

                //        {

                //            // Give some back to me.

                //            Stat_Change(STAT_Mana, MIN(iDiff, m_StatInt));

                //        }

                //        break;

                //    }

                //}

                //goto simple_effect;



                case SPELL_TYPE.SPELL_Mass_Dispel:

                    throw new NotImplementedException();
                //Spell_Effect_Area(m_Act.m_pt, 15, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Meteor_Swarm:

                    throw new NotImplementedException();
                    //// Multi explosion ??? 0x36b0

                    //SPELL_TYPE.Spell_Effect_Area(m_Act.m_pt, 4, iSkillLevel);

                    break;



                case SPELL_TYPE.SPELL_Polymorph:

                    throw new NotImplementedException();
                //// This has a menu select for client.

                //if (GetPrivLevel() < PLEVEL_Seer)

                //{

                //    if (pObj != this)

                //        return (false);

                //}

                //goto simple_effect;



                // 8th



                case SPELL_TYPE.SPELL_Earthquake:

                    throw new NotImplementedException();
                //Spell_Effect_Area(GetTopPoint(), SPHEREMAP_VIEW_SIGHT, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Vortex:

                    m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_VORTEX;

                    m_Act.m_atMagery.m_fSummonPet = true;

                    goto summon_effect;



                case SPELL_TYPE.SPELL_Resurrection:

                case SPELL_TYPE.SPELL_Light:

                    goto simple_effect;



                case SPELL_TYPE.SPELL_Air_Elem:

                    m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_AIR_ELEM;

                    m_Act.m_atMagery.m_fSummonPet = true;

                    goto summon_effect;

                case SPELL_TYPE.SPELL_Daemon:

                    throw new NotImplementedException();
                //m_Act.m_atMagery.m_SummonID = (Calc_GetRandVal(2)) ? CREID_TYPE.CREID_DAEMON_SWORD : CREID_TYPE.CREID_DAEMON;

                //m_Act.m_atMagery.m_fSummonPet = true;

                //goto summon_effect;

                case SPELL_TYPE.SPELL_Earth_Elem:

                    m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_EARTH_ELEM;

                    m_Act.m_atMagery.m_fSummonPet = true;

                    goto summon_effect;

                case SPELL_TYPE.SPELL_Fire_Elem:

                    m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_FIRE_ELEM;

                    m_Act.m_atMagery.m_fSummonPet = true;

                    goto summon_effect;

                case SPELL_TYPE.SPELL_Water_Elem:

                    m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_WATER_ELEM;

                    m_Act.m_atMagery.m_fSummonPet = true;

                    goto summon_effect;



                // Necro

                case SPELL_TYPE.SPELL_Summon_Undead:

                    throw new NotImplementedException();
                //switch (Calc_GetRandVal(15))

                //{

                //    case 1:

                //        m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_LICH;

                //        break;

                //    case 3:

                //    case 5:

                //    case 7:

                //    case 9:

                //        m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_SKELETON;

                //        break;

                //    default:

                //        m_Act.m_atMagery.m_SummonID = CREID_TYPE.CREID_ZOMBIE;

                //        break;

                //}

                //m_Act.m_atMagery.m_fSummonPet = true;

                //goto summon_effect;



                case SPELL_TYPE.SPELL_Animate_Dead:

                    throw new NotImplementedException();
                //if (!Spell_Effect_AnimateDead(REF_CAST(CItemCorpse, pObj)))

                //    return false;

                //break;

                case SPELL_TYPE.SPELL_Bone_Armor:

                    throw new NotImplementedException();
                //if (!Spell_Effect_BoneArmor(REF_CAST(CItemCorpse, pObj)))

                //    return false;

                //break;

                case SPELL_TYPE.SPELL_Fire_Bolt:

                    throw new NotImplementedException();
                //Spell_Effect_Bolt(pObj, ITEMID_FX_FIRE_BOLT, iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Ale:     // 90 = drunkeness ?

                case SPELL_TYPE.SPELL_Wine:    // 91 = mild drunkeness ?

                case SPELL_TYPE.SPELL_Liquor:  // 92 = extreme drunkeness ?

                case SPELL_TYPE.SPELL_Hallucination:

                case SPELL_TYPE.SPELL_Stone:

                case SPELL_TYPE.SPELL_Shrink:

                case SPELL_TYPE.SPELL_Mana:

                case SPELL_TYPE.SPELL_Refresh:

                case SPELL_TYPE.SPELL_Restore:     // increases both your hit points and your stamina.

                case SPELL_TYPE.SPELL_Sustenance:      //  // serves to fill you up. (Remember, healing rate depends on how well fed you are!)

                case SPELL_TYPE.SPELL_Forget:          //  // permanently lowers one skill.

                case SPELL_TYPE.SPELL_Gender_Swap:     //  // permanently changes your gender.

                case SPELL_TYPE.SPELL_Chameleon:       //  // makes your skin match the colors of whatever is behind you.

                case SPELL_TYPE.SPELL_BeastForm:       //  // polymorphs you into an animal for a while.

                case SPELL_TYPE.SPELL_Monster_Form:    //  // polymorphs you into a monster for a while.

                case SPELL_TYPE.SPELL_Trance:          //  // temporarily increases your meditation skill.

                case SPELL_TYPE.SPELL_Particle_Form:   //  // turns you into an immobile, but untargetable particle system for a while.

                case SPELL_TYPE.SPELL_Shield:          //  // erects a temporary force field around you. Nobody approaching will be able to get within 1 tile of you, though you can move close to them if you wish.

                case SPELL_TYPE.SPELL_Steelskin:       //  // turns your skin into steel, giving a boost to your AR.

                case SPELL_TYPE.SPELL_Stoneskin:       //  // turns your skin into stone, giving a boost to your AR.

                    goto simple_effect;



                default:

                    // No effect on creatures it seems.

                    break;

            }



            //if (pObj != NULL &&

            //    pObj.IsChar() &&

            //    pObj != this &&

            //    pSpellDef.IsSpellType(SPELLFLAG_GOOD))

            //{

            //    CCharPtr pChar = REF_CAST(CChar, pObj);

            //    ASSERT(pChar);

            //    pChar.OnHelpedBy(this);

            //}



            //// Make noise.

            //if (!IsStatFlag(STATF_Insubstantial))

            //{

            //    Sound(pSpellDef->m_sound);

            //}

            return (true);

        }

        public STAT_LEVEL Skill_GetAdjusted(SKILL_TYPE skill)

        {

            // Get the skill adjusted for str,dex,int = 0-1000



            // m_SkillStat is used to figure out how much

            // of the total bonus comes from the stats

            // so if it's 80, then 20% (100% - 80%) comes from

            // the stat (str,int,dex) bonuses



            // example:



            // These are the cchar's stats:

            // m_Skill[x] = 50.0

            // m_Stat[str] = 50, m_Stat[int] = 30, m_Stat[dex] = 20



            // these are the skill "defs":

            // m_SkillStat = 80

            // m_StatBonus[str] = 50

            // m_StatBonus[int] = 50

            // m_StatBonus[dex] = 0



            // Pure bonus is:

            // 50% of str (25) + 50% of int (15) = 40



            // Percent of pure bonus to apply to raw skill is

            // 20% = 100% - m_SkillStat = 100 - 80



            // adjusted bonus is: 8 (40 * 0.2)



            // so the effective skill is 50 (the raw) + 8 (the bonus)

            // which is 58 in total.



            ASSERT(IsSkillBase(skill));

            CSkillDefPtr pSkillDef = g_Cfg.GetSkillDef(skill);

            ASSERT(pSkillDef);

            int iPureBonus =

            (pSkillDef.m_StatBonus[(int)STAT_TYPE.STAT_Str] * m_Stat[STAT_TYPE.STAT_Str]) +

            (pSkillDef.m_StatBonus[(int)STAT_TYPE.STAT_Int] * m_Stat[STAT_TYPE.STAT_Int]) +

            (pSkillDef.m_StatBonus[(int)STAT_TYPE.STAT_Dex] * m_Stat[STAT_TYPE.STAT_Dex]);



            int iAdjSkill = IMULDIV(pSkillDef.m_StatPercent, iPureBonus, 10000);



            // TODO:
            //switch ((SKILL_TYPE)skill)

            //{

            //    case SKILL_TYPE.SKILL_PROVOCATION:

            //    case SKILL_TYPE.SKILL_ENTICEMENT:

            //    case SKILL_TYPE.SKILL_PEACEMAKING:

            //        {

            //            double sMusicianshipPercent = (double)Skill_GetBase(SKILL_TYPE.SKILL_MUSICIANSHIP) / 1000.0;

            //            return ((STAT_LEVEL)(Skill_GetBase((SKILL_TYPE)skill) * sMusicianshipPercent + iAdjSkill));

            //        }

            //};



            return (Skill_GetBase((SKILL_TYPE)skill) + iAdjSkill);

        }

        public override void OnSpellEffect(SPELL_TYPE spell, CChar pCharSrc, int iSkillLevel, CItem pSourceItem)

        {

            // Spell has a direct effect on this char.

            // This should effect noto of source.

            // ARGS:

            //  pSourceItem = the potion, wand, scroll etc. NULL = cast (IT_SPELL)

            //  iSkillLevel = 0-1000 = difficulty. may be slightly larger .

            // RETURN:

            //  false = the spell did not work. (should we get credit ?)



            if (this == NULL)

                return;

            // TODO:
            //            ASSERT(!IsItem());



            if (iSkillLevel <= 0)   // spell died (fizzled?).

                return;


            // TODO:
            //          CSphereExpArgs Args(this,

            //              (pCharSrc != NULL ) ? ((CScriptConsole*)pCharSrc) : ((CScriptConsole*)&g_Serv),

            //(int)spell, iSkillLevel, pSourceItem );

            //          if (OnTrigger(CCharDef::T_SpellEffect, Args) == TRIGRET_RET_VAL)

            //              return (false);



            CSpellDefPtr pSpellDef = g_Cfg.GetSpellDef(spell);

            if (pSpellDef == NULL)

                return;



            // TODO:
            //// Most spells don't work on ghosts.

            //if (IsStatFlag(STATF_DEAD) && spell != SPELL_Resurrection)

            //    return false;



            bool fResistAttempt = true;

            // TODO:
            //switch (spell)  // just strengthen the effect.

            //{

            //    case SPELL_Wall_of_Stone:

            //        StatFlag_Clear(STATF_Freeze);

            //        return true;    // not caught anyway

            //    case SPELL_Poison:

            //    case SPELL_Poison_Field:

            //        if (IsStatFlag(STATF_Poisoned))

            //        {

            //            fResistAttempt = false;

            //        }   // no further effect. don't count resist effect.

            //        break;

            //    case SPELL_Paralyze_Field:

            //    case SPELL_Paralyze:

            //        if (IsStatFlag(STATF_Freeze))

            //            return false;   // no further effect.

            //        break;

            //}



            //bool fPotion = (pSourceItem != NULL && pSourceItem->IsType(IT_POTION));

            //if (fPotion)

            //    fResistAttempt = false;

            //if (pCharSrc == this)

            //    fResistAttempt = false;



            if (pSpellDef.IsSpellType(SPELLFLAG_HARM))

            {

                // Can't harm yourself directly ?

                if (pCharSrc == this)

                    return;


                // TODO:
                //if (IsStatFlag(STATF_INVUL))

                //{

                //    Effect(EFFECT_OBJ, ITEMID_FX_GLOW, this, 9, 30, false);

                //    return false;

                //}



                //if (!fPotion && fResistAttempt)

                //{

                //    if (pCharSrc != NULL && GetPrivLevel() > PLEVEL_Guest)

                //    {

                //        if (pCharSrc->GetPrivLevel() <= PLEVEL_Guest)

                //        {

                //            pCharSrc->WriteString("The guest curse strikes you.");

                //            goto reflectit;

                //        }

                //    }



                //    // Check resistance to magic ?

                //    if (pSpellDef->IsSpellType(SPELLFLAG_RESIST))

                //    {

                //        if (Skill_UseQuick(SKILL_MAGICRESISTANCE, iSkillLevel))

                //        {

                //            WriteString("You feel yourself resisting magic");



                //            // iSkillLevel

                //            iSkillLevel /= 2;   // ??? reduce effect of spell.

                //        }



                //        // Check magic reflect.

                //        if (IsStatFlag(STATF_Reflection))   // reflected.

                //        {

                //            StatFlag_Clear(STATF_Reflection);

                //            reflectit:

                //            Effect(EFFECT_OBJ, ITEMID_FX_GLOW, this, 9, 30, false);

                //            if (pCharSrc != NULL)

                //            {

                //                pCharSrc->OnSpellEffect(spell, NULL, iSkillLevel / 2, pSourceItem);

                //            }

                //            return false;

                //        }

                //    }

                //}



                //if (!OnAttackedBy(pCharSrc, 1, false))

                //    return false;

            }



            if (pSpellDef.IsSpellType(SPELLFLAG_FX_TARG) &&

                pSpellDef.m_idEffect != 0)

            {

                Effect(EFFECT_TYPE.EFFECT_OBJ, pSpellDef.m_idEffect, this, 0, 15); // 9, 14

            }



            iSkillLevel = iSkillLevel / 2 + Calc_GetRandVal(iSkillLevel / 2);   // randomize the effect.



            switch (spell)

            {



                case SPELL_TYPE.SPELL_Ale:     // 90 = drunkeness ?

                case SPELL_TYPE.SPELL_Wine:    // 91 = mild drunkeness ?

                case SPELL_TYPE.SPELL_Liquor:  // 92 = extreme drunkeness ?



                case SPELL_TYPE.SPELL_Clumsy:

                case SPELL_TYPE.SPELL_Feeblemind:

                case SPELL_TYPE.SPELL_Weaken:

                case SPELL_TYPE.SPELL_Agility:

                case SPELL_TYPE.SPELL_Cunning:

                case SPELL_TYPE.SPELL_Strength:

                case SPELL_TYPE.SPELL_Bless:

                case SPELL_TYPE.SPELL_Curse:

                case SPELL_TYPE.SPELL_Mass_Curse:

                    throw new NotImplementedException();
                //Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_STATS, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Heal:

                case SPELL_TYPE.SPELL_Great_Heal:

                    throw new NotImplementedException();
                //if (iSkillLevel > 1000)

                //{

                //    Stat_Change(STAT_Health, g_Cfg.GetSpellEffect(spell, iSkillLevel), m_StatMaxHealth + 20);

                //}

                //else

                //{

                //    Stat_Change(STAT_Health, g_Cfg.GetSpellEffect(spell, iSkillLevel));

                //}

                //break;



                case SPELL_TYPE.SPELL_Night_Sight:

                    throw new NotImplementedException();
                //Spell_Equip_Create(SPELL_Night_Sight, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Night_Sight, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Reactive_Armor:

                    throw new NotImplementedException();
                //Spell_Equip_Create(SPELL_Reactive_Armor, LAYER_SPELL_Reactive, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Magic_Reflect:

                    throw new NotImplementedException();
                //Spell_Equip_Create(SPELL_Magic_Reflect, LAYER_SPELL_Magic_Reflect, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Poison:

                case SPELL_TYPE.SPELL_Poison_Field:

                    throw new NotImplementedException();
                //if (!fPotion)

                //{

                //    Effect(EFFECT_OBJ, ITEMID_FX_CURSE_EFFECT, this, 0, 15);

                //}

                //Spell_Effect_Poison(iSkillLevel, iSkillLevel / 50, pCharSrc);

                //break;



                case SPELL_TYPE.SPELL_Cure:

                    throw new NotImplementedException();
                //Spell_Effect_Cure(iSkillLevel, iSkillLevel > 900);

                //break;

                case SPELL_TYPE.SPELL_Arch_Cure:

                    throw new NotImplementedException();
                //Spell_Effect_Cure(iSkillLevel, true);

                //break;



                case SPELL_TYPE.SPELL_Protection:

                case SPELL_TYPE.SPELL_Arch_Prot:

                    throw new NotImplementedException();
                //Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Protection, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Dispel:

                case SPELL_TYPE.SPELL_Mass_Dispel:

                    throw new NotImplementedException();
                // ??? should be difficult to dispel SPELL_Summon creatures

                //Spell_Effect_Dispel((pCharSrc != NULL && pCharSrc->IsGM()) ? 150 : 50);

                //break;



                case SPELL_TYPE.SPELL_Reveal:

                    throw new NotImplementedException();
                //if (!Reveal())

                //    break;

                //Effect(EFFECT_OBJ, ITEMID_FX_BLESS_EFFECT, this, 0, 15);

                //break;



                case SPELL_TYPE.SPELL_Invis:

                    throw new NotImplementedException();
                //Spell_Equip_Create(SPELL_Invis, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Invis, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Incognito:

                    throw new NotImplementedException();
                //Spell_Equip_Create(SPELL_Incognito, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Incognito, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Particle_Form:   // 112 // turns you into an immobile, but untargetable particle system for a while.

                case SPELL_TYPE.SPELL_Stone:

                case SPELL_TYPE.SPELL_Paralyze_Field:

                case SPELL_TYPE.SPELL_Paralyze:

                    // Effect( EFFECT_OBJ, ITEMID_FX_CURSE_EFFECT, this, 0, 15 );

                    throw new NotImplementedException();
                //Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Paralyze, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Mana_Drain:

                case SPELL_TYPE.SPELL_Mana_Vamp:

                    throw new NotImplementedException();
                //Stat_Change(STAT_Mana, -iSkillLevel);

                //break;



                case SPELL_TYPE.SPELL_Harm:

                    OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_POISON | DAMAGE_MAGIC | DAMAGE_GENERAL);

                    break;

                case SPELL_TYPE.SPELL_Mind_Blast:

                    OnTakeDamage(iSkillLevel, pCharSrc, DAMAGE_POISON | DAMAGE_MAGIC | DAMAGE_GENERAL);

                    break;

                case SPELL_TYPE.SPELL_Explosion:

                    OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_MAGIC | DAMAGE_HIT_BLUNT | DAMAGE_GENERAL);

                    break;

                case SPELL_TYPE.SPELL_Energy_Bolt:

                case SPELL_TYPE.SPELL_Magic_Arrow:

                    OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_MAGIC | DAMAGE_HIT_PIERCE);

                    break;

                case SPELL_TYPE.SPELL_Fireball:

                case SPELL_TYPE.SPELL_Fire_Bolt:

                    OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_MAGIC | DAMAGE_HIT_BLUNT | DAMAGE_FIRE);

                    break;

                case SPELL_TYPE.SPELL_Fire_Field:

                case SPELL_TYPE.SPELL_Flame_Strike:

                    // Burn whoever is there.

                    OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_MAGIC | DAMAGE_FIRE | DAMAGE_GENERAL);

                    break;

                case SPELL_TYPE.SPELL_Meteor_Swarm:

                    Effect(EFFECT_TYPE.EFFECT_OBJ, ITEMID_TYPE.ITEMID_FX_EXPLODE_3, this, 9, 6);

                    OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_MAGIC | DAMAGE_HIT_BLUNT | DAMAGE_FIRE);

                    break;

                case SPELL_TYPE.SPELL_Earthquake:

                    OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_HIT_BLUNT | DAMAGE_GENERAL);

                    break;

                case SPELL_TYPE.SPELL_Lightning:

                case SPELL_TYPE.SPELL_Chain_Lightning:

                    throw new NotImplementedException();
                //GetTopSector()->LightFlash();

                //Effect(EFFECT_LIGHTNING, ITEMID_NOTHING, pCharSrc);

                //OnTakeDamage(g_Cfg.GetSpellEffect(spell, iSkillLevel), pCharSrc, DAMAGE_ELECTRIC | DAMAGE_GENERAL);

                //break;



                case SPELL_TYPE.SPELL_Resurrection:

                    throw new NotImplementedException();
                //return Spell_Effect_Resurrection((pCharSrc && pCharSrc->IsGM()) ? -1 : 0, NULL);



                case SPELL_TYPE.SPELL_Light:

                    throw new NotImplementedException();
                //Effect(EFFECT_OBJ, ITEMID_FX_HEAL_EFFECT, this, 9, 6);

                //Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_LIGHT, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Hallucination:

                    {

                        throw new NotImplementedException();
                        //CItemPtr pSpell = Spell_Equip_Create(SPELL_Hallucination, LAYER_FLAG_Hallucination, iSkillLevel, 10 * TICKS_PER_SEC, pCharSrc, !fPotion);

                        //ASSERT(pSpell);

                        //pSpell->m_itSpell.m_spellcharges = Calc_GetRandVal(30);

                    }

                    break;

                case SPELL_TYPE.SPELL_Polymorph:

                    {

                    throw new NotImplementedException();
//                        CREID_TYPE creid = m_Act.m_atMagery.m_SummonID;

                        //#define SPELL_MAX_POLY_STAT 150



                        //                        CItemPtr pSpell = Spell_Equip_Create(SPELL_Polymorph, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Polymorph, iSkillLevel, 0, pCharSrc, !fPotion);

                        //                        ASSERT(pSpell);



                        //                        SetID(creid);



                        //                        CCharDefPtr pCharDef = Char_GetDef();

                        //                        ASSERT(pCharDef);



                        //                        // set to creature type stats.

                        //                        if (pCharDef->m_Str)

                        //                        {

                        //                            int iStatPrv = m_StatStr;

                        //                            int iChange = pCharDef->m_Str - iStatPrv;

                        //                            if (iChange > SPELL_MAX_POLY_STAT)

                        //                                iChange = SPELL_MAX_POLY_STAT;

                        //                            if (iChange < -50)

                        //                                iChange = -50;

                        //                            Stat_Set(STAT_Str, iChange + iStatPrv);

                        //                            pSpell->m_itSpell.m_PolyStr = m_StatStr - iStatPrv;

                        //                        }

                        //                        else

                        //                        {

                        //                            pSpell->m_itSpell.m_PolyStr = 0;

                        //                        }

                        //                        if (pCharDef->m_Dex)

                        //                        {

                        //                            int iStatPrv = m_StatDex;

                        //                            int iChange = pCharDef->m_Dex - iStatPrv;

                        //                            if (iChange > SPELL_MAX_POLY_STAT)

                        //                                iChange = SPELL_MAX_POLY_STAT;

                        //                            if (iChange < -50)

                        //                                iChange = -50;

                        //                            Stat_Set(STAT_Dex, iChange + iStatPrv);

                        //                            pSpell->m_itSpell.m_PolyDex = m_StatDex - iStatPrv;

                        //                        }

                        //                        else

                        //                        {

                        //                            pSpell->m_itSpell.m_PolyDex = 0;

                        //                        }

                        //                        Update();       // show everyone I am now a new type

                                            }

                        //                    break;



                case SPELL_TYPE.SPELL_Shrink:

                    // Getting a pet to drink this is funny.
                    throw new NotImplementedException();

                //if (m_pPlayer.IsValidNewObj())

                //    break;

                //if (fPotion && pSourceItem)

                //{

                //    pSourceItem->DeleteThis();

                //}

                //NPC_Shrink();   // this delete's the char !!!

                //break;



                case SPELL_TYPE.SPELL_Mana:

                    throw new NotImplementedException();
                //if (iSkillLevel > 1000)

                //{

                //    Stat_Change(STAT_Mana, g_Cfg.GetSpellEffect(spell, iSkillLevel), m_StatInt + 20);

                //}

                //else

                //{

                //    Stat_Change(STAT_Mana, g_Cfg.GetSpellEffect(spell, iSkillLevel));

                //}

                //break;



                case SPELL_TYPE.SPELL_Refresh:
                    throw new NotImplementedException();

                //if (iSkillLevel > 1000)

                //{

                //    Stat_Change(STAT_Stam, g_Cfg.GetSpellEffect(spell, iSkillLevel), m_StatDex + 20);

                //}

                //else

                //{

                //    Stat_Change(STAT_Stam, g_Cfg.GetSpellEffect(spell, iSkillLevel));

                //}

                //break;



                case SPELL_TYPE.SPELL_Restore:     // increases both your hit points and your stamina.

                    throw new NotImplementedException();
                //Stat_Change(STAT_Stam, g_Cfg.GetSpellEffect(spell, iSkillLevel));

                //Stat_Change(STAT_Health, g_Cfg.GetSpellEffect(spell, iSkillLevel));

                //break;



                case SPELL_TYPE.SPELL_Forget:          // 109 // permanently lowers one skill.

                    {

                        throw new NotImplementedException();
                        //int iSkillLevel = 0;

                        //Skill_Degrade(SKILL_QTY);

                    }

                    break;

                case SPELL_TYPE.SPELL_Sustenance:      // 105 // serves to fill you up. (Remember, healing rate depends on how well fed you are!)

                    {

                        throw new NotImplementedException();
                        //CCharDefPtr pCharDef = Char_GetDef();

                        //ASSERT(pCharDef);

                        //Stat_Set(STAT_Food, pCharDef->m_MaxFood + (pCharDef->m_MaxFood / 2));

                    }

                    break;

                case SPELL_TYPE.SPELL_Gender_Swap:     // 110 // permanently changes your gender.

                    throw new NotImplementedException();
                //if (IsHuman())

                //{

                //    CCharDefPtr pCharDef = Char_GetDef();

                //    ASSERT(pCharDef);



                //    SetID(pCharDef->IsFemale() ? CREID_MAN : CREID_WOMAN);

                //    m_prev_id = GetID();

                //    Update();

                //}

                //break;



                case SPELL_TYPE.SPELL_Chameleon:       // 106 // makes your skin match the colors of whatever is behind you.

                case SPELL_TYPE.SPELL_BeastForm:       // 107 // polymorphs you into an animal for a while.

                case SPELL_TYPE.SPELL_Monster_Form:    // 108 // polymorphs you into a monster for a while.

                    throw new NotImplementedException();
                //Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Polymorph, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Trance:          // 111 // temporarily increases your meditation skill.

                    throw new NotImplementedException();
                //Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_STATS, iSkillLevel, 0, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Shield:          // 113 // erects a temporary force field around you. Nobody approaching will be able to get within 1 tile of you, though you can move close to them if you wish.

                case SPELL_TYPE.SPELL_Steelskin:       // 114 // turns your skin into steel, giving a boost to your AR.

                case SPELL_TYPE.SPELL_Stoneskin:       // 115 // turns your skin into stone, giving a boost to your AR.

                    throw new NotImplementedException();
                //Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_Protection, iSkillLevel, -1, pCharSrc, !fPotion);

                //break;



                case SPELL_TYPE.SPELL_Regenerate:

                    // Set number of charges based on effect level.

                    //

                    {

                        throw new NotImplementedException();
                        //int iDuration = pSpellDef->m_Duration.GetLinear(iSkillLevel);

                        //iDuration /= (2 * TICKS_PER_SEC);

                        //if (iDuration <= 0)

                        //    iDuration = 1;

                        //CItemPtr pSpell = Spell_Equip_Create(spell, fPotion ? LAYER_FLAG_Potion : LAYER_SPELL_STATS, iSkillLevel, 2 * TICKS_PER_SEC, pCharSrc, !fPotion);

                        //ASSERT(pSpell);

                        //pSpell->m_itSpell.m_spellcharges = iDuration;

                    }

                    break;



                default:

                    // seems to have no effect.

                    break;

            }
        }
    }
}