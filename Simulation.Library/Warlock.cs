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
                                GetAllGear().Where(e => e is not null).Select(e => e.Intellect).Sum() +
                                auras.Select(e => e.FlatIntMod).Sum()) *
                                (1 + (auras.Select(e => e.IntModifer).Sum() / 100)));

        private int intellect = 131;
        public double SpellCrit => spellCrit + (Intellect / 81.9) + (SpellCritRating / 22.1) + auras.Select(e => e.SpellCritModifer).Sum();
        private double spellCrit = 1.7;
        public int SpellPower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.SpellPower).Sum() +
                                    spellPower +
                                    auras.Select(e => e.FlatSpellMod).Sum()) *
                                    (1 + (auras.Select(e => e.SpellModifer).Sum() / 100)));
        private int spellPower = 0;
        public int ShadowPower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.ShadowSpellPower).Sum() +
                                    shadowPower +
                                    auras.Select(e => e.FlatShadowMod).Sum()) *
                                    (1 + (auras.Select(e => e.ShadowModifer).Sum() / 100)));
        private int shadowPower = 0;
        public int FirePower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.FireSpellPower).Sum() +
                                    firePower +
                                    auras.Select(e => e.FlatFireMod).Sum()) *
                                    (1 + (auras.Select(e => e.FireModifer).Sum() / 100)));
        private int firePower = 0;
        public int ArcanePower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.ArcaneSpellPower).Sum() +
                                    arcanePower +
                                    auras.Select(e => e.FlatArcaneMod).Sum()) *
                                    (1 + (auras.Select(e => e.ArcaneModifer).Sum() / 100)));
        private int arcanePower = 0;
        public int FrostPower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.FrostSpellPower).Sum() +
                                    frostPower +
                                    auras.Select(e => e.FlatFrostMod).Sum()) *
                                    (1 + (auras.Select(e => e.FrostModifer).Sum() / 100)));
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
        #region DestructionTalents
        public int BaneRank
        {
            set
            {
                RemoveTalent(baneStr);
                if (value <= banes.Length && value > 0)
                {
                    Talents.Add(banes.First(b => b.Level == value));
                }
                else if(value > banes.Length)
                {
                    Talents.Add(banes.First(b => b.Level == banes.Length));
                }
            }
        }
        public int CataclysmRank
        {
            set
            {
                RemoveTalent(cataclysmStr);
                if (value <= cataclysms.Length && value > 0)
                {
                    Talents.Add(cataclysms.First(b => b.Level == value));
                }
                else if(value > cataclysms.Length)
                {
                    Talents.Add(cataclysms.First(b => b.Level == cataclysms.Length));
                }
            }
        }
        public int ImprovedShadowBoltsRank
        {
            set
            {
                RemoveTalent(improvedShadowBoltStr);
                if (value <= improvedShadowBolts.Length && value > 0)
                {
                    Talents.Add(improvedShadowBolts.First(b => b.Level == value));
                }
                else if(value > improvedShadowBolts.Length)
                {
                    Talents.Add(improvedShadowBolts.First(b => b.Level == improvedShadowBolts.Length));
                }
            }
        }
        public int ImprovedFireboltRank
        {
            set
            {
                RemoveTalent(improvedFireboltStr);
                if (value <= improvedFirebolts.Length && value > 0)
                {
                    Talents.Add(improvedFirebolts.First(b => b.Level == value));
                }
                else if(value > improvedFirebolts.Length)
                {
                    Talents.Add(improvedFirebolts.First(b => b.Level == improvedFirebolts.Length));
                }
            }
        }
        public int ImprovedLashOfPainRank
        {
            set
            {
                RemoveTalent(improvedLashOfPainStr);
                if (value <= improvedLastOfPain.Length && value > 0)
                {
                    Talents.Add(improvedLastOfPain.First(b => b.Level == value));
                }
                else if (value > improvedLastOfPain.Length)
                {
                    Talents.Add(improvedLastOfPain.First(b => b.Level == improvedLastOfPain.Length));
                }
            }
        }
        public int DevastationRank
        {
            set
            {
                RemoveTalent(devastationStr);
                if (value <= devasations.Length && value > 0)
                {
                    Talents.Add(devasations.First(b => b.Level == value));
                }
                else if (value > devasations.Length)
                {
                    Talents.Add(devasations.First(b => b.Level == devasations.Length));
                }
            }
        }
        public int ShadowburnRank
        {
            set
            {
                RemoveTalent(shadowburnStr);
                if(value > 0)
                {
                    Talents.Add(shadowburn);
                }
            }
        }
        public int ConflagrateRank
        {
            set
            {
                RemoveTalent(conflagrateStr);
                if (value > 0)
                {
                    Talents.Add(conflagrate);
                }
            }
        }
        public int RuinRank
        {
            set
            {
                RemoveTalent(ruinStr);
                if (value > 0)
                {
                    Talents.Add(ruin);
                }
            }
        }
        public int ImprovedSearingPainRank
        {
            set
            {
                RemoveTalent(improvedSearingPainStr);
                if (value <= improvedSearingPain.Length && value > 0)
                {
                    Talents.Add(improvedSearingPain.First(b => b.Level == value));
                }
                else if (value > improvedSearingPain.Length)
                {
                    Talents.Add(improvedSearingPain.First(b => b.Level == improvedSearingPain.Length));
                }
            }
        }
        public int ImprovedImmolateRank
        {
            set
            {
                RemoveTalent(improvedImmolateStr);
                if (value <= improvedImmolate.Length && value > 0)
                {
                    Talents.Add(improvedImmolate.First(b => b.Level == value));
                }
                else if (value > improvedImmolate.Length)
                {
                    Talents.Add(improvedImmolate.First(b => b.Level == improvedImmolate.Length));
                }
            }
        }
        public int EmberstormRank
        {
            set
            {
                RemoveTalent(emberstormStr);
                if (value <= emberstorms.Length && value > 0)
                {
                    Talents.Add(emberstorms.First(b => b.Level == value));
                }
                else if (value > emberstorms.Length)
                {
                    Talents.Add(emberstorms.First(b => b.Level == emberstorms.Length));
                }
            }
        }
        public int ShadowAndFlameRank
        {
            set
            {
                RemoveTalent(shadowAndFlameStr);
                if (value <= shadowAndFlames.Length && value > 0)
                {
                    Talents.Add(shadowAndFlames.First(b => b.Level == value));
                }
                else if (value > shadowAndFlames.Length)
                {
                    Talents.Add(shadowAndFlames.First(b => b.Level == shadowAndFlames.Length));
                }
            }
        }

        private static string[] baneAffectedSpells1 = new[] { "Immolate", "Shadow Bolt" };
        private static string[] baneAffectedSpells2 = new[] { "Soul Fire" };
        private static string baneStr = "Bane";
        private Talent[] banes = new Talent[]
        {
            new(){ ID = "17788", Level = 1, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -100 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -400 } } },
            new(){ ID = "17789", Level = 2, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -200 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -800 } } },
            new(){ ID = "17790", Level = 3, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -300 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -1200 } } },
            new(){ ID = "17791", Level = 4, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -400 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -1600 } } },
            new(){ ID = "17792", Level = 5, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -500 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -2000 } } }
        };

        private static string[] cataclysmAffectedSpells = new[] { "Immolate", "Shadow Bolt", "Rain of Fire", "Searing Pain", "Soul Fire", "Hellfire" };
        private static string cataclysmStr = "Cataclysm";
        private Talent[] cataclysms = new Talent[]
        {
            new(){ ID = "17778", Level = 1, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -1 } } },
            new(){ ID = "17779", Level = 2, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -2 } } },
            new(){ ID = "17780", Level = 3, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -3 } } },
            new(){ ID = "17781", Level = 4, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -4 } } },
            new(){ ID = "17782", Level = 5, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -5 } } }
        };

        private static string[] improvedShadowBoltAffectedSpells = new string[] { "Shadow Bolt" };
        private static string improvedShadowBoltStr = "Improved Shadow Bolt";
        private Talent[] improvedShadowBolts = new Talent[]
        {
            new(){ ID = "17793", Level = 1, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 4 } } },
            new(){ ID = "17796", Level = 2, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 8 } } },
            new(){ ID = "17801", Level = 3, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 12 } } },
            new(){ ID = "17802", Level = 4, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 16 } } },
            new(){ ID = "17803", Level = 5, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 20 } } },
        };

        private static string[] improvedFireboltAffectSpells = new[] { "Firebolt" };
        private static string improvedFireboltStr = "Improved Firebolt";
        private Talent[] improvedFirebolts = new Talent[]
        {
            new(){ ID = "18126", Level = 1, Name = improvedFireboltStr, Effects = new() { new(){ AffectedSpells = improvedFireboltAffectSpells.ToList() , Modify = Modify.Casttime, Value = -250 } } },
            new(){ ID = "18127", Level = 2, Name = improvedFireboltStr, Effects = new() { new(){ AffectedSpells = improvedFireboltAffectSpells.ToList() , Modify = Modify.Casttime, Value = -500 } } }
        };

        private static string[] improvedLashOfPainAffectedSpells = new[] { "Lash of Pain" };
        private static string improvedLashOfPainStr = "Improved Lash of Pain";
        private Talent[] improvedLastOfPain= new Talent[]
        {
            new(){ ID = "18128", Level = 1, Name = improvedLashOfPainStr, Effects = new() { new(){ AffectedSpells = improvedLashOfPainAffectedSpells.ToList() , Modify = Modify.Cooldown, Value = -3000 } } },
            new(){ ID = "18129", Level = 2, Name = improvedLashOfPainStr, Effects = new() { new(){ AffectedSpells = improvedLashOfPainAffectedSpells.ToList() , Modify = Modify.Cooldown, Value = -6000 } } }
        };

        private static string[] devastationAffectedSpells = new[] { "Immolate", "Shadow Bolt", "Searing Pain", "Soul Fire"};
        private static string devastationStr = "Devastation";
        private Talent[] devasations = new Talent[]
        {
            new(){ ID = "18130", Level = 1, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.Critchance, Value = 1 } } },
            new(){ ID = "18131", Level = 2, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.Critchance, Value = 2 } } },
            new(){ ID = "18132", Level = 3, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.Critchance, Value = 3 } } },
            new(){ ID = "18133", Level = 4, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.Critchance, Value = 4 } } },
            new(){ ID = "18134", Level = 5, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.Critchance, Value = 5 } } }
        };

        private static string[] shadowburnAffectedSpells = new[] { "Shadowburn" };
        private static string shadowburnStr = "Shadowburn";
        private Talent shadowburn = new() { ID = "17877", Level = 1, Name = shadowburnStr, Effects = new() { new() { AffectedSpells = shadowburnAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 17962 } } };

        private static string[] improvedSearingPainAffectedSpells = new[] { "Searing Pain" };
        private static string improvedSearingPainStr = "Improved Searing Pain";
        private Talent[] improvedSearingPain = new Talent[]
        {
            new() { ID = "17927", Level = 1, Name = improvedSearingPainStr, Effects = new() { new() { AffectedSpells = improvedSearingPainAffectedSpells.ToList(), Modify = Modify.Critchance, Value = 4 } } },
            new() { ID = "17929", Level = 2, Name = improvedSearingPainStr, Effects = new() { new() { AffectedSpells = improvedSearingPainAffectedSpells.ToList(), Modify = Modify.Critchance, Value = 7 } } },
            new() { ID = "17930", Level = 3, Name = improvedSearingPainStr, Effects = new() { new() { AffectedSpells = improvedSearingPainAffectedSpells.ToList(), Modify = Modify.Critchance, Value = 10 } } }
        };

        private static string[] improvedImmolateAffectedSpells = new[] { "Immolate" };
        private static string improvedImmolateStr = "Improved Immolate";
        private Talent[] improvedImmolate = new Talent[]
        {
            new() { ID = "17815", Level = 1, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 5 } } },
            new() { ID = "17833", Level = 2, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 10 } } },
            new() { ID = "17834", Level = 3, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 15 } } },
            new() { ID = "17835", Level = 4, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 20 } } },
            new() { ID = "17836", Level = 5, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 25 } } }
        };

        private static string[] ruinAffectedSpells = new[] { "Immolate", "Shadow Bolt", "Searing Pain", "Hellfire", "Soul Fire" };
        private static string ruinStr = "Ruin";
        private Talent ruin = new() { ID = "17959", Level = 1, Name = ruinStr, Effects = new() { new() { AffectedSpells = ruinAffectedSpells.ToList(), Modify = Modify.CritDamagePercent, Value = 100 } } };

        private static string[] emberstormAffectedSpells = new[] { "Immolate", "Hellfire", "Rain of Fire", "Incinerate", "Searing Pain" };
        private static string[] emberstormCastAffectedSpells = new[] { "Incinerate" };
        private static string emberstormStr = "Emberstorm";
        private Talent[] emberstorms = new Talent[]
        {
        new() { ID = "17954", Level = 1, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 2 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 2 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -2 } } },
        new() { ID = "17955", Level = 2, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 4 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 4 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -4 } } },
        new() { ID = "17956", Level = 3, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 6 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 6 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -6 } } },
        new() { ID = "17957", Level = 4, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 8 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 8 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -8 } } },
        new() { ID = "17958", Level = 5, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 10 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 10 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -10 } } },
        };

        private static string[] conflagrateAffectedSpells = new[] { "Conflagrate" };
        private static string conflagrateStr = "Conflagrate";
        private Talent conflagrate = new() { ID = "17962", Level = 1, Name = conflagrateStr, Effects = new() { new() { AffectedSpells = conflagrateAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 17962 } } };

        private static string[] shadowAndFlameAffectedSpells = new[] { "Incinerate", "Shadow Bolt" };
        private static string shadowAndFlameStr = "Shadow and Flame";
        private Talent[] shadowAndFlames = new Talent[]
        {
            new() { ID = "30288", Level = 1, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 4 } } },
            new() { ID = "30289", Level = 2, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 8 } } },
            new() { ID = "30290", Level = 3, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 12 } } },
            new() { ID = "30291", Level = 4, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 16 } } },
            new() { ID = "30292", Level = 5, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 20 } } }
        };
        #endregion DestructionTalents

        #region AfflictionTalents
        public int SuppressionRank
        {
            set
            {
                RemoveTalent(suppressionStr);
                if (value <= suppressions.Length && value > 0)
                {
                    Talents.Add(suppressions.First(b => b.Level == value));
                }
                else if (value > suppressions.Length)
                {
                    Talents.Add(suppressions.First(b => b.Level == suppressions.Length));
                }
            }
        }
        public int CorruptionRank
        {
            set
            {
                RemoveTalent(improvedCorruptionStr);
                if (value <= improvedCorruptions.Length && value > 0)
                {
                    Talents.Add(improvedCorruptions.First(b => b.Level == value));
                }
                else if (value > improvedCorruptions.Length)
                {
                    Talents.Add(improvedCorruptions.First(b => b.Level == improvedCorruptions.Length));
                }
            }
        }
        public int ImprovedLifeTapRank
        {
            set
            {
                RemoveTalent(improvedLifeTapStr);
                if (value <= improvedLifeTaps.Length && value > 0)
                {
                    Talents.Add(improvedLifeTaps.First(b => b.Level == value));
                }
                else if (value > improvedLifeTaps.Length)
                {
                    Talents.Add(improvedLifeTaps.First(b => b.Level == improvedLifeTaps.Length));
                }
            }
        }
        public int SoulSiphonRank
        {
            set
            {
                RemoveTalent(soulSiphonStr);
                if (value <= soulSiphons.Length && value > 0)
                {
                    Talents.Add(soulSiphons.First(b => b.Level == value));
                }
                else if (value > soulSiphons.Length)
                {
                    Talents.Add(soulSiphons.First(b => b.Level == soulSiphons.Length));
                }
            }
        }
        public int ImprovedCurseOfAgonyRank
        {
            set
            {
                RemoveTalent(improvedCurseOfAgonyStr);
                if (value <= improvedCurseOfAgonys.Length && value > 0)
                {
                    Talents.Add(improvedCurseOfAgonys.First(b => b.Level == value));
                }
                else if (value > improvedCurseOfAgonys.Length)
                {
                    Talents.Add(improvedCurseOfAgonys.First(b => b.Level == improvedCurseOfAgonys.Length));
                }
            }
        }
        public int AmplifyCurseRank
        {
            set
            {
                RemoveTalent(amplifyCurseStr);
                if (value > 0)
                {
                    Talents.Add(amplifyCurse);
                }
            }
        }
        public int SiphoneLifeRank
        {
            set
            {
                RemoveTalent(siphoneLifeStr);
                if (value > 0)
                {
                    Talents.Add(siphoneLife);
                }
            }
        }
        public int UnstableAfflictionRank
        {
            set
            {
                RemoveTalent(unstableAfflictionStr);
                if (value > 0)
                {
                    Talents.Add(unstableAffliction);
                }
            }
        }
        public int NightfallRank
        {
            set
            {
                RemoveTalent(nightfallStr);
                if (value <= nightfalls.Length && value > 0)
                {
                    Talents.Add(nightfalls.First(b => b.Level == value));
                }
                else if (value > nightfalls.Length)
                {
                    Talents.Add(nightfalls.First(b => b.Level == nightfalls.Length));
                }
            }
        }
        public int EmpoweredCorruptionRank
        {
            set
            {
                RemoveTalent(empoweredCorruptionStr);
                if (value <= empoweredCorruptions.Length && value > 0)
                {
                    Talents.Add(empoweredCorruptions.First(b => b.Level == value));
                }
                else if (value > empoweredCorruptions.Length)
                {
                    Talents.Add(empoweredCorruptions.First(b => b.Level == empoweredCorruptions.Length));
                }
            }
        }
        public int ContagionRank
        {
            set
            {
                RemoveTalent(contagionStr);
                if (value <= contagions.Length && value > 0)
                {
                    Talents.Add(contagions.First(b => b.Level == value));
                }
                else if (value > contagions.Length)
                {
                    Talents.Add(contagions.First(b => b.Level == contagions.Length));
                }
            }
        }

        private static string[] suppressionAffectedSpells = new[] { "Corruption", "Drain Life", "Curse of Weakness", "Curse of Tongues", "Fear", "Curse of Doom", "Curse of Agony", "Drain Soul", "Drain Mana", "Curse of Recklessness", "Curse of the Elements", "Howl of Terror", "Seed of Corruption", "Curse of Exhaustion", "Curse of Exhaustion", "Death Coil", "Shadow Embrace", "Unstable Affliction" };
        private static string suppressionStr = "Suppression";
        private Talent[] suppressions = new Talent[]
        {
            new(){ ID = "18174", Level = 1, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.Hitchance, Value = 2 } } },
            new(){ ID = "18175", Level = 2, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.Hitchance, Value = 4 } } },
            new(){ ID = "18176", Level = 3, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.Hitchance, Value = 6 } } },
            new(){ ID = "18177", Level = 4, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.Hitchance, Value = 8 } } },
            new(){ ID = "18178", Level = 5, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.Hitchance, Value = 10 } } }
        };

        private static string[] improvedCorruptionAffectedSpells = new[] { "Corruption" };
        private static string improvedCorruptionStr = "Improved Corruption";
        private Talent[] improvedCorruptions = new Talent[]
        {
            new(){ ID = "17810", Level = 1, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -400 } } },
            new(){ ID = "17811", Level = 2, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -800 } } },
            new(){ ID = "17812", Level = 3, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -1200 } } },
            new(){ ID = "17813", Level = 4, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -1600 } } },
            new(){ ID = "17814", Level = 5, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -2000 } } }
        };

        private static string[] improvedLifeTapAffectedSpells = new[] { "Life Tap" };
        private static string improvedLifeTapStr = "Improved Life Tap";
        private Talent[] improvedLifeTaps = new Talent[]
        {
            new(){ ID = "18182", Level = 1, Name = improvedLifeTapStr, Effects = new() { new(){ AffectedSpells = improvedLifeTapAffectedSpells.ToList() , Modify = Modify.Unique, Value = 10 } } },
            new(){ ID = "18183", Level = 2, Name = improvedLifeTapStr, Effects = new() { new(){ AffectedSpells = improvedLifeTapAffectedSpells.ToList() , Modify = Modify.Unique, Value = 20 } } }
        };

        private static string[] soulSiphonAffectedSpells = new[] { "Drain Life" };
        private static string soulSiphonStr = "Soul Siphon";
        private Talent[] soulSiphons = new Talent[]
        {
            new(){ ID = "18182", Level = 1, Name = soulSiphonStr, Effects = new() { new(){ AffectedSpells = soulSiphonAffectedSpells.ToList() , Modify = Modify.Unique, Value = 2 } } },
            new(){ ID = "18183", Level = 2, Name = soulSiphonStr, Effects = new() { new(){ AffectedSpells = soulSiphonAffectedSpells.ToList() , Modify = Modify.Unique, Value = 4 } } }
        };

        private static string[] improvedCurseOfAgonyAffectedSpells = new[] { "Curse of Agony" };
        private static string improvedCurseOfAgonyStr = "Improved Curse of Agony";
        private Talent[] improvedCurseOfAgonys = new Talent[]
        {
            new(){ ID = "18827", Level = 1, Name = improvedCurseOfAgonyStr, Effects = new() { new(){ AffectedSpells = improvedCurseOfAgonyAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 5 } } },
            new(){ ID = "18829", Level = 2, Name = improvedCurseOfAgonyStr, Effects = new() { new(){ AffectedSpells = improvedCurseOfAgonyAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 10 } } }
        };

        private static string[] amplifyCurseAffectedSpells = new[] { "Amplify Curse" };
        private static string amplifyCurseStr = "Amplify Curse";
        private Talent amplifyCurse = new() { ID = "18827", Level = 1, Name = amplifyCurseStr, Effects = new() { new() { AffectedSpells = amplifyCurseAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 18288 } } };

        private static string[] nightfallAffectedSpells = new[] { "Drain Life", "Corruption" };
        private static string nightfallStr = "Nightfall";
        private Talent[] nightfalls = new Talent[]
        {
            new(){ ID = "18094", Level = 1, Name = nightfallStr, Effects = new() { new(){ AffectedSpells = improvedCurseOfAgonyAffectedSpells.ToList() , Modify = Modify.PeriodicProc, Value = 2 } } },
            new(){ ID = "18095", Level = 2, Name = nightfallStr, Effects = new() { new(){ AffectedSpells = improvedCurseOfAgonyAffectedSpells.ToList() , Modify = Modify.PeriodicProc, Value = 4 } } }
        };

        private static string[] empoweredCorruptionSpells = new[] { "Corruption" };
        private static string empoweredCorruptionStr = "Empowered Corruption";
        private Talent[] empoweredCorruptions = new Talent[]
        {
            new(){ ID = "32381", Level = 1, Name = empoweredCorruptionStr, Effects = new() { new(){ AffectedSpells = empoweredCorruptionSpells.ToList() , Modify = Modify.SpellPowerPercent, Value = 12 } } },
            new(){ ID = "32382", Level = 2, Name = empoweredCorruptionStr, Effects = new() { new(){ AffectedSpells = empoweredCorruptionSpells.ToList() , Modify = Modify.SpellPowerPercent, Value = 24 } } },
            new(){ ID = "32383", Level = 3, Name = empoweredCorruptionStr, Effects = new() { new(){ AffectedSpells = empoweredCorruptionSpells.ToList() , Modify = Modify.SpellPowerPercent, Value = 36 } } }
        };

        private static string[] siphoneLifeAffectedSpells = new[] { "Siphone Life" };
        private static string siphoneLifeStr = "Siphone Life";
        private Talent siphoneLife = new() { ID = "18827", Level = 1, Name = siphoneLifeStr, Effects = new() { new() { AffectedSpells = siphoneLifeAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 18265 } } };

        private static string[] contagionAffectedSpells = new[] { "Corruption", "Curse of Agony", "Seed of Corruption" };
        private static string contagionStr = "Contagion";
        private Talent[] contagions = new Talent[]
        {
            new(){ ID = "30060", Level = 1, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 1 } } },
            new(){ ID = "30061", Level = 2, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 2 } } },
            new(){ ID = "30062", Level = 3, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 3 } } },
            new(){ ID = "30063", Level = 4, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 4 } } },
            new(){ ID = "30064", Level = 5, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 5 } } }
        };

        private static string[] unstableAfflictionAffectedSpells = new[] { "Unstable Affliction" };
        private static string unstableAfflictionStr = "Unstable Affliction";
        private Talent unstableAffliction = new() { ID = "30108", Level = 1, Name = unstableAfflictionStr, Effects = new() { new() { AffectedSpells = unstableAfflictionAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 30108 } } };
        #endregion AfflictionTalents

        #region DemonologyTalents

        #endregion DemonologyTalents

        private void RemoveTalent(string talentName)
        {
            Talents.RemoveAll(x => x.Name == talentName);
        }
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
            if (mana + manaFromLT > MaxMana)
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
            double dmgModTalents = GetDamagePercentIncreaseFromTalents(shadowbolt);
            double casttimeMod = GetCastTimeModFromTalents(shadowbolt);
            shadowbolt.CastTime += casttimeMod;
            var CritMod = GetCritModFromTalents(shadowbolt);
            bool isCrit = rnd.Next(100) <= SpellCrit + CritMod;
            lastSpelledCasted = shadowbolt;
            dmg = rnd.Next(GetShadowboltMinDmg(shadowbolt), GetShadowboltMaxDmg(shadowbolt));
            dmg = dmg + ((SpellPower + ShadowPower) * GetShadowboltSPMod(shadowbolt));
            mana -= shadowbolt.Cost;
            if (!DidHit(lastSpelledCasted)) dmg = 0;
            dmg = dmg * (1 + (dmgModTalents / 100));
            dmg = isCrit ? dmg * 1.5 : dmg;
            report.ReportDamage(dmg, lastSpelledCasted, dmg > 0, isCrit);
            return dmg;
        }

        private double GetDamagePercentIncreaseFromTalents(Spell spell)
        {
            var spellEffects = GetAllEffectsBySpell(spell).ToList();
            return spellEffects.Where(x => x.Modify == Modify.DamagePercent).Select(x => x.Value).Sum();
        }

        private double GetHitFromTalents(Spell spell)
        {
            var spellEffects = GetAllEffectsBySpell(spell).ToList();
            return spellEffects.Where(x => x.Modify == Modify.Hitchance).Select(x => x.Value).Sum();
        }

        private double GetCastTimeModFromTalents(Spell spell)
        {
            var spellEffects = GetAllEffectsBySpell(spell).ToList();
            return spellEffects.Where(x => x.Modify == Modify.Casttime).Select(x => x.Value).Sum();
        }

        private double GetCritModFromTalents(Spell spell)
        {
            var spellEffects = GetAllEffectsBySpell(spell).ToList();
            return spellEffects.Where(x => x.Modify == Modify.Critchance).Select(x => x.Value).Sum();
        }

        private List<Effect> GetAllEffectsBySpell(Spell spell)
        {
            var talentsToModSpell = Talents.Where(x => x.Effects.Any(e => e.AffectedSpells.Any(y => y == spell.Name)));
            var effectsToSpell = talentsToModSpell.Select(x => x.Effects.Select(y => y)).ToList();
            List<Effect> effects = new();
            effects.AddRange(effectsToSpell.SelectMany(list => list.Where(effect => effect.AffectedSpells.Any(x => x == spell.Name))));
            return effects;
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
            if (equipment.InvSlot == "Main Hand")
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
            if (mainhand is not null && mainhand.InvSlot == "Two-Hand") return;
            offhand = equipment.InvSlot == "Held In Off-hand" ? equipment : offhand;
        }
        public void EquipRanged(Equipment equipment) => ranged = equipment.InvSlot == "Ranged" ? equipment : ranged;
        #endregion EquipGear

    }
}
