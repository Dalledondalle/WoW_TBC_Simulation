using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Library
{
    public abstract class PlayerClass : Unit
    {
        #region Stats
        public override void UpdateStats()
        {
            MP5 = GetAllGear().Where(e => e is not null).Select(e => e.ManaRegn).Sum() + basemp5 + (int)GetModAdditivesFromAuras(Modify.MP5Flat) + GetSumOfSocketBonuses(Modify.MP5Flat) + GetSumOfSocketsMP5();
            SpellHitRating = GetAllGear().Where(e => e is not null).Select(e => e.SpellHitRating).Sum() + baseSpellHitRating + (int)GetModAdditivesFromAuras(Modify.SpellHitRating) + GetSumOfSocketBonuses(Modify.SpellHitRating) + GetSumOfSocketsSpellHit();
            SpellHit = (SpellHitRating / 12.6) + baseSpellHit + (int)GetModAdditivesFromAuras(Modify.SpellHitChance);
            SpellCritRating = GetAllGear().Where(e => e is not null).Select(e => e.SpellCritRating).Sum() + baseSpellCritRating + (int)GetModAdditivesFromAuras(Modify.SpellCritRating) + GetSumOfSocketBonuses(Modify.SpellCritRating) + GetSumOfSocketsSpellCritRating();
            Intellect = (int)((baseIntellect +
                                            GetSumOfSocketBonuses(Modify.IntellectFlat) +
                                            GetSumOfSocketsIntellect() +
                                            GetAllGear().Where(e => e is not null).Select(e => e.Intellect).Sum() +
                                            (int)GetModAdditivesFromAuras(Modify.IntellectFlat)) *
                                            GetModMultiplicativeFromAuras(Modify.IntellectPercent));
            SpellCrit = (Intellect / 81.9) + (SpellCritRating / 22.1) + baseSpellCrit + (int)GetModAdditivesFromAuras(Modify.SpellCritChance) + GetSumOfSocketBonuses(Modify.SpellCritRating);
            SpellPower = (int)((GetAllGear().Where(e => e is not null).Select(e => e.SpellPower).Sum() +
                                            baseSpellPower +
                                            GetSumOfSocketBonuses(Modify.SpellPower) +
                                            GetSumOfSocketsSpellPower() +
                                            (int)GetModAdditivesFromAuras(Modify.SpellPower)) *
                                            GetModMultiplicativeFromAuras(Modify.SpellPowerPercent));

            ShadowPower = (int)((GetAllGear().Where(e => e is not null).Select(e => e.ShadowSpellPower).Sum() +
                                            baseShadowPower +
                                            (int)GetModAdditivesFromAuras(Modify.ShadowPower)) *
                                            GetModMultiplicativeFromAuras(Modify.ShadowPercent));

            FirePower = (int)((GetAllGear().Where(e => e is not null).Select(e => e.FireSpellPower).Sum() +
                                            baseFirePower +
                                            (int)GetModAdditivesFromAuras(Modify.FirePower)) *
                                            GetModMultiplicativeFromAuras(Modify.FirePercent));

            ArcanePower = (int)((GetAllGear().Where(e => e is not null).Select(e => e.ArcaneSpellPower).Sum() +
                                            baseArcanePower +
                                            (int)GetModAdditivesFromAuras(Modify.ArcanePower)) *
                                            GetModMultiplicativeFromAuras(Modify.ArcanePercent));

            FrostPower = (int)((GetAllGear().Where(e => e is not null).Select(e => e.FrostSpellPower).Sum() +
                                            baseFrostPower +
                                            (int)GetModAdditivesFromAuras(Modify.FrostPower)) *
                                            GetModMultiplicativeFromAuras(Modify.FrostPercent));

            SpellHasteRating = GetAllGear().Where(e => e is not null).Select(e => e.SpellHasteRating).Sum() + baseSpellHasteRating + (int)GetModAdditivesFromAuras(Modify.SpellHasteRating) + GetSumOfSocketBonuses(Modify.SpellHasteRating) + GetSumOfSocketsSpellHasteRating();
            //Hasted Cast Time = Base Cast Time / (1 + ( Spell Haste Rating / 1577 ) )
            SpellHaste = (SpellHasteRating / 15.77) + baseSpellHaste + (int)GetModAdditivesFromAuras(Modify.SpellHastePercent);
        }
        #endregion Stats

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
        protected Effect metaGemEffect
        {
            get
            {
                var meta = GetAllGems().FirstOrDefault(x => x.Color == "Meta");
                if (meta is null) return null;
                if (meta.ID == 25893) return new() { InternalCD = 35, Modify = Modify.SpellHasteRating, Value = 320, ProcChance = 15000, AuraID = "32837" };
                if (meta.ID == 34220) return new() { Modify = Modify.CritDamagePercent, Value = 3 };
                if (meta.ID == 25901) return new() { Modify = Modify.ManaReturnFlat, Value = 300, ProcChance = 5, InternalCD = 15000 };
                return null;
            }
        }


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

        public List<Talent> Talents { get; set; } = new();
        protected void RemoveTalent(string talentName)
        {
            Talents.RemoveAll(x => x.Name == talentName);
        }
        protected Equipment[] GetAllGear()
        {
            return new Equipment[] { head, neck, shoulders, back, chest, wrist, hands, waist, legs, feet, ring1, ring2, trinket1, trinket2, mainhand, offhand, ranged };
        }
        protected int GetSumOfSocketsSpellPower()
        {
            return GetAllGems().Sum(x => x.SpellPower);
        }
        protected int GetSumOfSocketsSpellCritRating()
        {
            return GetAllGems().Sum(x => x.SpellCritRating);
        }
        protected int GetSumOfSocketsSpellHit()
        {
            return GetAllGems().Sum(x => x.SpellHitRating);
        }
        protected int GetSumOfSocketsSpellHasteRating()
        {
            return GetAllGems().Sum(x => x.SpellHasteRating);
        }
        protected int GetSumOfSocketsIntellect()
        {
            return GetAllGems().Sum(x => x.Intellect);
        }
        protected int GetSumOfSocketsMP5()
        {
            return GetAllGems().Sum(x => x.MP5);
        }
        protected List<Gem> GetAllGems()
        {
            List<Gem> Gems = new();
            foreach (var item in GetAllGear())
            {
                if (item is not null)
                    Gems = Gems.Concat(item.Gems).ToList();
            }
            return Gems;
        }
        public void SocketItem(Equipment equipment, Gem gem, int gemSlot)
        {
            equipment.SocketGem(gem, gemSlot);
            UpdateStats();
        }
        protected int GetSumOfSocketBonuses(Modify stat)
        {
            return GetAllGear()
                    .Where(e => e is not null && e.IsSocketBonusActive)
                    .Select(b => Wowhead.SocketBonuses
                                    .FirstOrDefault(x => x.ID == b.SocketBonus) is not null
                                    && Wowhead.SocketBonuses.FirstOrDefault(x => x.ID == b.SocketBonus).Stat == stat
                                    ? Wowhead.SocketBonuses.FirstOrDefault(x => x.ID == b.SocketBonus).Amount : 0).Sum();
        }

        #region ModsFromTalents
        protected double GetModAdditivesFromTalents(Spell spell, Modify mod)
        {
            var spellEffects = GetAllEffectsBySpell(spell).ToList();
            return spellEffects.Where(x => x.Modify == mod).Select(x => x.Value).Sum();
        }
        protected double GetModMultiplicativeFromTalents(Spell spell, Modify mod)
        {
            double modifier = 1.0;
            var spellEffects = GetAllEffectsBySpell(spell).ToList();
            double[] mods = spellEffects.Where(x => x.Modify == mod).Select(x => (double)x.Value).ToArray();
            foreach (var item in mods)
            {
                double t = (1 + (item / 100));
                modifier = modifier * t;
            }
            return modifier;
        }
        protected List<Effect> GetAllEffectsBySpell(Spell spell)
        {
            var talentsToModSpell = Talents.Where(x => x.Effects.Any(e => e.AffectedSpells.Any(y => y == spell.Name || y == "All")));
            var effectsToSpell = talentsToModSpell.Select(x => x.Effects.Select(y => y)).ToList();
            List<Effect> effects = new();
            effects.AddRange(effectsToSpell.SelectMany(list => list.Where(effect => effect.AffectedSpells.Any(x => x == spell.Name || x == "All"))));
            return effects;
        }
        protected List<Effect> GetAllEffectsFromAuraBySpell(Spell spell = null)
        {
            string spellName = spell is null ? "All" : spell.Name;
            var talentsToModSpell = auras.Where(x => x.Value.Effects.Any(e => e.AffectedSpells.Any(y => y == spellName || y == "All")));
            var effectsToSpell = talentsToModSpell.Select(x => x.Value.Effects.Select(y => y)).ToList();
            List<Effect> effects = new();
            effects.AddRange(effectsToSpell.SelectMany(list => list.Where(effect => effect.AffectedSpells.Any(x => x == spellName || x == "All"))));
            return effects;
        }
        protected double GetModMultiplicativeFromAuras(Modify mod, Spell spell = null)
        {
            double modifier = 1.0;
            var spellEffects = GetAllEffectsFromAuraBySpell(spell).ToList();
            double[] mods = spellEffects.Where(x => x.Modify == mod).Select(x => (double)x.Value).ToArray();
            foreach (var item in mods)
            {
                modifier *= 1 + (item / 100);
            }
            return modifier;
        }
        protected double GetModAdditivesFromAuras(Modify mod, Spell spell = null)
        {
            var spellEffects = GetAllEffectsFromAuraBySpell(spell).ToList();
            var modValue = spellEffects.Where(x => x.Modify == mod).Select(x => x.Value).Sum();
            return modValue;
        }
        protected void ProcOnCast(Spell spell, double fightTick)
        {
            if (metaGemEffect.ProcChance > 0)
            {
                if (metaGemEffect.LastProcTimeStamp + metaGemEffect.InternalCD < fightTick)
                {
                    var rnd = new Random();
                    bool procced = rnd.Next(100) < metaGemEffect.ProcChance;
                    if (procced)
                    {
                        switch (metaGemEffect.Modify)
                        {
                            case Modify.SpellHasteRating:
                                var aura = Wowhead.Auras.FirstOrDefault(x => x.SpellID == metaGemEffect.AuraID);
                                if (aura is not null) buffs.Add(fightTick, aura);
                                break;
                            case Modify.ManaReturnFlat:
                                AddMana(metaGemEffect.Value);
                                metaGemEffect.LastProcTimeStamp = fightTick;
                                break;
                            case Modify.Casttime:
                                break;
                            case Modify.CasttimePercent:
                                break;
                            case Modify.DamagePercent:
                                break;
                            case Modify.DamageFlat:
                                break;
                            case Modify.SpellPowerPercent:
                                break;
                            case Modify.PeriodicDamagePercent:
                                break;
                            case Modify.SpellHitChance:
                                break;
                            case Modify.SpellHitRating:
                                break;
                            case Modify.SpellCritRating:
                                break;
                            case Modify.CritRating:
                                break;
                            case Modify.SpellCritChance:
                                break;
                            case Modify.CritChance:
                                break;
                            case Modify.HitChance:
                                break;
                            case Modify.CritDamagePercent:
                                break;
                            case Modify.ManaPercent:
                                break;
                            case Modify.ProcOnHit:
                                break;
                            case Modify.PeriodicProc:
                                break;
                            case Modify.Cooldown:
                                break;
                            case Modify.LearnSpell:
                                break;
                            case Modify.Unique:
                                break;
                            case Modify.SpellEffectiveness:
                                break;
                            case Modify.MaxMana:
                                break;
                            case Modify.AuraDuration:
                                break;
                            case Modify.SpellHastePercent:
                                break;
                            case Modify.HolyPower:
                                break;
                            case Modify.HolyPercent:
                                break;
                            case Modify.ShadowPower:
                                break;
                            case Modify.ShadowPercent:
                                break;
                            case Modify.ArcanePower:
                                break;
                            case Modify.ArcanePercent:
                                break;
                            case Modify.FirePower:
                                break;
                            case Modify.FirePercent:
                                break;
                            case Modify.NaturePower:
                                break;
                            case Modify.NaturePercent:
                                break;
                            case Modify.FrostPower:
                                break;
                            case Modify.FrostPercent:
                                break;
                            case Modify.HealingPower:
                                break;
                            case Modify.SpellPower:
                                break;
                            case Modify.ManaReturnPercentOfDamage:
                                break;
                            case Modify.ManaReturnPercenttOfMaxMana:
                                break;
                            case Modify.ManaReturnPercentOfBaseMana:
                                break;
                            case Modify.IntellectPercent:
                                break;
                            case Modify.IntellectFlat:
                                break;
                            case Modify.MP5Flat:
                                break;
                            case Modify.MP5Percent:
                                break;
                            case Modify.SpiritFlat:
                                break;
                            case Modify.SpirtPercent:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            UpdateStats();
        }
        #endregion ModsFromTalents

        #region EquipGear
        public void EquipHead(Equipment equipment)
        {
            head = equipment.InvSlot == "Head" ? equipment : head;
            UpdateStats();
        }
        public void EquipNeck(Equipment equipment)
        {
            neck = equipment.InvSlot == "Neck" ? equipment : neck;
            UpdateStats();
        }
        public void EquipShoulders(Equipment equipment)
        {
            shoulders = equipment.InvSlot == "Shoulder" ? equipment : shoulders;
            UpdateStats();
        }
        public void EquipBack(Equipment equipment)
        {
            back = equipment.InvSlot == "Back" ? equipment : back;
            UpdateStats();
        }        public void EquipChest(Equipment equipment)
        {
            chest = equipment.InvSlot == "Chest" ? equipment : chest;
            UpdateStats();
        }
        public void EquipWrist(Equipment equipment)
        {
            wrist = equipment.InvSlot == "Wrist" ? equipment : wrist;
            UpdateStats();
        }
        public void EquipHands(Equipment equipment)
        {
            hands = equipment.InvSlot == "Hands" ? equipment : hands;
            UpdateStats();
        }
        public void EquipWaist(Equipment equipment)
        {
            waist = equipment.InvSlot == "Waist" ? equipment : waist;
            UpdateStats();
        }
        public void EquipLegs(Equipment equipment)
        {
            legs = equipment.InvSlot == "Legs" ? equipment : legs;
            UpdateStats();
        }
        public void EquipFeet(Equipment equipment)
        {
            feet = equipment.InvSlot == "Feet" ? equipment : feet;
            UpdateStats();
        }
        public void EquipRing1(Equipment equipment)
        {
            ring1 = equipment.InvSlot == "Finger" ? equipment : ring1;
            UpdateStats();
        }
        public void EquipRing2(Equipment equipment)
        {
            ring2 = equipment.InvSlot == "Finger" ? equipment : ring2;
            UpdateStats();
        }
        public void EquipTrinket1(Equipment equipment)
        {
            trinket1 = equipment.InvSlot == "Trinket" ? equipment : trinket1;
            UpdateStats();
        }
        public void EquipTrinket2(Equipment equipment)
        {
            trinket2 = equipment.InvSlot == "Trinket" ? equipment : trinket2;
            UpdateStats();
        }
        public void EquipMainhand(Equipment equipment)
        {
            if (equipment.InvSlot == "Main Hand")
                mainhand = equipment.InvSlot == "Main Hand" ? equipment : mainhand;
            else
            {
                EquipTwohander(equipment);
            }
            UpdateStats();
        }
        private void EquipTwohander(Equipment equipment)
        {
            mainhand = equipment.InvSlot == "Two-Hand" ? equipment : mainhand;
            if (mainhand is not null && mainhand.InvSlot == "Two-Hand") offhand = null;
            UpdateStats();
        }
        public void EquipOffhand(Equipment equipment)
        {
            if (mainhand is not null && mainhand.InvSlot == "Two-Hand") return;
            offhand = equipment.InvSlot == "Held In Off-hand" ? equipment : offhand;
            UpdateStats();
        }
        public void EquipRanged(Equipment equipment)
        {
            ranged = equipment.InvSlot == "Ranged" ? equipment : ranged;
            UpdateStats();
        }
        #endregion EquipGear
    }
}
