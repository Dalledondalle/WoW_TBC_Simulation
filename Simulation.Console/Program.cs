﻿using Simulation.Library;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Simulation.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Wowhead wh = new();
            var spell = wh.GetSpell(27222);
            //var spell = wh.GetSpell(686);
            System.Console.WriteLine(spell.Name);
        }

        private static void Run()
        {
            var wl = CreateWarlock();
            //PrintWarlock(wl);
            Simulate(wl, 1000, 150000);
            System.Console.WriteLine("-----------------------");
            //PrintWarlock(wl);
            Simulate(new Warlock(), 1000, 150000);
        }

        private static void PrintWarlock(Warlock wl)
        {
            System.Console.WriteLine($"Max mana: {wl.MaxMana}");
            System.Console.WriteLine($"Intellect: {wl.Intellect}");
            System.Console.WriteLine($"Spell Damage: {wl.SpellPower}");
            System.Console.WriteLine($"Spell Crit Rating: {wl.SpellCritRating}");
            System.Console.WriteLine($"Spell Crit: {wl.SpellCrit}");
            System.Console.WriteLine($"Spell Hit Rating: {wl.SpellHitRating}");
            System.Console.WriteLine($"Spell Hit: {wl.SpellHit}");
            System.Console.WriteLine($"Spell Haste Rating: {wl.SpellHasteRating}");
            System.Console.WriteLine($"Spell Haste: {wl.SpellHaste}");
        }

        private static Warlock CreateWarlock()
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipHead(wh.GetItem(31051));
            wl.EquipNeck(wh.GetItem(34204));
            wl.EquipShoulders(wh.GetItem(31054));
            wl.EquipBack(wh.GetItem(32331));
            wl.EquipChest(wh.GetItem(31052));
            wl.EquipWrist(wh.GetItem(32586));
            wl.EquipHands(wh.GetItem(31050));
            wl.EquipWaist(wh.GetItem(34541));
            wl.EquipLegs(wh.GetItem(31053));
            wl.EquipFeet(wh.GetItem(34564));
            wl.EquipRing1(wh.GetItem(34362));
            wl.EquipRing2(wh.GetItem(29305));
            wl.EquipTrinket1(wh.GetItem(34429));
            wl.EquipTrinket2(wh.GetItem(35326));
            wl.EquipMainhand(wh.GetItem(34337));
            wl.EquipRanged(wh.GetItem(34347));
            return wl;
        }

        private static void PrintWowheadItem(int id)
        {
            Wowhead wh = new();
            var s = wh.GetItem(id);
            System.Console.WriteLine(s);
        }

        private static void Simulate(Warlock wl, int itterations, double fightLength)
        {
            Stopwatch sw = new();
            sw.Start();
            Report[] array = new Report[itterations];
            Wowhead wh = new();
            var shadowbolt = wh.GetSpell(27209);
            var lifetap = wh.GetSpell(27222);
            for (int i = 0; i < itterations; i++)
            {
                Report report = new Report()
                {
                    FightLength = fightLength,
                    FightNo = i,
                };
                double currentFight = 0;
                while (fightLength > currentFight)
                {
                    if (wl.CanCastShadowBolt(shadowbolt))
                    {
                        wl.CastShadowBolt(shadowbolt, report);
                    }
                    else
                    {
                        wl.CastLifeTap(lifetap, report);
                    }
                    currentFight += wl.WaitForNextCast();
                }
                array[i] = report;
            }

            double worstFight = array.Min(r => r.DPS);
            double bestFight = array.Max(r => r.DPS);
            double avg = array.Average(r => r.DPS);
            sw.Stop();
            System.Console.WriteLine($"Worst fight: {worstFight.ToString("0.###")}\nBest fight: {bestFight.ToString("0.###")}\nOn average: {avg.ToString("0.###")}\nTime elapsed: {sw.Elapsed}\nCrit %: {wl.SpellCrit.ToString("0.##")}%");
        }
    }
}