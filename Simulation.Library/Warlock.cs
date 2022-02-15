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
        public int MP5 => GetAllGear().Where(e => e is not null).Select(e => e.ManaRegn).Sum() + mp5 + auras.Select(e => e.FlatManaRegenMod).Sum();
        private int mp5 = 0;
        public int SpellHitRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellHitRating).Sum() + spellHitRating + auras.Select(e => e.SpellHitRatingMod).Sum();
        private int spellHitRating = 0;
        public double SpellHit => (SpellHitRating / 12.6) + spellHit + auras.Select(e => e.SpellHitModifer).Sum();
        private double spellHit = 0;
        public int SpellCritRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellCritRating).Sum() + spellCritRating + auras.Select(e => e.SpellCritRatingMod).Sum();
        public int spellCritRating = 0;
        public int Intellect => (int)((intellect +
                                GetAllGear().Where(e => e is not null).Select(e => e.Intellect).Sum()+
                                auras.Select(e => e.FlatIntMod).Sum()) *
                                (1 + (auras.Select(e => e.IntModifer).Sum()/100)));

        private int intellect = 131;
        public double SpellCrit => spellCrit + (Intellect / 81.9) + (SpellCritRating / 22.1) + auras.Select(e => e.SpellCritModifer).Sum();
        private double spellCrit = 1.7;
        public int SpellPower =>    (int)((GetAllGear().Where(e => e is not null).Select(e => e.SpellPower).Sum() +
                                    spellPower +
                                    auras.Select(e => e.FlatSpellMod).Sum()) *
                                    (1 + (auras.Select(e => e.SpellModifer).Sum()/100)));
        private int spellPower = 0;
        public int ShadowPower =>   (int)((GetAllGear().Where(e => e is not null).Select(e => e.ShadowSpellPower).Sum() +
                                    shadowPower +
                                    auras.Select(e => e.FlatShadowMod).Sum()) *
                                    (1 + (auras.Select(e => e.ShadowModifer).Sum()/100)));
        private int shadowPower = 0;
        public int FirePower =>     (int)((GetAllGear().Where(e => e is not null).Select(e => e.FireSpellPower).Sum() +
                                    firePower +
                                    auras.Select(e => e.FlatFireMod).Sum()) *
                                    (1 + (auras.Select(e => e.FireModifer).Sum()/100)));
        private int firePower = 0;
        public int ArcanePower =>   (int)((GetAllGear().Where(e => e is not null).Select(e => e.ArcaneSpellPower).Sum() +
                                    arcanePower +
                                    auras.Select(e => e.FlatArcaneMod).Sum()) *
                                    (1 + (auras.Select(e => e.ArcaneModifer).Sum()/100)));
        private int arcanePower = 0;
        public int FrostPower =>    (int)((GetAllGear().Where(e => e is not null).Select(e => e.FrostSpellPower).Sum() +
                                    frostPower +
                                    auras.Select(e => e.FlatFrostMod).Sum()) *
                                    (1 + (auras.Select(e => e.FrostModifer).Sum()/100)));
        private int frostPower;
        public int SpellHasteRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellHasteRating).Sum() + spellHasteRating + auras.Select(e => e.SpellHasteRatingMod).Sum();
        public int spellHasteRating = 0;
        //Hasted Cast Time = Base Cast Time / (1 + ( Spell Haste Rating / 1577 ) )
        public double SpellHaste => (SpellHasteRating / 15.77) + spellHaste + auras.Select(e => e.SpellHasteModifer).Sum();
        private double spellHaste = 0;
        public int Mana => mana;
        private int mana = 0;
        private int baseMana = 2615;
        public int MaxMana => (Math.Min(20, Intellect) + 15 * (Intellect - Math.Min(20, Intellect))) + baseMana;
        #endregion Stats

        #region SpellsAndAuras
        public Spell lastSpelledCasted { get; private set; }
        private List<Aura> auras => Buffs.Concat(Debuffs).ToList();
        public List<Aura> Buffs => buffs.OrderBy(x => x.EndTimer).ToList();
        private List<Aura> buffs = new();

        public List<Aura> Debuffs => debuffs;
        private List<Aura> debuffs = new();
        #endregion SpellsAndAuras

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

        #region Talents
        public List<Talent> Talents { get; set; } = new();
        public int BaneRank { set
            {
                if(value < 1 )
                {
                    Talents.RemoveAll(t => t.Name == "Bane");
                }
                else if(value < 6)
                {
                    Talents.Add(banes.First(b => b.Level == value));
                }
                else
                {
                    Talents.Add(banes.First(b => b.Level == 5));
                }
            }
        }


        private Talent[] banes = new Talent[]
        {
            new(){ ID = "17788", Level = 1, Name = "Bane", Effects = new() { new(){ AffectedSpells = new(){ "Immolate", "Shadow Bolt"}, Modify = Modify.Casttime, Value = -100 }, new(){AffectedSpells = new(){ "Soul Fire"}, Modify = Modify.Casttime, Value = -400 } } },
            new(){ ID = "17789", Level = 2, Name = "Bane", Effects = new() { new(){ AffectedSpells = new(){ "Immolate", "Shadow Bolt"}, Modify = Modify.Casttime, Value = -200 }, new(){AffectedSpells = new(){ "Soul Fire"}, Modify = Modify.Casttime, Value = -800 } } },
            new(){ ID = "17790", Level = 3, Name = "Bane", Effects = new() { new(){ AffectedSpells = new(){ "Immolate", "Shadow Bolt"}, Modify = Modify.Casttime, Value = -300 }, new(){AffectedSpells = new(){ "Soul Fire"}, Modify = Modify.Casttime, Value = -1200 } } },
            new(){ ID = "17791", Level = 4, Name = "Bane", Effects = new() { new(){ AffectedSpells = new(){ "Immolate", "Shadow Bolt"}, Modify = Modify.Casttime, Value = -400 }, new(){AffectedSpells = new(){ "Soul Fire"}, Modify = Modify.Casttime, Value = -1600 } } },
            new(){ ID = "17792", Level = 5, Name = "Bane", Effects = new() { new(){ AffectedSpells = new(){ "Immolate", "Shadow Bolt"}, Modify = Modify.Casttime, Value = -500 }, new(){AffectedSpells = new(){ "Soul Fire"}, Modify = Modify.Casttime, Value = -2000 } } }
        };
        #endregion Talents

        public Warlock()
        {
            mana = MaxMana;
        }

        private Equipment[] GetAllGear()
        {
            return new Equipment[] { head, neck, shoulders, back, chest, wrist, hands, waist, legs, feet, ring1, ring2, trinket1, trinket2, mainhand, offhand, ranged };
        }
        #region Auras
        public void AddAura(Aura Aura)
        {
            if (Aura is null) return;
            if (Aura.AuraType == AuraType.Buff) buffs.Add(Aura);
            if (Aura.AuraType == AuraType.Debuff) debuffs.Add(Aura);
        }
        public void CastFelArmor(Aura Aura)
        {
            if (Aura is null || Aura.Name != "Fel Armor") return;
            if (!Buffs.Where(a => a.Name == Aura.Name).Any()) Buffs.Add(Aura);
        }
        public void CheckBuffs(double timestamp)
        {
            buffs.RemoveAll(b => b.EndTimer < timestamp);
        }
        #endregion Auras

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
            this.spellHasteRating += hasteRating;
        }
        public void AddHaste(double haste)
        {
            this.spellHaste += haste;
        }
        public void AddCritRating(int critRating)
        {
            this.spellCritRating += critRating;
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
            report.ReportManaGained(ManaGained, $"Life Tap");
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
            var talentsToModSB = Talents.Where(x => x.Effects.Any(e => e.AffectedSpells.Contains(shadowbolt.Name))).Select(x => x.Effects);
            List<Effect> SBEffects = new();
            foreach (var effect in talentsToModSB) SBEffects.Concat(effect);
            var CritMod = SBEffects.Where(x => x.Modify == Modify.Critchance).Select(x => x.Value).Sum();
            bool isCrit = rnd.Next(100) <= SpellCrit + CritMod;
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
        private bool DidHit(Spell spell, double hitmodifier = 0)
        {
            Random rnd = new();
            return rnd.Next(0) < 83 + SpellHit + hitmodifier;
        }
        public double WaitForNextCast()
        {
            if (lastSpelledCasted is null) return 0;
            else
            {
                double gcd = (lastSpelledCasted.GCD) / (1 + (SpellHaste / 100));
                if (gcd < 1000) gcd = 1000;
                double castTime = (lastSpelledCasted.CastTime) / (1 + (SpellHaste / 100));
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
