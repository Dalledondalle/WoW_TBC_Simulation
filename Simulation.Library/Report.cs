using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Library
{
    public class Report
    {
        public double TotalDamageDone => Spells.Select(sr => sr.Damage).ToList().Sum();
        public List<SpellReport> AllSpellsCasted 
        {
            get
            {
                List<SpellReport> allSpellsCasted = new();
                foreach (var item in Spells)
                {
                    allSpellsCasted.Add(item);
                }
                foreach (var item in RessourcesRegenerated)
                {
                    allSpellsCasted.Add(item);
                }
                return allSpellsCasted;
            }
        }
        public double DPS => TotalDamageDone / (FightLength / 1000);
        public List<DamageSpellReport> Spells { get; set; }
        public int FightNo { get; set; }
        public double FightLength { get; set; }
        public List<RessourceRegenratedReport> RessourcesRegenerated { get; set; }
        public Report()
        {
            Spells = new();
            RessourcesRegenerated = new();
        }

        public void ReportDamage(double dmg, Spell spell, double figthTick, bool hit, bool isCrit = false, bool tick = false)
        {
            DamageSpellReport spellReport = new()
            {
                SpellId = spell.ID,
                Damage = dmg,
                Hit = hit,
                Crit = isCrit,
                Tick = tick,
                FightTick = figthTick
            };
            Spells.Add(spellReport);
        }

        public void ReportManaGained(int amount, Spell spell, double fightTick)
        {
            RessourceRegenratedReport regenrated = new()
            {
                SpellId = spell.ID,
                Amount = amount,
                Source = spell.Name,
                FightTick = fightTick
            };
            RessourcesRegenerated.Add(regenrated);
        }
    }

    public class SpellReport
    {
        public string SpellId { get; set; }
        public double FightTick { get; set; }
    }

    public class RessourceRegenratedReport : SpellReport
    {
        public string Source { get; set; }
        public int Amount { get; set; }
    }

    public class DamageSpellReport : SpellReport
    {        
        public bool Hit { get; set; }
        public bool Crit { get; set; }
        public bool Tick { get; set; }
        public double Damage { get; set; }
        
    }
}
