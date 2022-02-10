using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Simulation.Library
{
    public class Warlock
    {
        #region Stats
        public int MP5 => GetAllGear().Where(e => e is not null).Select(e => e.ManaRegn).Sum() + mp5;
        private int mp5 = 0;
        public int HitRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellHitRating).Sum() + hitRating;
        private int hitRating = 0;
        public double Hit => (HitRating / 12.6) + hit;
        private double hit = 0;
        public int CritRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellCritRating).Sum() + critRating;
        public int critRating = 0;
        public int Intellect => intellect + GetAllGear().Where(e => e is not null).Select(e => e.Intellect).Sum();

        private int intellect = 131;
        public double Crit => crit + (Intellect / 81.9) + (CritRating / 22.1);
        private double crit = 1.7;
        public int SpellPower => GetAllGear().Where(e => e is not null).Select(e => e.SpellPower).Sum() + spellPower;
        private int spellPower = 0;
        public int ShadowPower => GetAllGear().Where(e => e is not null).Select(e => e.ShadowSpellPower).Sum() + shadowPower;
        private int shadowPower = 0;
        public int FirePower => GetAllGear().Where(e => e is not null).Select(e => e.FireSpellPower).Sum() + firePower;
        private int firePower = 0;
        public int ArcanePower => GetAllGear().Where(e => e is not null).Select(e => e.ArcaneSpellPower).Sum() + arcanePower;
        private int arcanePower = 0;
        public int FrostPower => GetAllGear().Where(e => e is not null).Select(e => e.FrostSpellPower).Sum() + frostPower;
        private int frostPower;
        public int HasteRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellHasteRating).Sum() + hasteRating;
        public int hasteRating = 0;
        //Hasted Cast Time = Base Cast Time / (1 + ( Spell Haste Rating / 1577 ) )
        public double Haste => (HasteRating / 15.77) + haste;
        private double haste = 0;
        public int Mana => mana;
        private int mana = 0;
        private int baseMana = 2615;
        public int MaxMana => (Math.Min(20, Intellect) + 15 * (Intellect - Math.Min(20, Intellect))) + baseMana;
        #endregion Stats

        #region Spells
        public Spell lastSpelledCasted { get; private set; }
        #endregion Spells

        #region Equipment
        private Equipment head;
        private Equipment neck;
        private Equipment shoulders;
        private Equipment back;
        private Equipment chest;
        private Equipment wrist;
        private Equipment hands;
        private Equipment waist;
        private Equipment legs;
        private Equipment feet;
        private Equipment ring1;
        private Equipment ring2;
        private Equipment trinket1;
        private Equipment trinket2;
        private Equipment mainhand;
        private Equipment offhand;
        private Equipment ranged;

        public Equipment Head => head;
        public Equipment Neck => neck;
        public Equipment Shoulders => shoulders;
        public Equipment Back => back;
        public Equipment Chest => chest;
        public Equipment Wrist => wrist;
        public Equipment Hands => hands;
        public Equipment Waist => waist;
        public Equipment Legs => legs;
        public Equipment Feet => feet;
        public Equipment Ring1 => ring1;
        public Equipment Ring2 => ring2;
        public Equipment Trinket1 => trinket1;
        public Equipment Trinket2 => trinket2;
        public Equipment Mainhand => mainhand;
        public Equipment Offhand => offhand;
        public Equipment Ranged => ranged;
        public Equipment[] Equipment => GetAllGear();
        #endregion Equipment

        public Warlock()
        {
            mana = MaxMana;
        }

        private Equipment[] GetAllGear()
        {
            return new Equipment[] { head, neck, shoulders, back, chest, wrist, hands, waist, legs, feet, ring1, ring2, trinket1, trinket2, mainhand, offhand, ranged };
        }

        #region AddStats
        public void AddMana(int mana)
        {
            this.mana += mana;
            if (this.mana < 0) this.mana = 0;
            if (this.mana > MaxMana) this.mana = MaxMana;
        }
        public void AddIntellect(int intellect)
        {
            this.intellect += intellect;
            if (this.intellect < 0) this.intellect = 0;
        }
        public void AddHasteRating(int hasteRating)
        {
            this.hasteRating += hasteRating;
        }
        public void AddHaste(double haste)
        {
            this.haste += haste;
        }
        public void AddCritRating(int critRating)
        {
            this.critRating += critRating;
        }
        public void AddShadowSpellPower(int shadowPower)
        {
            this.shadowPower += shadowPower;
        }
        public void AddFireSpellPower(int firePower)
        {
            this.firePower += firePower;
        }
        public void AddSpellPower(int spellPower)
        {
            this.spellPower += spellPower;
        }
        public void AddArcanePower(int arcanePower)
        {
            this.arcanePower += arcanePower;
        }
        public void AddFrostPower(int frostPower)
        {
            this.frostPower = frostPower;
        }
        #endregion AddStats

        #region CastSpells
        public bool CanCastShadowBolt(Spell shadowbolt)
        {
            return shadowbolt.Cost < Mana;
        }
        public void CastLifeTap(Spell lifetap, Report report = null)
        {
            if (report is null) report = new();
            if (lifetap.Name != "Life Tap") return;
            int manaFromLT = ManaGainedFromLifetap(lifetap);
            int ManaGained = 0;
            lastSpelledCasted = lifetap;
            if(mana + manaFromLT > MaxMana)
            {
                ManaGained = MaxMana - mana;
            }
            else
            {
                ManaGained = manaFromLT;
            }
            report.ReportManaGained(ManaGained, $"Life Tap{lastSpelledCasted.Rank}");
            this.mana += ManaGained;
            if (this.mana > MaxMana) this.mana = MaxMana;
        }
        private int ManaGainedFromLifetap(Spell lifetap)
        {
            return int.Parse(lifetap.ToolTipText.Split(' ')[1]);
        }
        public double CastShadowBolt(Spell shadowbolt, Report report = null)
        {
            if (report is null) report = new();
            if (shadowbolt.Name != "Shadow Bolt") return 0;
            double dmg;
            Random rnd = new();
            bool isCrit = rnd.Next(100) <= Crit;
            lastSpelledCasted = shadowbolt;
            dmg = rnd.Next(GetShadowboltMinDmg(shadowbolt), GetShadowboltMaxDmg(shadowbolt));
            dmg = dmg + ((SpellPower + ShadowPower) * GetShadowboltSPMod(shadowbolt));
            mana -= shadowbolt.Cost;
            if (!DidHit(lastSpelledCasted)) dmg = 0;
            dmg = isCrit ? dmg * 1.5 : dmg;
            report.ReportDamage(dmg, lastSpelledCasted, dmg > 0, isCrit);
            return dmg;
        }
        private int GetShadowboltMinDmg(Spell shadowbolt)
        {
            return int.Parse(shadowbolt.ToolTipText.Split(' ')[8]);
        }

        private int GetShadowboltMaxDmg(Spell shadowbolt)
        {
            return int.Parse(shadowbolt.ToolTipText.Split(' ')[10]);
        }

        private double GetShadowboltSPMod(Spell shadowbolt)
        {
            int start = shadowbolt.Effects[0].IndexOf("SP mod");
            int end = shadowbolt.Effects[0].IndexOf(")PVP");
            int lenght = end - start;
            string str = shadowbolt.Effects[0];
            string sub = str.Substring(start, lenght);
            sub = sub.Split(' ')[2];
            return double.Parse(sub);
        }
        private bool DidHit(Spell spell)
        {
            Random rnd = new();
            return rnd.Next(0) < 83 + Hit;
        }
        public double WaitForNextCast()
        {
            if (lastSpelledCasted is null) return 0;
            else
            {
                double gcd = (lastSpelledCasted.GCD) / (1 + (Haste / 100));
                if (gcd < 1000) gcd = 1000;
                double castTime = (lastSpelledCasted.CastTime) / (1 + (Haste / 100));
                return Math.Max(gcd, castTime);
            }
        }
        #endregion CastSpells

        #region EquipGear
        public void EquipHead(Equipment equipment) => head = equipment.InvSlot == "Head" ? equipment : head;
        public void EquipNeck(Equipment equipment) => neck = equipment.InvSlot == "Neck" ? equipment : neck;
        public void EquipShoulders(Equipment equipment) => shoulders = equipment.InvSlot == "Shoulder" ? equipment : shoulders;
        public void EquipBack(Equipment equipment) => back = equipment.InvSlot == "Back" ? equipment : back;
        public void EquipChest(Equipment equipment) => chest = equipment.InvSlot == "Chest" ? equipment : chest;
        public void EquipWrist(Equipment equipment) => wrist = equipment.InvSlot == "Wrist" ? equipment : wrist;
        public void EquipHands(Equipment equipment) => hands = equipment.InvSlot == "Hands" ? equipment : hands;
        public void EquipWaist(Equipment equipment) => waist = equipment.InvSlot == "Waist" ? equipment : waist;
        public void EquipLegs(Equipment equipment) => legs = equipment.InvSlot == "Legs" ? equipment : legs;
        public void EquipFeet(Equipment equipment) => feet = equipment.InvSlot == "Feet" ? equipment : feet;
        public void EquipRing1(Equipment equipment) => ring1 = equipment.InvSlot == "Finger" ? equipment : ring1;
        public void EquipRing2(Equipment equipment) => ring2 = equipment.InvSlot == "Finger" ? equipment : ring2;
        public void EquipTrinket1(Equipment equipment) => trinket1 = equipment.InvSlot == "Trinket" ? equipment : trinket1;
        public void EquipTrinket2(Equipment equipment) => trinket2 = equipment.InvSlot == "Trinket" ? equipment : trinket2;
        public void EquipMainhand(Equipment equipment)
        {
            if(equipment.InvSlot == "Main Hand")
            mainhand = equipment.InvSlot == "Main Hand" ? equipment : mainhand;
            else
            {
                EquipTwohander(equipment);
            }
        }
        private void EquipTwohander(Equipment equipment)
        {
            mainhand = equipment.InvSlot == "Two-Hand" ? equipment : mainhand;
            if (mainhand is not null && mainhand.InvSlot == "Two-Hand") offhand = null;
        }
        public void EquipOffhand(Equipment equipment)
        {
            if(mainhand is not null && mainhand.InvSlot == "Two-Hand") return;
            offhand = equipment.InvSlot == "Held In Off-hand" ? equipment : offhand;
        }
        public void EquipRanged(Equipment equipment) => ranged = equipment.InvSlot == "Ranged" ? equipment : ranged;
        #endregion EquipGear

    }
}
