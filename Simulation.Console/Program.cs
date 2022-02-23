using Simulation.Library;
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
            Run();
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
            wl.Shoulders.SocketGem(yellowGem,2 );

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

        private static void Simulate(Warlock wl, int itterations, double fightLength)
        {
            Stopwatch sw = new();
            Dummy target = new();
            sw.Start();
            Report[] array = new Report[itterations];
            Wowhead wh = new();
            var shadowbolt = wh.GetSpell(27209);
            var lifetap = wh.GetSpell(27222);
            var felarmor = wh.GetSpell(28189);
            //Pre fight setup
            wl.CastSpell(felarmor, target, 0, new Report());

            var clone = (Warlock)wl.Clone();

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
