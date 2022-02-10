using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation.Library
{
    public class Report
    {
        public double TotalDamageDone => Spells.Select(sr => sr.Damage).Sum();
        public double DPS => TotalDamageDone / (FightLength / 1000);
        public List<SpellReport> Spells { get; set; }
        public int FightNo { get; set; }
        public double FightLength { get; set; }
        public List<RessourceRegenrated> RessourcesRegenerated { get; set; }
        public Report()
        {
            Spells = new();
            RessourcesRegenerated = new();
        }

        public void ReportDamage(double dmg, Spell spell, bool hit, bool isCrit = false, bool tick = false)
        {
            SpellReport spellReport = new()
            {
                SpellId = spell.ID,
                Damage = dmg,
                Hit = hit,
                Crit = isCrit,
                Tick = tick
            };
            Spells.Add(spellReport);
        }

        public void ReportManaGained(int amount, string source)
        {
            RessourceRegenrated regenrated = new()
            {
                Amount = amount,
                Source = source
            };
            RessourcesRegenerated.Add(regenrated);
        }
    }

    public class RessourceRegenrated
    {
        public string Source { get; set; }
        public int Amount { get; set; }
    }

    public class SpellReport
    {
        public string SpellId { get; set; }
        public bool Hit { get; set; }
        public bool Crit { get; set; }
        public bool Tick { get; set; }
        public double Damage { get; set; }
    }
}
