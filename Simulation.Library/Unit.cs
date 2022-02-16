using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Library
{
    public abstract class Unit
    {
        #region Stats
        public int MP5 => mp5;
        protected int mp5 = 0;
        public int SpellHitRating => spellHitRating;
        protected int spellHitRating = 0;
        public double SpellHit => spellHit;
        protected double spellHit = 0;
        public int SpellCritRating => spellCritRating;
        protected int spellCritRating = 0;
        public int Intellect => intellect;
        protected int intellect = 0;
        public double SpellCrit => spellCrit;
        protected double spellCrit = 0;
        public int SpellPower => spellPower;
        protected int spellPower = 0;
        public int ShadowPower => shadowPower;
        protected int shadowPower = 0;
        public int FirePower => firePower;
        protected int firePower = 0;
        public int ArcanePower => arcanePower;
        protected int arcanePower = 0;
        public int FrostPower => frostPower;
        protected int frostPower;
        public int SpellHasteRating => spellHasteRating;
        protected int spellHasteRating = 0;
        //Hasted Cast Time = Base Cast Time / (1 + ( Spell Haste Rating / 1577 ) )
        public double SpellHaste => spellHaste;
        protected double spellHaste = 0;
        public int Mana => mana;
        protected int mana = 0;
        protected int baseMana = 0;
        public int MaxMana => (Math.Min(20, Intellect) + 15 * (Intellect - Math.Min(20, Intellect))) + baseMana;
        #endregion Stats
        #region SpellsAndAuras
        public Spell lastSpelledCasted { get; protected set; }
        protected List<Aura> auras => Buffs.Concat(Debuffs).ToList();
        public List<Aura> Buffs => buffs.OrderBy(x => x.EndTimer).ToList();
        protected List<Aura> buffs = new();
        public List<Aura> Debuffs => debuffs;
        protected List<Aura> debuffs = new();
        public void AddAura(Aura Aura)
        {
            if (Aura is null) return;
            if (Aura.AuraType == AuraType.Buff) buffs.Add(Aura);
            if (Aura.AuraType == AuraType.Debuff) debuffs.Add(Aura);
        }
        #endregion SpellsAndAuras

        public virtual void CastSpell(Spell spell, Unit target, double fightTick, Report report)
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
    }
}
