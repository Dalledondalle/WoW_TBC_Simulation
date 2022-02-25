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
        static Dictionary<int, TimeSpan> sims = new();
        static void Main(string[] args)
        {
            for (int i = 1; i < 10001; i++)
            {
                System.Console.WriteLine($"Running {i} tasks now");
                Run(i);
                System.Console.Clear();
                TimeSpan time = sims.Min(x => x.Value);
                int threadCount = sims.First(x => x.Value == time).Key;
                System.Console.WriteLine($"Fast setup currently is {threadCount} threads which took {time}");
            }

            TimeSpan bestTime = sims.Min(x => x.Value);
            int bestThread = sims.First(x => x.Value == bestTime).Key;
            System.Console.WriteLine($"Fast setup was is {bestThread} threads which took {bestTime}");
            //Simulate(new Warlock(), 300, 150000);
            //PrintAReport(reports.First());
        }

        private static void Run(int threads)
        {
            Stopwatch sw = new();
            reports.Clear();
            //var wl = CreateWarlock();
            //PrintWarlock(wl);
            //Simulate(wl, 10000, 150000);
            //System.Console.WriteLine("-----------------------");
            //PrintWarlock(wl);
            sw.Start();
            //Parallel.For(0, 100, i =>
            //{

            var tasks = MakeTasks(threads).ToList();
            while (tasks.Any(t => !t.IsCompleted))
            {
                //System.Console.Write($"\r{reports.Count}");
            }
            System.Console.WriteLine();
            //});
            sw.Stop();
            //double worstFight = reports.Min(r => r.DPS);
            //double bestFight = reports.Max(r => r.DPS);
            //double avg = reports.Average(r => r.DPS);
            //System.Console.WriteLine($"Worst fight: {worstFight.ToString("0.###")}\nBest fight: {bestFight.ToString("0.###")}\nOn average: {avg.ToString("0.###")}\nTime elapsed: {sw.Elapsed}");
            //Random rnd = new();
            //Report reportToAnalyze = reports[rnd.Next(reports.Count)];

            //PrintAReport(reportToAnalyze);

            sims.Add(threads, sw.Elapsed);

        }

        static void PrintAReport(Report reportToAnalyze)
        {
            Wowhead wh = new();
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

        static IEnumerable<Task> MakeTasks(int amount)
        {
            Warlock wl = CreateWarlock();
            for (int i = 0; i < amount; i++)
            {
                yield return Task.Run(() => { Simulate(wl, 100000 / amount, 150000); });
            }
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


            for (int i = 0; i < itterations+1; i++)
            {
                var clone = (Warlock)wl.Clone();
                Report report = new Report()
                {
                    FightLength = fightLength,
                    FightNo = i,
                };
                double currentFight = 0;
                while (fightLength >= currentFight)
                {
                    if (!clone.HaveManaForSpell(shadowbolt))
                    {
                        clone.CastLifeTap(lifetap, report, currentFight);
                    }
                    else
                    {
                        clone.CastSpell(shadowbolt, target, currentFight, report);
                    }
                    currentFight += clone.WaitForNextCast();
                }
                reports.Add(report);
            }

            //double worstFight = array.Min(r => r.DPS);
            //double bestFight = array.Max(r => r.DPS);
            //double avg = array.Average(r => r.DPS);
            //sw.Stop();
            //System.Console.WriteLine($"Worst fight: {worstFight.ToString("0.###")}\nBest fight: {bestFight.ToString("0.###")}\nOn average: {avg.ToString("0.###")}\nTime elapsed: {sw.Elapsed}\nCrit %: {wl.SpellCrit.ToString("0.##")}%");
        }
    }
}
