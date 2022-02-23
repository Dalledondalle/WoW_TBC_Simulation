using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Library
{
    public abstract class Unit : ICloneable
    {
        #region Stats
        public int MP5 { get => mp5; protected set => mp5 = value; }
        protected int mp5 = 0;
        protected int basemp5 = 0;
        public int SpellHitRating { get => spellHitRating; protected set => spellHitRating = value; }
        protected int spellHitRating = 0;
        protected int baseSpellHitRating = 0;
        public double SpellHit { get => spellHit; protected set => spellHit = value; }
        protected double spellHit = 0;
        protected double baseSpellHit = 0;
        public int SpellCritRating { get => spellCritRating; protected set => spellCritRating = value; }
        protected int spellCritRating = 0;
        protected int baseSpellCritRating = 0;
        public int Intellect
        {
            get => intellect; protected set => intellect = value;
        }
        protected int intellect = 0;
        protected int baseIntellect = 0;
        public double SpellCrit
        {
            get => spellCrit; protected set => spellCrit = value;
        }
        protected double spellCrit = 0;
        protected double baseSpellCrit = 0;
        public int SpellPower
        {
            get => spellPower; protected set => spellPower = value;
        }
        protected int spellPower = 0;
        protected int baseSpellPower = 0;
        public int ShadowPower
        {
            get => shadowPower; protected set => shadowPower = value;
        }
        protected int shadowPower = 0;
        protected int baseShadowPower = 0;
        public int FirePower
        {
            get => firePower; protected set => firePower = value;
        }
        protected int firePower = 0;
        protected int baseFirePower = 0;
        public int ArcanePower
        {
            get => arcanePower; protected set => arcanePower = value;
        }
        protected int arcanePower = 0;
        protected int baseArcanePower = 0;
        public int FrostPower
        {
            get => frostPower; protected set => frostPower = value;
        }
        protected int frostPower = 0;
        protected int baseFrostPower = 0;
        public int SpellHasteRating
        {
            get => spellHasteRating; protected set => spellHasteRating = value;
        }
        protected int spellHasteRating = 0;
        protected int baseSpellHasteRating = 0;
        //Hasted Cast Time = Base Cast Time / (1 + ( Spell Haste Rating / 1577 ) )
        public double SpellHaste
        {
            get => spellHaste; protected set => spellHaste = value;
        }
        protected double spellHaste = 0;
        protected double baseSpellHaste = 0;
        public int Mana
        {
            get => mana; protected set => mana = value;
        }
        protected int mana = 0;
        protected int baseMana = 0;
        public int MaxMana => (Math.Min(20, Intellect) + 15 * (Intellect - Math.Min(20, Intellect))) + baseMana;
        #endregion Stats
        #region SpellsAndAuras
        public Spell lastSpelledCasted { get; protected set; }
        protected Dictionary<double, Aura> auras => Buffs.Concat(Debuffs).ToDictionary(x => x.Key, x => x.Value);
        public Dictionary<double, Aura> Buffs => buffs.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        protected Dictionary<double, Aura> buffs = new();
        public Dictionary<double, Aura> Debuffs => debuffs;
        protected Dictionary<double, Aura> debuffs = new();
        public void AddAura(Aura Aura, double fightTick)
        {
            if (Aura is null) return;
            if (Aura.AuraType == AuraType.Buff) buffs.Add(fightTick, Aura);
            if (Aura.AuraType == AuraType.Debuff) debuffs.Add(fightTick, Aura);
            UpdateStats();
        }
        #endregion SpellsAndAuras

        public virtual void CastSpell(Spell spell, Unit target, double fightTick, Report report)
        {
            return;
        }

        public virtual void UpdateStats()
        {
            return;
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
            this.baseIntellect += intellect;
            if (this.intellect < 0) this.intellect = 0;
            UpdateStats();
        }
        public void AddHasteRating(int hasteRating)
        {
            this.baseSpellHasteRating += hasteRating;
            UpdateStats();
        }
        public void AddHaste(double haste)
        {
            this.baseSpellHaste += haste;
            UpdateStats();
        }
        public virtual void AddCritRating(int critRating)
        {
            baseSpellCritRating += critRating;
            UpdateStats();
        }
        public void AddShadowSpellPower(int shadowPower)
        {
            this.baseShadowPower += shadowPower;
            UpdateStats();
        }
        public void AddFireSpellPower(int firePower)
        {
            this.baseFirePower += firePower;
            UpdateStats();
        }
        public void AddSpellPower(int spellPower)
        {
            this.baseSpellPower += spellPower;
            UpdateStats();
        }
        public void AddArcanePower(int arcanePower)
        {
            this.baseArcanePower += arcanePower;
            UpdateStats();
        }
        public void AddFrostPower(int frostPower)
        {
            this.baseFrostPower = frostPower;
            UpdateStats();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion AddStats
    }
}
