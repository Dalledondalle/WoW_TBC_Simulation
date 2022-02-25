using Simulation.Library;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Simulation.Console
{
    class Program
    {
        static Wowhead wh = new();
        static Warlock wl = new();
        static Report[] reports;
        static void Main(string[] args)
        {
            System.Console.WriteLine("Press any key to start");
            System.Console.ReadKey();
            Run(30);
        }

        private static void Run(int threads)
        {
            Stopwatch sw = new();
            reports = new Report[threads * 1000];
            //var wl = CreateWarlock();
            //PrintWarlock(wl);
            //Simulate(wl, 10000, 150000);
            //System.Console.WriteLine("-----------------------");
            //PrintWarlock(wl);
            sw.Start();
            //Parallel.For(0, 100, i =>
            //{
            var counter = Task.Run(() =>
            {
                System.Console.Clear();
                while (reports.Any(x => x is null))
                {
                    double progress = Math.Round((double)reports.Count(x => x is not null) / ((double)threads * 1000) * 100, 2);
                    System.Console.Write($"\r{progress}%");
                }
                System.Console.Clear();
                System.Console.WriteLine("100%");
            });

            var tasks = MakeTasks(threads);
            counter.Wait();
            System.Console.WriteLine();
            //});
            sw.Stop();
            double worstFight = reports.Min(r => r.DPS);
            double bestFight = reports.Max(r => r.DPS);
            double avg = reports.Average(r => r.DPS);
            System.Console.WriteLine($"Worst fight: {worstFight.ToString("0.###")}\nBest fight: {bestFight.ToString("0.###")}\nOn average: {avg.ToString("0.###")}\nTime elapsed: {sw.Elapsed}\nOver {reports.Length} sims");
            if (reports.Any(x => x is null))
            {
                System.Console.WriteLine($"There are {reports.Count(x => x is null)} nulls");
            }
            System.Console.WriteLine($"SpellPower: {wl.SpellPower}\nSpellCritRating: {wl.SpellCritRating}\nSpellCritChance: {Math.Round(wl.SpellCrit, 2)}%\nSpellHasteRating: {wl.SpellHasteRating}\nSpellHaste: {Math.Round(wl.SpellHaste,2)}%\nSpellHitRating: {wl.SpellHitRating}\nSpellHitChance: {Math.Round(wl.SpellHit,2)}%");
            //Random rnd = new();
            //Report reportToAnalyze = reports[rnd.Next(reports.Count)];

            //PrintAReport(reportToAnalyze);
        }

        static void PrintAReport(Report reportToAnalyze)
        {
            foreach (var item in reportToAnalyze.AllSpellsCasted.OrderBy(x => x.FightTick))
            {
                if (item.GetType() == typeof(DamageSpellReport))
                {
                    DamageSpellReport sp = item as DamageSpellReport;
                    System.Console.WriteLine($"{wh.GetSpell(int.Parse(sp.SpellId)).Name} did {sp.Damage} at {sp.FightTick}. Crit: {sp.Crit}");
                }
                if (item.GetType() == typeof(RessourceRegenratedReport))
                {
                    RessourceRegenratedReport rp = item as RessourceRegenratedReport;
                    System.Console.WriteLine($"{wh.GetSpell(int.Parse(rp.SpellId)).Name} restored {rp.Amount} Mana at  {rp.FightTick}");
                }
            }
        }

        static Task[] MakeTasks(int amount)
        {
            wl = CreateWarlock();
            Task[] t = new Task[amount];
            Parallel.For(0, amount, i =>
            {
                t[i] = Simulate(wl, 150000, i);
            });
            return t;
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
            wl = new();
            var metaGem = wh.GetGem(34220);
            var redGem = wh.GetGem(35488);
            var blueGem = wh.GetGem(32215);
            var yellowGem = wh.GetGem(35761);

            wl.EquipHead(wh.GetEquipment(31051));
            wl.Head.SocketGem(metaGem, 1);
            wl.Head.SocketGem(redGem, 2);

            wl.EquipNeck(wh.GetEquipment(34204));

            wl.EquipShoulders(wh.GetEquipment(31054));
            wl.Shoulders.SocketGem(blueGem, 1);
            wl.Shoulders.SocketGem(yellowGem, 2);

            wl.EquipBack(wh.GetEquipment(32331));

            wl.EquipChest(wh.GetEquipment(31052));
            wl.Chest.SocketGem(yellowGem, 1);
            wl.Chest.SocketGem(yellowGem, 2);
            wl.Chest.SocketGem(blueGem, 3);

            wl.EquipWrist(wh.GetEquipment(32586));

            wl.EquipHands(wh.GetEquipment(31050));
            wl.Hands.SocketGem(yellowGem, 1);

            wl.EquipWaist(wh.GetEquipment(34541));
            wl.Waist.SocketGem(yellowGem, 1);

            wl.EquipLegs(wh.GetEquipment(31053));
            wl.Legs.SocketGem(yellowGem, 1);

            wl.EquipFeet(wh.GetEquipment(34564));
            wl.Feet.SocketGem(yellowGem, 1);

            wl.EquipRing1(wh.GetEquipment(32527));
            wl.EquipRing2(wh.GetEquipment(32527));
            wl.EquipTrinket1(wh.GetEquipment(30449));
            //wl.EquipTrinket2(wh.GetEquipment(30449));
            wl.EquipMainhand(wh.GetEquipment(34182));
            wl.Mainhand.SocketGem(yellowGem, 1);
            wl.Mainhand.SocketGem(yellowGem, 2);
            wl.Mainhand.SocketGem(yellowGem, 3);

            wl.EquipRanged(wh.GetEquipment(34347));
            wl.Ranged.SocketGem(yellowGem, 1);
            return wl;
        }

        private static void PrintWowheadItem(int id)
        {
            var s = wh.GetEquipment(id);
            System.Console.WriteLine(s);
        }

        private static Task Simulate(PlayerClass playerClass, double fightLength, int id)
        {
            //Stopwatch sw = new();
            Dummy target = new();
            //sw.Start();
            int itterations = 1000;
            var shadowbolt = wh.GetSpell(27209);
            var lifetap = wh.GetSpell(27222);
            var felarmor = wh.GetSpell(28189);
            return Task.Run(() =>
            {
                for (int i = 0; i < itterations; i++)
                {
                    int thisId = (id * itterations) + i;
                    var clone = (Warlock)playerClass.Clone();
                    Report report = new Report()
                    {
                        FightLength = fightLength,
                        FightNo = thisId,
                    };
                    clone.CastSpell(felarmor, target, 0, report);
                    double currentFight = 0;
                    while (fightLength >= currentFight)
                    {
                        if (!clone.HaveManaForSpell(shadowbolt))
                        {
                            currentFight += clone.CastSpell(lifetap, target, currentFight, report);
                        }
                        else
                        {
                            currentFight += clone.CastSpell(shadowbolt, target, currentFight, report);
                        }
                    }
                    reports[thisId] = report;
                }
            });
        }
    }
}
