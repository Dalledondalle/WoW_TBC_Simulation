using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Simulation.Library
{
    public class Warlock : PlayerClass
    {
        #region SpellNames
        private const string shadowBoltStr = "Shadow Bolt";
        private const string immolateStr = "Immolate";
        private const string soulFireStr = "Soul Fire";
        private const string rainOfFireStr = "Rain of Fire";
        private const string searingPainStr = "Searing Pain";
        private const string hellFireStr = "Hellfire";
        private const string shadowburnStr = "Shadowburn";
        private const string fireboltStr = "Firebolt";
        private const string lashOfPainStr = "Lash of Pain";
        private const string incinerateStr = "Incinerate";
        private const string conflagrateStr = "Conflagrate";
        private const string shadowfuryStr = "Shadowfury";
        private const string unstableAfflictionStr = "Unstable Affliction";
        private const string corruptionStr = "Corruption";
        private const string seedOfCorruption = "Seed of Corruption";
        private const string lifeTapStr = "Life Tap";
        private const string curseOfTheElementsStr = "Curse of the Elements";
        private const string curseOfRecklessnessStr = "Curse of Recklessness";
        private const string curseOfDoomStr = "Curse of Doom";
        private const string amplifyCurseStr = "Amplify Curse";
        private const string curseOfAgonyStr = "Curse of Agony";
        private const string curseOfTongues = "Curse of Tongues";
        private const string drainManaStr = "Drain Mana";
        private const string drainSoulStr = "Drain Soul";
        private const string howlOfTerrorStr = "Howl of Terror";
        private const string fearStr = "Fear";
        private const string deathCoilStr = "Death Coil";
        private const string curseOfWeaknessStr = "Curse of Weakness";
        private const string drainLifeStr = "Drain Life";
        private const string fireShieldStr = "Fire Shield";
        private const string bloodPactStr = "Blood Pact";
        private const string soothingKissStr = "Soothing Kiss";
        private const string lesserInvisibilityStr = "Lesser Invisibility";
        private const string seductionStr = "Seduction";
        private const string demonArmorStr = "Demon Armor";
        private const string felArmorStr = "Fel Armor";
        private const string demonicSacrificeStr = "Demonic Sacrifice";
        private const string soulLinkStr = "Soul Link";
        private const string felguardStr = "Felguard";
        private const string spellstoneStr = "Spellstone";
        private const string firestoneStr = "Firestone";
        #endregion SpellNames
        #region Talents
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
        public int ShadowfuryRank
        {
            set
            {
                RemoveTalent(shadowfuryStr);
                if (value > 0)
                {
                    Talents.Add(shadowfury);
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

        private static string[] destructionSpells = new[] { immolateStr, shadowBoltStr, rainOfFireStr, hellFireStr, searingPainStr, soulFireStr, incinerateStr, shadowburnStr, conflagrateStr, shadowfuryStr };

        private static string[] baneAffectedSpells1 = new[] { immolateStr, shadowBoltStr };
        private static string[] baneAffectedSpells2 = new[] { soulFireStr };
        private const string baneStr = "Bane";
        private Talent[] banes = new Talent[]
        {
            new(){ ID = "17788", Level = 1, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -100 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -400 } } },
            new(){ ID = "17789", Level = 2, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -200 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -800 } } },
            new(){ ID = "17790", Level = 3, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -300 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -1200 } } },
            new(){ ID = "17791", Level = 4, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -400 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -1600 } } },
            new(){ ID = "17792", Level = 5, Name = baneStr, Effects = new() { new(){ AffectedSpells = baneAffectedSpells1.ToList(), Modify = Modify.Casttime, Value = -500 }, new(){AffectedSpells = baneAffectedSpells2.ToList(), Modify = Modify.Casttime, Value = -2000 } } }
        };

        private static string[] cataclysmAffectedSpells => destructionSpells;
        private const string cataclysmStr = "Cataclysm";
        private Talent[] cataclysms = new Talent[]
        {
            new(){ ID = "17778", Level = 1, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -1 } } },
            new(){ ID = "17779", Level = 2, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -2 } } },
            new(){ ID = "17780", Level = 3, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -3 } } },
            new(){ ID = "17781", Level = 4, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -4 } } },
            new(){ ID = "17782", Level = 5, Name = cataclysmStr, Effects = new() { new(){ AffectedSpells = cataclysmAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = -5 } } }
        };

        private static string[] improvedShadowBoltAffectedSpells = new string[] { shadowBoltStr };
        private const string improvedShadowBoltStr = "Improved Shadow Bolt";
        private Talent[] improvedShadowBolts = new Talent[]
        {
            new(){ ID = "17793", Level = 1, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 4 } } },
            new(){ ID = "17796", Level = 2, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 8 } } },
            new(){ ID = "17801", Level = 3, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 12 } } },
            new(){ ID = "17802", Level = 4, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 16 } } },
            new(){ ID = "17803", Level = 5, Name = improvedShadowBoltStr, Effects = new() { new(){ AffectedSpells = improvedShadowBoltAffectedSpells.ToList() , Modify = Modify.ManaPercent, Value = 20 } } },
        };

        private static string[] improvedFireboltAffectSpells = new[] { fireboltStr };
        private const string improvedFireboltStr = "Improved Firebolt";
        private Talent[] improvedFirebolts = new Talent[]
        {
            new(){ ID = "18126", Level = 1, Name = improvedFireboltStr, Effects = new() { new(){ AffectedSpells = improvedFireboltAffectSpells.ToList() , Modify = Modify.Casttime, Value = -250 } } },
            new(){ ID = "18127", Level = 2, Name = improvedFireboltStr, Effects = new() { new(){ AffectedSpells = improvedFireboltAffectSpells.ToList() , Modify = Modify.Casttime, Value = -500 } } }
        };

        private static string[] improvedLashOfPainAffectedSpells = new[] { lashOfPainStr };
        private const string improvedLashOfPainStr = "Improved Lash of Pain";
        private Talent[] improvedLastOfPain= new Talent[]
        {
            new(){ ID = "18128", Level = 1, Name = improvedLashOfPainStr, Effects = new() { new(){ AffectedSpells = improvedLashOfPainAffectedSpells.ToList() , Modify = Modify.Cooldown, Value = -3000 } } },
            new(){ ID = "18129", Level = 2, Name = improvedLashOfPainStr, Effects = new() { new(){ AffectedSpells = improvedLashOfPainAffectedSpells.ToList() , Modify = Modify.Cooldown, Value = -6000 } } }
        };

        private static string[] devastationAffectedSpells => destructionSpells;
        private const string devastationStr = "Devastation";
        private Talent[] devasations = new Talent[]
        {
            new(){ ID = "18130", Level = 1, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.SpellCritChance, Value = 1 } } },
            new(){ ID = "18131", Level = 2, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.SpellCritChance, Value = 2 } } },
            new(){ ID = "18132", Level = 3, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.SpellCritChance, Value = 3 } } },
            new(){ ID = "18133", Level = 4, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.SpellCritChance, Value = 4 } } },
            new(){ ID = "18134", Level = 5, Name = devastationStr, Effects = new() { new(){ AffectedSpells = devastationAffectedSpells.ToList() , Modify = Modify.SpellCritChance, Value = 5 } } }
        };

        private static string[] shadowburnAffectedSpells = new[] { shadowburnStr };
        private Talent shadowburn = new() { ID = "17877", Level = 1, Name = shadowburnStr, Effects = new() { new() { AffectedSpells = shadowburnAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 17962 } } };

        private static string[] improvedSearingPainAffectedSpells = new[] { searingPainStr };
        private const string improvedSearingPainStr = "Improved Searing Pain";
        private Talent[] improvedSearingPain = new Talent[]
        {
            new() { ID = "17927", Level = 1, Name = improvedSearingPainStr, Effects = new() { new() { AffectedSpells = improvedSearingPainAffectedSpells.ToList(), Modify = Modify.SpellCritChance, Value = 4 } } },
            new() { ID = "17929", Level = 2, Name = improvedSearingPainStr, Effects = new() { new() { AffectedSpells = improvedSearingPainAffectedSpells.ToList(), Modify = Modify.SpellCritChance, Value = 7 } } },
            new() { ID = "17930", Level = 3, Name = improvedSearingPainStr, Effects = new() { new() { AffectedSpells = improvedSearingPainAffectedSpells.ToList(), Modify = Modify.SpellCritChance, Value = 10 } } }
        };

        private static string[] improvedImmolateAffectedSpells = new[] { immolateStr };
        private const string improvedImmolateStr = "Improved Immolate";
        private Talent[] improvedImmolate = new Talent[]
        {
            new() { ID = "17815", Level = 1, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 5 } } },
            new() { ID = "17833", Level = 2, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 10 } } },
            new() { ID = "17834", Level = 3, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 15 } } },
            new() { ID = "17835", Level = 4, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 20 } } },
            new() { ID = "17836", Level = 5, Name = improvedImmolateStr, Effects = new() { new() { AffectedSpells = improvedImmolateAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 25 } } }
        };

        private static string[] ruinAffectedSpells => destructionSpells;
        private const string ruinStr = "Ruin";
        private Talent ruin = new() { ID = "17959", Level = 1, Name = ruinStr, Effects = new() { new() { AffectedSpells = ruinAffectedSpells.ToList(), Modify = Modify.CritDamagePercent, Value = 100 } } };

        private static string[] emberstormAffectedSpells = new[] { immolateStr, hellFireStr, rainOfFireStr, incinerateStr, searingPainStr };
        private static string[] emberstormCastAffectedSpells = new[] { incinerateStr };
        private const string emberstormStr = "Emberstorm";
        private Talent[] emberstorms = new Talent[]
        {
        new() { ID = "17954", Level = 1, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 2 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 2 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -2 } } },
        new() { ID = "17955", Level = 2, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 4 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 4 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -4 } } },
        new() { ID = "17956", Level = 3, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 6 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 6 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -6 } } },
        new() { ID = "17957", Level = 4, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 8 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 8 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -8 } } },
        new() { ID = "17958", Level = 5, Name = emberstormStr, Effects = new() { new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.DamagePercent, Value = 10 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.PeriodicDamagePercent, Value = 10 }, new() { AffectedSpells = emberstormAffectedSpells.ToList(), Modify = Modify.CasttimePercent, Value = -10 } } },
        };

        private static string[] conflagrateAffectedSpells = new[] { conflagrateStr };
        private Talent conflagrate = new() { ID = "17962", Level = 1, Name = conflagrateStr, Effects = new() { new() { AffectedSpells = conflagrateAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 17962 } } };

        private static string[] shadowAndFlameAffectedSpells = new[] { incinerateStr, shadowBoltStr };
        private const string shadowAndFlameStr = "Shadow and Flame";

        private Talent[] shadowAndFlames = new Talent[]
        {
            new() { ID = "30288", Level = 1, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 4 } } },
            new() { ID = "30289", Level = 2, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 8 } } },
            new() { ID = "30290", Level = 3, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 12 } } },
            new() { ID = "30291", Level = 4, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 16 } } },
            new() { ID = "30292", Level = 5, Name = shadowAndFlameStr, Effects = new() { new() { AffectedSpells = shadowAndFlameAffectedSpells.ToList(), Modify = Modify.SpellPowerPercent, Value = 20 } } }
        };

        private static string[] shadowfuryAffectedSpells = new[] { shadowfuryStr };
        private Talent shadowfury = new() { ID = "30283", Level = 1, Name = shadowfuryStr, Effects = new() { new() { AffectedSpells = shadowfuryAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 17962 } } };
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
        public int ImprovedCorruptionRank
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
        public int SiphonLifeRank
        {
            set
            {
                RemoveTalent(siphonLifeStr);
                if (value > 0)
                {
                    Talents.Add(siphonLife);
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
        public int ShadowMasteryRank
        {
            set
            {
                RemoveTalent(shadowMasteryStr);
                if (value <= shadowMasterys.Length && value > 0)
                {
                    Talents.Add(shadowMasterys.First(b => b.Level == value));
                }
                else if (value > shadowMasterys.Length)
                {
                    Talents.Add(shadowMasterys.First(b => b.Level == shadowMasterys.Length));
                }
            }
        }

        private static string[] afflictionSpells = new[] { seedOfCorruption, curseOfTheElementsStr, curseOfRecklessnessStr, curseOfDoomStr, curseOfTongues, curseOfWeaknessStr, corruptionStr, drainLifeStr, curseOfAgonyStr, lifeTapStr, deathCoilStr, fearStr, howlOfTerrorStr, drainSoulStr, drainManaStr, amplifyCurseStr };

        private static string[] suppressionAffectedSpells => afflictionSpells;
        private static string suppressionStr = "Suppression";
        private Talent[] suppressions = new Talent[]
        {
            new(){ ID = "18174", Level = 1, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.SpellHitChance, Value = 2 } } },
            new(){ ID = "18175", Level = 2, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.SpellHitChance, Value = 4 } } },
            new(){ ID = "18176", Level = 3, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.SpellHitChance, Value = 6 } } },
            new(){ ID = "18177", Level = 4, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.SpellHitChance, Value = 8 } } },
            new(){ ID = "18178", Level = 5, Name = suppressionStr, Effects = new() { new(){ AffectedSpells = suppressionAffectedSpells.ToList() , Modify = Modify.SpellHitChance, Value = 10 } } }
        };

        private static string[] improvedCorruptionAffectedSpells = new[] { corruptionStr };
        private static string improvedCorruptionStr = "Improved Corruption";
        private Talent[] improvedCorruptions = new Talent[]
        {
            new(){ ID = "17810", Level = 1, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -400 } } },
            new(){ ID = "17811", Level = 2, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -800 } } },
            new(){ ID = "17812", Level = 3, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -1200 } } },
            new(){ ID = "17813", Level = 4, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -1600 } } },
            new(){ ID = "17814", Level = 5, Name = improvedCorruptionStr, Effects = new() { new(){ AffectedSpells = improvedCorruptionAffectedSpells.ToList() , Modify = Modify.Casttime, Value = -2000 } } }
        };

        private static string[] improvedLifeTapAffectedSpells = new[] { lifeTapStr };
        private static string improvedLifeTapStr = "Improved Life Tap";
        private Talent[] improvedLifeTaps = new Talent[]
        {
            new(){ ID = "18182", Level = 1, Name = improvedLifeTapStr, Effects = new() { new(){ AffectedSpells = improvedLifeTapAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 10 } } },
            new(){ ID = "18183", Level = 2, Name = improvedLifeTapStr, Effects = new() { new(){ AffectedSpells = improvedLifeTapAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 20 } } }
        };

        private static string[] soulSiphonAffectedSpells = new[] { drainLifeStr };
        private static string soulSiphonStr = "Soul Siphon";
        private Talent[] soulSiphons = new Talent[]
        {
            new(){ ID = "18182", Level = 1, Name = soulSiphonStr, Effects = new() { new(){ AffectedSpells = soulSiphonAffectedSpells.ToList() , Modify = Modify.Unique, Value = 2 } } },
            new(){ ID = "18183", Level = 2, Name = soulSiphonStr, Effects = new() { new(){ AffectedSpells = soulSiphonAffectedSpells.ToList() , Modify = Modify.Unique, Value = 4 } } }
        };

        private static string[] improvedCurseOfAgonyAffectedSpells = new[] { curseOfAgonyStr };
        private static string improvedCurseOfAgonyStr = "Improved Curse of Agony";
        private Talent[] improvedCurseOfAgonys = new Talent[]
        {
            new(){ ID = "18827", Level = 1, Name = improvedCurseOfAgonyStr, Effects = new() { new(){ AffectedSpells = improvedCurseOfAgonyAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 5 } } },
            new(){ ID = "18829", Level = 2, Name = improvedCurseOfAgonyStr, Effects = new() { new(){ AffectedSpells = improvedCurseOfAgonyAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 10 } } }
        };

        private static string[] amplifyCurseAffectedSpells = new[] { amplifyCurseStr };
        private Talent amplifyCurse = new() { ID = "18827", Level = 1, Name = amplifyCurseStr, Effects = new() { new() { AffectedSpells = amplifyCurseAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 18288 } } };

        private static string[] nightfallAffectedSpells = new[] { drainLifeStr, corruptionStr };
        private static string nightfallStr = "Nightfall";
        private Talent[] nightfalls = new Talent[]
        {
            new(){ ID = "18094", Level = 1, Name = nightfallStr, Effects = new() { new(){ AffectedSpells = nightfallAffectedSpells.ToList() , Modify = Modify.PeriodicProc, Value = 2 } } },
            new(){ ID = "18095", Level = 2, Name = nightfallStr, Effects = new() { new(){ AffectedSpells = nightfallAffectedSpells.ToList() , Modify = Modify.PeriodicProc, Value = 4 } } }
        };

        private static string[] empoweredCorruptionSpells = new[] { corruptionStr };
        private static string empoweredCorruptionStr = "Empowered Corruption";
        private Talent[] empoweredCorruptions = new Talent[]
        {
            new(){ ID = "32381", Level = 1, Name = empoweredCorruptionStr, Effects = new() { new(){ AffectedSpells = empoweredCorruptionSpells.ToList() , Modify = Modify.SpellPowerPercent, Value = 12 } } },
            new(){ ID = "32382", Level = 2, Name = empoweredCorruptionStr, Effects = new() { new(){ AffectedSpells = empoweredCorruptionSpells.ToList() , Modify = Modify.SpellPowerPercent, Value = 24 } } },
            new(){ ID = "32383", Level = 3, Name = empoweredCorruptionStr, Effects = new() { new(){ AffectedSpells = empoweredCorruptionSpells.ToList() , Modify = Modify.SpellPowerPercent, Value = 36 } } }
        };

        private static string[] siphonLifeAffectedSpells = new[] { "Siphon Life" };
        private static string siphonLifeStr = "Siphon Life";
        private Talent siphonLife = new() { ID = "18827", Level = 1, Name = siphonLifeStr, Effects = new() { new() { AffectedSpells = siphonLifeAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 18265 } } };

        private static string[] shadowMasteryAffectedSpellsDamage = new[] { seedOfCorruption, unstableAfflictionStr };
        private static string[] shadowMasteryAffectedSpellsPeriodic= new[] { seedOfCorruption, unstableAfflictionStr, corruptionStr, drainLifeStr, drainSoulStr, curseOfAgonyStr, "Siphon Life", shadowBoltStr, shadowfuryStr, shadowburnStr };
        private static string shadowMasteryStr = "Shadow Mastery";
        private Talent[] shadowMasterys = new Talent[]
        {
            new(){ ID = "18271", Level = 1, Name = shadowMasteryStr, Effects = new() { new(){ AffectedSpells = shadowMasteryAffectedSpellsPeriodic.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 2 }, new(){ AffectedSpells = shadowMasteryAffectedSpellsDamage.ToList() , Modify = Modify.DamagePercent, Value = 2 } } },
            new(){ ID = "18272", Level = 2, Name = shadowMasteryStr, Effects = new() { new(){ AffectedSpells = shadowMasteryAffectedSpellsPeriodic.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 4 }, new(){ AffectedSpells = shadowMasteryAffectedSpellsDamage.ToList() , Modify = Modify.DamagePercent, Value = 4 } } },
            new(){ ID = "18273", Level = 3, Name = shadowMasteryStr, Effects = new() { new(){ AffectedSpells = shadowMasteryAffectedSpellsPeriodic.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 6 }, new(){ AffectedSpells = shadowMasteryAffectedSpellsDamage.ToList() , Modify = Modify.DamagePercent, Value = 6 } } },
            new(){ ID = "18274", Level = 4, Name = shadowMasteryStr, Effects = new() { new(){ AffectedSpells = shadowMasteryAffectedSpellsPeriodic.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 8 }, new(){ AffectedSpells = shadowMasteryAffectedSpellsDamage.ToList() , Modify = Modify.DamagePercent, Value = 8 } } },
            new(){ ID = "18275", Level = 5, Name = shadowMasteryStr, Effects = new() { new(){ AffectedSpells = shadowMasteryAffectedSpellsPeriodic.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 10 }, new(){ AffectedSpells = shadowMasteryAffectedSpellsDamage.ToList() , Modify = Modify.DamagePercent, Value = 10 } } }
        };

        private static string[] contagionAffectedSpells = new[] { corruptionStr, curseOfAgonyStr, seedOfCorruption };
        private static string contagionStr = "Contagion";
        private Talent[] contagions = new Talent[]
        {
            new(){ ID = "30060", Level = 1, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 1 } } },
            new(){ ID = "30061", Level = 2, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 2 } } },
            new(){ ID = "30062", Level = 3, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 3 } } },
            new(){ ID = "30063", Level = 4, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 4 } } },
            new(){ ID = "30064", Level = 5, Name = contagionStr, Effects = new() { new(){ AffectedSpells = contagionAffectedSpells.ToList() , Modify = Modify.PeriodicDamagePercent, Value = 5 } } }
        };

        private static string[] unstableAfflictionAffectedSpells = new[] { unstableAfflictionStr };
        private Talent unstableAffliction = new() { ID = "30108", Level = 1, Name = unstableAfflictionStr, Effects = new() { new() { AffectedSpells = unstableAfflictionAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 30108 } } };
        #endregion AfflictionTalents

        #region DemonologyTalents
        public int ImprovedImpRank
        {
            set
            {
                RemoveTalent(improvedImpStr);
                if (value <= improvedImps.Length && value > 0)
                {
                    Talents.Add(improvedImps.First(b => b.Level == value));
                }
                else if (value > improvedImps.Length)
                {
                    Talents.Add(improvedImps.First(b => b.Level == improvedImps.Length));
                }
            }
        }
        public int FelIntellectRank
        {
            set
            {
                RemoveTalent(felIntellectStr);
                if (value <= felIntellects.Length && value > 0)
                {
                    Talents.Add(felIntellects.First(b => b.Level == value));
                }
                else if (value > felIntellects.Length)
                {
                    Talents.Add(felIntellects.First(b => b.Level == felIntellects.Length));
                }
            }
        }
        public int ImprovedSayaadRank { set
            {
                RemoveTalent(improvedSayaadStr);
                if(value <= improvedSayaads.Length && value > 0)
                {
                    Talents.Add(improvedSayaads.First(b => b.Level == value));
                }
                else if (value > improvedSayaads.Length)
                {
                    Talents.Add(improvedSayaads.First(b => b.Level == improvedSayaads.Length));
                }
            }
        }
        public int DemonicAegisRank
        {
            set
            {
                RemoveTalent(demonicAegisStr);
                if (value <= demonicAegiss.Length && value > 0)
                {
                    Talents.Add(demonicAegiss.First(b => b.Level == value));
                }
                else if (value > demonicAegiss.Length)
                {
                    Talents.Add(demonicAegiss.First(b => b.Level == demonicAegiss.Length));
                }
            }
        }
        public int UnholyPowerRank
        {
            set
            {
                RemoveTalent(unholyPowerStr);
                if (value <= unholyPowers.Length && value > 0)
                {
                    Talents.Add(unholyPowers.First(b => b.Level == value));
                }
                else if (value > unholyPowers.Length)
                {
                    Talents.Add(unholyPowers.First(b => b.Level == unholyPowers.Length));
                }
            }
        }
        public int DemonicSacrificeRank
        {
            set
            {
                RemoveTalent(demonicSacrificeStr);
                if (value > 0)
                {
                    Talents.Add(demonicSacrifice);
                }
            }
        }
        public int SoulLinkRank
        {
            set
            {
                RemoveTalent(soulLinkStr);
                if (value > 0)
                {
                    Talents.Add(soulLink);
                }
            }
        }
        public int FelguardRank
        {
            set
            {
                RemoveTalent(felguardStr);
                if (value > 0)
                {
                    Talents.Add(felguard);
                }
            }
        }
        public int MasterConjurorRank
        {
            set
            {
                RemoveTalent(masterConjurorStr);
                if (value <= masterConjurors.Length && value > 0)
                {
                    Talents.Add(masterConjurors.First(b => b.Level == value));
                }
                else if (value > masterConjurors.Length)
                {
                    Talents.Add(masterConjurors.First(b => b.Level == masterConjurors.Length));
                }
            }
        }
        public int MasterDemonologistRank
        {
            set
            {
                RemoveTalent(masterDemonologistStr);
                if (value <= masterDemonologists.Length && value > 0)
                {
                    Talents.Add(masterDemonologists.First(b => b.Level == value));
                }
                else if (value > masterDemonologists.Length)
                {
                    Talents.Add(masterDemonologists.First(b => b.Level == masterDemonologists.Length));
                }
            }
        }
        public int DemonicKnowledgeRank
        {
            set
            {
                RemoveTalent(demonicKnowledgeStr);
                if (value <= demonicKnowledges.Length && value > 0)
                {
                    Talents.Add(demonicKnowledges.First(b => b.Level == value));
                }
                else if (value > demonicKnowledges.Length)
                {
                    Talents.Add(demonicKnowledges.First(b => b.Level == demonicKnowledges.Length));
                }
            }
        }
        public int DemonicTacticsRank
        {
            set
            {
                RemoveTalent(demonicTacticsStr);
                if (value <= demonicTacticss.Length && value > 0)
                {
                    Talents.Add(demonicTacticss.First(b => b.Level == value));
                }
                else if (value > demonicTacticss.Length)
                {
                    Talents.Add(demonicTacticss.First(b => b.Level == demonicTacticss.Length));
                }
            }
        }

        private static string[] pets = new[] { "Felhunter", "Voidwalker", "Imp", "Succubus", "Incubus", felguardStr };

        private static string[] improvedImpAffectedSpells = new[] { fireShieldStr, fireboltStr, bloodPactStr };
        private static string improvedImpStr = "Improved Imp";
        private Talent[] improvedImps = new Talent[]
        {
            new(){ ID = "18694", Level = 1, Name = improvedImpStr, Effects = new() { new(){ AffectedSpells = improvedImpAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 10 } } },
            new(){ ID = "18695", Level = 2, Name = improvedImpStr, Effects = new() { new(){ AffectedSpells = improvedImpAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 20 } } },
            new(){ ID = "18696", Level = 3, Name = improvedImpStr, Effects = new() { new(){ AffectedSpells = improvedImpAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 30 } } }
        };

        private static string[] felIntellectAffectedSpells => pets;
        private static string felIntellectStr = "Fel Intellect";
        private Talent[] felIntellects = new Talent[]
        {
            new(){ ID = "18731", Level = 1, Name = felIntellectStr, Effects = new() { new(){ AffectedSpells = felIntellectAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 5 }, new() { AffectedSpells = new(){ "Mana" }, Modify = Modify.MaxMana, Value = 1} } },
            new(){ ID = "18743", Level = 2, Name = felIntellectStr, Effects = new() { new(){ AffectedSpells = felIntellectAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 10 }, new() { AffectedSpells = new(){ "Mana" }, Modify = Modify.MaxMana, Value = 2} } },
            new(){ ID = "18744", Level = 3, Name = felIntellectStr, Effects = new() { new(){ AffectedSpells = felIntellectAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 15 }, new() { AffectedSpells = new(){ "Mana" }, Modify = Modify.MaxMana, Value = 3} } }
        };

        private static string[] improvedSayaadAffectedSpellsEffectiveness = new[] { soothingKissStr, lashOfPainStr };
        private static string[] improvedSayaadAffectedSpellsBuffDuration= new[] { lesserInvisibilityStr, seductionStr };
        private static string improvedSayaadStr = "Improved Sayaad";
        private Talent[] improvedSayaads = new Talent[]
        {
            new(){ ID = "18754", Level = 1, Name = improvedSayaadStr, Effects = new() { new(){ AffectedSpells = improvedSayaadAffectedSpellsEffectiveness.ToList() , Modify = Modify.SpellEffectiveness, Value = 10 }, new() { AffectedSpells = improvedSayaadAffectedSpellsBuffDuration.ToList(), Modify = Modify.AuraDuration, Value = 10} } },
            new(){ ID = "18755", Level = 2, Name = improvedSayaadStr, Effects = new() { new(){ AffectedSpells = improvedSayaadAffectedSpellsEffectiveness.ToList() , Modify = Modify.SpellEffectiveness, Value = 20 }, new() { AffectedSpells = improvedSayaadAffectedSpellsBuffDuration.ToList(), Modify = Modify.AuraDuration, Value = 20} } },
            new(){ ID = "18756", Level = 3, Name = improvedSayaadStr, Effects = new() { new(){ AffectedSpells = improvedSayaadAffectedSpellsEffectiveness.ToList() , Modify = Modify.SpellEffectiveness, Value = 30 }, new() { AffectedSpells = improvedSayaadAffectedSpellsBuffDuration.ToList(), Modify = Modify.AuraDuration, Value = 30} } }
        };

        private static string[] demonicAegisAffectedSpells = new[] { demonArmorStr, felArmorStr };
        private static string demonicAegisStr = "Demonic Aegis";
        private Talent[] demonicAegiss = new Talent[]
        {
            new(){ ID = "30143", Level = 1, Name = demonicAegisStr, Effects = new() { new(){ AffectedSpells = demonicAegisAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 10 } } },
            new(){ ID = "30144", Level = 2, Name = demonicAegisStr, Effects = new() { new(){ AffectedSpells = demonicAegisAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 20 } } },
            new(){ ID = "30145", Level = 3, Name = demonicAegisStr, Effects = new() { new(){ AffectedSpells = demonicAegisAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 30 } } }
        };

        private static string[] unholyPowerAffectedSpells => pets;
        private static string unholyPowerStr = "Unholy Power";
        private Talent[] unholyPowers = new Talent[]
        {
            new(){ ID = "18769", Level = 1, Name = unholyPowerStr, Effects = new() { new(){ AffectedSpells = unholyPowerAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 4 } } },
            new(){ ID = "18770", Level = 2, Name = unholyPowerStr, Effects = new() { new(){ AffectedSpells = unholyPowerAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 8 } } },
            new(){ ID = "18771", Level = 3, Name = unholyPowerStr, Effects = new() { new(){ AffectedSpells = unholyPowerAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 12 } } },
            new(){ ID = "18772", Level = 4, Name = unholyPowerStr, Effects = new() { new(){ AffectedSpells = unholyPowerAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 16 } } },
            new(){ ID = "18773", Level = 5, Name = unholyPowerStr, Effects = new() { new(){ AffectedSpells = unholyPowerAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 20 } } }
        };

        private static string[] demonicSacrificeAffectedSpells = new[] { demonicSacrificeStr };
        private Talent demonicSacrifice = new() { ID = "18788", Level = 1, Name = demonicSacrificeStr, Effects = new() { new() { AffectedSpells = demonicSacrificeAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 18788 } } };

        private static string[] soulLinkAffectedSpells = new[] { soulLinkStr };
        private Talent soulLink = new() { ID = "19028", Level = 1, Name = soulLinkStr, Effects = new() { new() { AffectedSpells = soulLinkAffectedSpells.ToList(), Modify = Modify.LearnSpell, Value = 19028 } } };

        private static string[] felguardSpells = new[] { felguardStr };
        private Talent felguard = new() { ID = "30146", Level = 1, Name = felguardStr, Effects = new() { new() { AffectedSpells = felguardSpells.ToList(), Modify = Modify.LearnSpell, Value = 30146 } } };

        private static string[] masterConjurorAffectedSpells = new[] { spellstoneStr, firestoneStr };
        private static string masterConjurorStr = "Master Conjuror";
        private Talent[] masterConjurors = new Talent[]
        {
            new(){ ID = "18767", Level = 1, Name = masterConjurorStr, Effects = new() { new(){ AffectedSpells = masterConjurorAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 15 } } },
            new(){ ID = "18768", Level = 2, Name = masterConjurorStr, Effects = new() { new(){ AffectedSpells = masterConjurorAffectedSpells.ToList() , Modify = Modify.SpellEffectiveness, Value = 30 } } },
        };

        private static string[] masterDemonologistAffectedSpells => pets;
        private static string masterDemonologistStr = "Master Demonologist";
        private Talent[] masterDemonologists = new Talent[]
        {
            new(){ ID = "23785", Level = 1, Name = masterDemonologistStr, Effects = new() { new(){ AffectedSpells = masterDemonologistAffectedSpells.ToList() , Modify = Modify.Unique, Value = 1 } } },
            new(){ ID = "23822", Level = 2, Name = masterDemonologistStr, Effects = new() { new(){ AffectedSpells = masterDemonologistAffectedSpells.ToList() , Modify = Modify.Unique, Value = 2 } } },
            new(){ ID = "23823", Level = 3, Name = masterDemonologistStr, Effects = new() { new(){ AffectedSpells = masterDemonologistAffectedSpells.ToList() , Modify = Modify.Unique, Value = 3 } } },
            new(){ ID = "23824", Level = 4, Name = masterDemonologistStr, Effects = new() { new(){ AffectedSpells = masterDemonologistAffectedSpells.ToList() , Modify = Modify.Unique, Value = 4 } } },
            new(){ ID = "23825", Level = 5, Name = masterDemonologistStr, Effects = new() { new(){ AffectedSpells = masterDemonologistAffectedSpells.ToList() , Modify = Modify.Unique, Value = 5 } } }
        };

        private static string[] demonKnowledgeAffectedSpells => pets;
        private static string demonicKnowledgeStr = "Demonic Knowledge";
        private Talent[] demonicKnowledges = new Talent[]
        {
            new(){ ID = "35691", Level = 1, Name = demonicKnowledgeStr, Effects = new() { new(){ AffectedSpells = demonKnowledgeAffectedSpells.ToList() , Modify = Modify.Unique, Value = 4 } } },
            new(){ ID = "35692", Level = 2, Name = demonicKnowledgeStr, Effects = new() { new(){ AffectedSpells = demonKnowledgeAffectedSpells.ToList() , Modify = Modify.Unique, Value = 8 } } },
            new(){ ID = "35693", Level = 3, Name = demonicKnowledgeStr, Effects = new() { new(){ AffectedSpells = demonKnowledgeAffectedSpells.ToList() , Modify = Modify.Unique, Value = 12 } } }

        };

        private static string[] demonicTaticsAffectedSpellsPets => pets;
        private static string[] demonicTaticsAffectedSpellsPlayer => new[] {"Spell Crit"};
        private static string demonicTacticsStr = "Demonic Tactics";
        private Talent[] demonicTacticss = new Talent[]
        {
            new(){ ID = "30242", Level = 1, Name = demonicTacticsStr, Effects = new() { new(){ AffectedSpells = demonicTaticsAffectedSpellsPets.ToList() , Modify = Modify.SpellEffectiveness, Value = 1 }, new(){ AffectedSpells = demonicTaticsAffectedSpellsPlayer.ToList() , Modify = Modify.SpellCritChance, Value = 1 } } },
            new(){ ID = "30245", Level = 2, Name = demonicTacticsStr, Effects = new() { new(){ AffectedSpells = demonicTaticsAffectedSpellsPets.ToList() , Modify = Modify.SpellEffectiveness, Value = 2 }, new(){ AffectedSpells = demonicTaticsAffectedSpellsPlayer.ToList() , Modify = Modify.SpellCritChance, Value = 2 } } },
            new(){ ID = "30246", Level = 3, Name = demonicTacticsStr, Effects = new() { new(){ AffectedSpells = demonicTaticsAffectedSpellsPets.ToList() , Modify = Modify.SpellEffectiveness, Value = 3 }, new(){ AffectedSpells = demonicTaticsAffectedSpellsPlayer.ToList() , Modify = Modify.SpellCritChance, Value = 3 } } },
            new(){ ID = "30247", Level = 4, Name = demonicTacticsStr, Effects = new() { new(){ AffectedSpells = demonicTaticsAffectedSpellsPets.ToList() , Modify = Modify.SpellEffectiveness, Value = 4 }, new(){ AffectedSpells = demonicTaticsAffectedSpellsPlayer.ToList() , Modify = Modify.SpellCritChance, Value = 4 } } },
            new(){ ID = "30248", Level = 5, Name = demonicTacticsStr, Effects = new() { new(){ AffectedSpells = demonicTaticsAffectedSpellsPets.ToList() , Modify = Modify.SpellEffectiveness, Value = 5 }, new(){ AffectedSpells = demonicTaticsAffectedSpellsPlayer.ToList() , Modify = Modify.SpellCritChance, Value = 5 } } }
        };
        #endregion DemonologyTalents
        #endregion Talents

        public Warlock()
        {
            baseSpellCrit = 1.7;
            baseIntellect = 131;
            UpdateStats();
            baseMana = 2615;
            mana = MaxMana;
            UpdateStats();
        }
        #region CastSpells
        public void CastFelArmor(Spell spell, double fightTick)
        {
            Aura a = new(spell.ID, spell.Name, AuraType.Buff);
            if(!buffs.Any(x => x.Value.Name == spell.Name))
            {
                int sp = GetFelArmorSP(spell);
                double modifier = GetModMultiplicativeFromTalents(spell, Modify.SpellEffectiveness);
                sp = (int)(sp * modifier);
                var effect = new Effect() { Value = sp, AuraID = spell.ID, Modify = Modify.SpellPower };
                a.Effects.Add(effect);
                buffs.Add(fightTick, a);
            }
            UpdateStats();
            //"Surrounds the caster with fel energy, increasing the amount of health generated through spells and effects by 20% and increasing spell damage by up to 100. &nbsp;Only one type of Armor spell can be active on the Warlock at any time. &nbsp;Lasts 30 min."
        }
        private int GetFelArmorSP(Spell spell)
        {
            string tooltip = spell.ToolTipText;
            int startIndex = tooltip.IndexOf("spell damage by up to ") + "spell damage by up to ".Length;
            int endIndex = tooltip.IndexOf(". &nbsp;Only");
            string spStr = tooltip.Substring(startIndex, endIndex - startIndex);
            return int.Parse(spStr);
        }
        public bool HaveManaForSpell(Spell spell)
        {
            return spell.Cost < Mana;
        }
        public void CastLifeTap(Spell lifetap, Report report = null, double fightTick = 0)
        {
            if (report is null) report = new();
            if (lifetap.Name != lifeTapStr) return;
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
            report.ReportManaGained(ManaGained, lifetap, fightTick + WaitForNextCast());
            this.mana += ManaGained;
            if (this.mana > MaxMana) this.mana = MaxMana;
        }
        private int ManaGainedFromLifetap(Spell lifetap)
        {
            return int.Parse(lifetap.ToolTipText.Split(' ')[1]);
        }
        protected double CastShadowBolt(Spell shadowbolt, Unit target, Report report = null, double fightTick = 0)
        {
            if (report is null) report = new();
            if (shadowbolt.Name != shadowBoltStr) return 0;
            double dmg;
            Random rnd = new();
            double dmgModTalents = GetModMultiplicativeFromTalents(shadowbolt, Modify.DamagePercent);
            double casttimeMod = GetModAdditivesFromTalents(shadowbolt, Modify.Casttime);
            shadowbolt.CastTime += casttimeMod;
            var CritMod = GetModAdditivesFromTalents(shadowbolt, Modify.SpellCritChance);
            bool isCrit = rnd.Next(100) <= SpellCrit + CritMod;
            lastSpelledCasted = shadowbolt;
            dmg = rnd.Next(GetShadowboltMinDmg(shadowbolt), GetShadowboltMaxDmg(shadowbolt));
            dmg = dmg + ((SpellPower + ShadowPower) * GetShadowboltSPMod(shadowbolt));
            mana -= shadowbolt.Cost;
            if (!DidHit(lastSpelledCasted)) dmg = 0;
            dmg = dmg * dmgModTalents;
            if(Talents.Any(x => x.Name == baneStr))
                dmg = isCrit ? (dmg * 2) * GetModMultiplicativeFromAuras(Modify.SpellCritChance, shadowbolt) : dmg;
            else
                dmg = isCrit ? dmg * 1.5 : dmg;
            report.ReportDamage(dmg, lastSpelledCasted, fightTick + WaitForNextCast(), dmg > 0, isCrit);
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
            int decimalSeprator = 0;
            if (sub.Contains(',')) decimalSeprator = sub.Substring(sub.IndexOf(',')).Length;
            if (sub.Contains('.')) decimalSeprator = sub.Substring(sub.IndexOf('.')).Length;
            double divider = Math.Pow(10, decimalSeprator-1);
            double spMod = double.Parse(sub) / divider;
            return spMod;
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

        public override void CastSpell(Spell spell, Unit target, double fightTick, Report report)
        {
            switch (spell.Name)
            {
                case shadowBoltStr:
                    CastShadowBolt(spell, target, report, fightTick);
                    break;
                default:
                    break;
            }
            return;
        }
    }
}
