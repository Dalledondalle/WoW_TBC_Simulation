using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        private Spell[] LifeTaps = new Spell[]
        {
            new LifeTap1(),
            new LifeTap2(),
            new LifeTap3(),
            new LifeTap4(),
            new LifeTap5(),
            new LifeTap6(),
            new LifeTap7()
        };
        private Spell[] shadowBolts = new Spell[]
        {
            new ShadowBolt1(),
            new ShadowBolt2(),
            new ShadowBolt3(),
            new ShadowBolt4(),
            new ShadowBolt5(),
            new ShadowBolt6(),
            new ShadowBolt7(),
            new ShadowBolt8(),
            new ShadowBolt9(),
            new ShadowBolt10(),
            new ShadowBolt11(),
        };
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
        public bool CanCastShadowBolt(int rank)
        {
            return shadowBolts.First(x => x.Rank == rank).Mana < Mana;
        }
        public void CastLifeTap(int rank = 0, Report report = null)
        {
            if (report is null) report = new();
            int ManaGained = 0;
            lastSpelledCasted = rank == 0 ? LifeTaps.First(x => x.Rank == LifeTaps.Max(y => y.Rank)) : LifeTaps.First(x => x.Rank == rank);
            if(mana - lastSpelledCasted.Mana > MaxMana)
            {
                ManaGained = MaxMana - mana;
            }
            else
            {
                ManaGained = -lastSpelledCasted.Mana;
            }
            report.ReportManaGained(ManaGained, $"Life Tap{lastSpelledCasted.Rank}");
            this.mana -= lastSpelledCasted.Mana;
            if (this.mana > MaxMana) this.mana = MaxMana;
        }
        public double CastShadowBolt(int rank = 0, Report report = null)
        {
            if (report is null) report = new();
            double dmg;
            Random rnd = new();
            bool isCrit = rnd.Next(100) <= Crit;
            lastSpelledCasted = rank == 0 ? shadowBolts.First(x => x.Rank == shadowBolts.Max(y => y.Rank)) : shadowBolts.First(x => x.Rank == rank);
            dmg = rnd.Next(lastSpelledCasted.MinDmg, lastSpelledCasted.MaxDmg);
            dmg = dmg + ((SpellPower + ShadowPower) * lastSpelledCasted.SpellMultiplier);
            mana -= lastSpelledCasted.Mana;
            if (!DidHit(lastSpelledCasted)) dmg = 0;
            dmg = isCrit ? dmg * 1.5 : dmg;
            report.ReportDamage(dmg, lastSpelledCasted, dmg > 0, isCrit);
            return dmg;
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

    public class Spell
    {
        public String Name { get; set; }
        public int ID { get; set; }
        public int Rank { get; set; }
        public double CastTime { get; protected set; }
        public int Range { get; protected set; }
        public int Mana { get; protected set; }
        public double SpellMultiplier { get; protected set; }
        public double GCD { get; protected set; }
        public int MaxDmg { get; protected set; }
        public int MinDmg { get; protected set; }
    }
    #region ShadowBolts
    public class ShadowBolt : Spell
    {
        public ShadowBolt()
        {
            Name = "Shadow Bolt";
            Range = 30;
            GCD = 1500;
        }
    }
    public class ShadowBolt1 : ShadowBolt
    {
        public ShadowBolt1()
        {
            ID = 686;
            Rank = 1;
            Mana = 25;
            CastTime = 1700;
            SpellMultiplier = 0.14;
            MinDmg = 13;
            MaxDmg = 18;
        }
    }
    public class ShadowBolt2 : ShadowBolt
    {
        public ShadowBolt2()
        {
            ID = 695;
            Rank = 2;
            Mana = 40;
            CastTime = 2200;
            SpellMultiplier = 0.299;
            MinDmg = 26;
            MaxDmg = 32;
        }
    }
    public class ShadowBolt3 : ShadowBolt
    {
        public ShadowBolt3()
        {
            ID = 705;
            Rank = 3;
            Mana = 70;
            CastTime = 2800;
            SpellMultiplier = 0.56;
            MinDmg = 52;
            MaxDmg = 61;
        }
    }
    public class ShadowBolt4 : ShadowBolt
    {
        public ShadowBolt4()
        {
            ID = 1088;
            Rank = 4;
            Mana = 110;
            CastTime = 3000;
            SpellMultiplier = 0.857;
            MinDmg = 92;
            MaxDmg = 104;
        }
    }
    public class ShadowBolt5 : ShadowBolt
    {
        public ShadowBolt5()
        {
            ID = 1106;
            Rank = 5;
            Mana = 160;
            CastTime = 3000;
            SpellMultiplier = 0.857;
            MinDmg = 150;
            MaxDmg = 170;
        }
    }
    public class ShadowBolt6 : ShadowBolt
    {
        public ShadowBolt6()
        {
            ID = 11659;
            Rank = 6;
            Mana = 210;
            CastTime = 3000;
            SpellMultiplier = 0.857;
            MinDmg = 213;
            MaxDmg = 240;
        }
    }
    public class ShadowBolt7 : ShadowBolt
    {
        public ShadowBolt7()
        {
            ID = 11659;
            Rank = 7;
            Mana = 265;
            CastTime = 3000;
            SpellMultiplier = 0.857;
            MinDmg = 292;
            MaxDmg = 327;
        }
    }
    public class ShadowBolt8 : ShadowBolt
    {
        public ShadowBolt8()
        {
            ID = 11660;
            Rank = 8;
            CastTime = 3000;
            Mana = 315;
            SpellMultiplier = 0.857;
            MaxDmg = 415;
            MinDmg = 373;
        }
    }
    public class ShadowBolt9 : ShadowBolt
    {
        public ShadowBolt9()
        {
            ID = 11661;
            Rank = 9;
            Mana = 370;
            CastTime = 3000;
            SpellMultiplier = 0.857;
            MinDmg = 470;
            MaxDmg = 522;
        }
    }
    public class ShadowBolt10 : ShadowBolt
    {
        public ShadowBolt10()
        {
            ID = 25307;
            Rank = 10;
            Mana = 380;
            CastTime = 3000;
            SpellMultiplier = 0.857;
            MinDmg = 497;
            MaxDmg = 554;
        }
    }
    public class ShadowBolt11 : ShadowBolt
    {
        public ShadowBolt11()
        {
            ID = 27209;
            Rank = 11;
            Mana = 420;
            CastTime = 3000;
            SpellMultiplier = 0.857;
            MinDmg = 544;
            MaxDmg = 607;
        }
    }
    #endregion ShadowBolts

    #region LifeTaps
    public class LifeTap : Spell
    {
        public LifeTap()
        {
            Name = "Life Tap";
            Range = -1;
            CastTime = 0;
            GCD = 0;
            SpellMultiplier = 0;
            MinDmg = 0;
            MaxDmg = 0;
        }
    }
    public class LifeTap1 : LifeTap
    {
        public LifeTap1()
        {
            ID = 1454;
            Rank = 1;
            Mana = -30;
        }
    }
    public class LifeTap2 : LifeTap
    {
        public LifeTap2()
        {
            ID = 1455;
            Rank = 2;
            Mana = -75;
        }
    }
    public class LifeTap3 : LifeTap
    {
        public LifeTap3()
        {
            ID = 1456;
            Rank = 3;
            Mana = -140;
        }
    }
    public class LifeTap4 : LifeTap
    {
        public LifeTap4()
        {
            ID = 11687;
            Rank = 4;
            Mana = -220;
        }
    }
    public class LifeTap5 : LifeTap
    {
        public LifeTap5()
        {
            ID = 11688;
            Rank = 5;
            Mana = -310;
        }
    }
    public class LifeTap6 : LifeTap
    {
        public LifeTap6()
        {
            ID = 11689;
            Rank = 6;
            Mana = -430;
        }
    }
    public class LifeTap7 : LifeTap
    {
        public LifeTap7()
        {
            ID = 27222;
            Rank = 7;
            Mana = -582;
        }
    }

    #endregion LifeTaps
}
