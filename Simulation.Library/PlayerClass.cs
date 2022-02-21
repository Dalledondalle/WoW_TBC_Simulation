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
        public new int MP5 => GetAllGear().Where(e => e is not null).Select(e => e.ManaRegn).Sum() + mp5 + auras.Select(e => e.FlatManaRegenMod).Sum() + GetSumOfSocketBonuses(BonusStat.MP5);
        public new int SpellHitRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellHitRating).Sum() + spellHitRating + auras.Select(e => e.SpellHitRatingMod).Sum() + GetSumOfSocketBonuses(BonusStat.spellhit);
        public new double SpellHit => (SpellHitRating / 12.6) + spellHit + auras.Select(e => e.SpellHitModifer).Sum();
        public new int SpellCritRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellCritRating).Sum() + spellCritRating + auras.Select(e => e.SpellCritRatingMod).Sum() + GetSumOfSocketBonuses(BonusStat.spellcrit);
        public new int Intellect => (int)((intellect +
                                GetSumOfSocketBonuses(BonusStat.intellect) +
                                GetAllGear().Where(e => e is not null).Select(e => e.Intellect).Sum() +
                                auras.Select(e => e.FlatIntMod).Sum()) *
                                (1 + (auras.Select(e => e.IntModifer).Sum() / 100)));
        public new double SpellCrit => spellCrit + (Intellect / 81.9) + (SpellCritRating / 22.1) + auras.Select(e => e.SpellCritModifer).Sum() + GetSumOfSocketBonuses(BonusStat.spellcrit);
        public new int SpellPower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.SpellPower).Sum() +
                                    spellPower +
                                    GetSumOfSocketBonuses(BonusStat.spellpower) +
                                    auras.Select(e => e.FlatSpellMod).Sum()) *
                                    (1 + (auras.Select(e => e.SpellModifer).Sum() / 100)));
        public new int ShadowPower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.ShadowSpellPower).Sum() +
                                    shadowPower +
                                    auras.Select(e => e.FlatShadowMod).Sum()) *
                                    (1 + (auras.Select(e => e.ShadowModifer).Sum() / 100)));
        public new int FirePower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.FireSpellPower).Sum() +
                                    firePower +
                                    auras.Select(e => e.FlatFireMod).Sum()) *
                                    (1 + (auras.Select(e => e.FireModifer).Sum() / 100)));
        public new int ArcanePower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.ArcaneSpellPower).Sum() +
                                    arcanePower +
                                    auras.Select(e => e.FlatArcaneMod).Sum()) *
                                    (1 + (auras.Select(e => e.ArcaneModifer).Sum() / 100)));
        public new int FrostPower => (int)((GetAllGear().Where(e => e is not null).Select(e => e.FrostSpellPower).Sum() +
                                    frostPower +
                                    auras.Select(e => e.FlatFrostMod).Sum()) *
                                    (1 + (auras.Select(e => e.FrostModifer).Sum() / 100)));
        public new int SpellHasteRating => GetAllGear().Where(e => e is not null).Select(e => e.SpellHasteRating).Sum() + spellHasteRating + auras.Select(e => e.SpellHasteRatingMod).Sum() + GetSumOfSocketBonuses(BonusStat.spellhaste);
        //Hasted Cast Time = Base Cast Time / (1 + ( Spell Haste Rating / 1577 ) )
        public new double SpellHaste => (SpellHasteRating / 15.77) + spellHaste + auras.Select(e => e.SpellHasteModifer).Sum();
        public new int Mana => mana;
        public new int MaxMana => (Math.Min(20, Intellect) + 15 * (Intellect - Math.Min(20, Intellect))) + baseMana;
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
        protected int GetSumOfSocketBonuses(BonusStat stat)
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
            var mods = spellEffects.Where(x => x.Modify == mod).Select(x => x.Value);
            foreach (var item in mods)
            {
                modifier *= 1 + (item / 100);
            }
            return modifier;
        }
        protected List<Effect> GetAllEffectsBySpell(Spell spell)
        {
            var talentsToModSpell = Talents.Where(x => x.Effects.Any(e => e.AffectedSpells.Any(y => y == spell.Name)));
            var effectsToSpell = talentsToModSpell.Select(x => x.Effects.Select(y => y)).ToList();
            List<Effect> effects = new();
            effects.AddRange(effectsToSpell.SelectMany(list => list.Where(effect => effect.AffectedSpells.Any(x => x == spell.Name))));
            return effects;
        }
        #endregion ModsFromTalents

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
