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
        static List<Report> reports = new();
        static Dictionary<int, TimeSpan> tests = new();
        static Thread[] threads = new Thread[100];
        static void Main(string[] args)
        {
            SetupThreads();
            System.Console.Read();
            System.Console.Clear();
            Run(10000, 50);
            //for (int i = 0; i < 100; i += 5)
            //{
            //    Run(10000, i);
            //}
            //System.Console.Clear();
            //tests = tests.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            //foreach (var item in tests)
            //{
            //    System.Console.WriteLine($"Threads: {item.Key} -> Time: {item.Value}");
            //}
        }

        private static void SetupThreads()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(SimulateWithThread);
            }
            System.Console.WriteLine("Threads sat up");
        }

        private static void Run(int itterations, int threadCount)
        {
            reports.Clear();
            //var wl = CreateWarlock();
            //PrintWarlock(wl);
            //Simulate(wl, 30000, 150000);
            //System.Console.WriteLine("-----------------------");
            //PrintWarlock(wl);
            //Simulate(new Warlock(), 30000, 150000);
            //Thread thread = new Thread(SimulateWithThread);
            //thread.Start(new ThreadPass{ Character = new Warlock(), FightLength = 150000, Itterations = 3000 });
            Stopwatch sw = new();
            sw.Start();

            Parallel.ForEach(threads, new ParallelOptions { MaxDegreeOfParallelism = -1 }, t =>
            {
                t.Start(new ThreadPass { Character = new Warlock(), FightLength = 150000, Itterations = itterations / threads.Length, ID = threads.ToList().IndexOf(t) });
            });

            //for (int i = 0; i < threadCount; i++)
            //{
            //    threads.Add(new Thread(SimulateWithThread));
            //}

            //foreach (var item in threads)
            //{
            //    item.Start(new ThreadPass { Character = new Warlock(), FightLength = 150000, Itterations = itterations / threadCount });
            //}
            //System.Console.WriteLine($"Threads {threadCount}");
            //while (reports.Count < itterations)
            //{
            //    if (reports.Count % 20 == 0)
            //    {
            //        double progress = (double)reports.Count / (double)itterations * 100;
            //        System.Console.Write($"\r{Math.Round(progress, 2)}%");
            //    }
            //}

            //while(threads.Any(x => x.IsAlive))
            //{
            //    if (reports.Count % 20 == 0)
            //    {
            //        double worstFight = reports.Min(r => r.DPS);
            //        double bestFight = reports.Max(r => r.DPS);
            //        double avg = reports.Average(r => r.DPS);
            //        System.Console.Write($"\rWorst fight: {worstFight.ToString("0.###")}\nBest fight: {bestFight.ToString("0.###")}\nOn average: {avg.ToString("0.###")}\n");
            //    }
            //}
            sw.Stop();
            System.Console.WriteLine($"Time elapsed: {sw.Elapsed}");
            tests.Add(threadCount, sw.Elapsed);
        }

        private class ThreadPass
        {
            public Unit Character { get; set; }
            public double FightLength { get; set; }
            public int Itterations { get; set; }
            public int ID { get; set; }
        }

        private static void SimulateWithThread(object obj)
        {
            ThreadPass tp = obj as ThreadPass;
            Simulate((Warlock)tp.Character, tp.Itterations, tp.FightLength, tp.ID);
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
            var metaGem = wh.GetGem(34220);
            var redGem = wh.GetGem(32196);
            var blueGem = wh.GetGem(32215);
            var yellowGem = wh.GetGem(35761);

            wl.EquipHead(wh.GetEquipment(31051));
            wl.Head.SocketGem(metaGem, 1);
            wl.Head.SocketGem(yellowGem, 2);

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

            wl.EquipRing1(wh.GetEquipment(34362));
            wl.EquipRing2(wh.GetEquipment(29305));
            wl.EquipTrinket1(wh.GetEquipment(34429));
            wl.EquipTrinket2(wh.GetEquipment(35326));
            wl.EquipMainhand(wh.GetEquipment(34337));
            wl.Mainhand.SocketGem(redGem, 1);
            wl.Mainhand.SocketGem(blueGem, 2);
            wl.Mainhand.SocketGem(blueGem, 3);

            wl.EquipRanged(wh.GetEquipment(34347));
            wl.Ranged.SocketGem(yellowGem, 1);
            return wl;
        }

        private static void PrintWowheadItem(int id)
        {
            Wowhead wh = new();
            var s = wh.GetEquipment(id);
            System.Console.WriteLine(s);
        }

        private static void Simulate(Warlock wl, int itterations, double fightLength, int id)
        {
            System.Console.WriteLine($"Start id: {id}");
            //Stopwatch sw = new();
            Dummy target = new();
            //sw.Start();
            Report[] array = new Report[itterations];
            Wowhead wh = new();
            var shadowbolt = wh.GetSpell(27209);
            var lifetap = wh.GetSpell(27222);
            var felarmor = wh.GetSpell(28189);
            //Pre fight setup
            wl.CastSpell(felarmor, target, 0, new Report());
            var clone = (Warlock)wl.Clone();

            for (int i = 0; i < itterations+1; i++)
            {
                Report report = new Report()
                {
                    FightLength = fightLength,
                    FightNo = i,
                };
                double currentFight = 0;
                while (fightLength > currentFight)
                {
                    if (clone.HaveManaForSpell(shadowbolt))
                    {
                        clone.CastSpell(shadowbolt, target, currentFight, report);
                    }
                    else
                    {
                        clone.CastLifeTap(lifetap, report);
                    }
                    currentFight += clone.WaitForNextCast();
                }
                //array[i] = report;
                reports.Add(report);
            }
            System.Console.WriteLine($"Stop id: {id}");
            //double worstFight = array.Min(r => r.DPS);
            //double bestFight = array.Max(r => r.DPS);
            //double avg = array.Average(r => r.DPS);
            //sw.Stop();
            //System.Console.WriteLine($"Worst fight: {worstFight.ToString("0.###")}\nBest fight: {bestFight.ToString("0.###")}\nOn average: {avg.ToString("0.###")}\nTime elapsed: {sw.Elapsed}\nCrit %: {wl.SpellCrit.ToString("0.##")}%");
            //System.Console.WriteLine($"Done {reports.Count}");
        }
    }
}
