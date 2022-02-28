using Simulation.Library;
using System.Linq;
using Xunit;

namespace TalentTests.Demonology
{
    #region 5pointsTalents
    public class UnholyPowerTests
    {
        private static string rank = "Rank";
        private static string propertyName = "UnholyPower" + rank;
        private static string talentName = "Unholy Power";
        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class MasterDemonologistTests
    {
        private static string rank = "Rank";
        private static string propertyName = "MasterDemonologist" + rank;
        private static string talentName = "Master Demonologist";
        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class DemonicTacticsTests
    {
        private static string rank = "Rank";
        private static string propertyName = "DemonicTactics" + rank;
        private static string talentName = "Demonic Tactics";
        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    #endregion 5pointsTalents

    #region 3pointsTalents
    public class ImprovedImpTests
    {
        private static string rank = "Rank";
        private static string propertyName = "ImprovedImp" + rank;
        private static string talentName = "Improved Imp";
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(3, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class FelIntellectTests
    {
        private static string rank = "Rank";
        private static string propertyName = "FelIntellect" + rank;
        private static string talentName = "Fel Intellect";
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(3, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class DemonicAegisTests
    {
        private static string rank = "Rank";
        private static string propertyName = "DemonicAegis" + rank;
        private static string talentName = "Demonic Aegis";
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(3, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(3, 130)]
        [InlineData(2, 120)]
        [InlineData(1, 110)]
        [InlineData(0, 100)]
        public void FelArmorRank2AffectedTests(int rank, int expectedSP)
        {
            Wowhead wh = new();
            Warlock wl = new();
            var spell = wh.GetSpell(28189);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);
            wl.CastSpell(spell, new Dummy(), 0, new Report());

            Assert.Equal(expectedSP, wl.SpellPower);
        }

        [Theory]
        [InlineData(3, 65)]
        [InlineData(2, 60)]
        [InlineData(1, 55)]
        [InlineData(0, 50)]
        public void FelArmorRank1AffectedTests(int rank, int expectedSP)
        {
            Wowhead wh = new();
            Warlock wl = new();
            var spell = wh.GetSpell(28176);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);
            wl.CastSpell(spell, new Dummy(),0, new Report());

            Assert.Equal(expectedSP, wl.SpellPower);
        }
    }
    public class DemonicKnowledgeTests
    {
        private static string rank = "Rank";
        private static string propertyName = "DemonicKnowledge" + rank;
        private static string talentName = "Demonic Knowledge";
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(3, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    #endregion 3pointsTalents

    #region 2pointsTalents
    public class MasterConjurorTests
    {
        private static string rank = "Rank";
        private static string propertyName = "MasterConjuror" + rank;
        private static string talentName = "Master Conjuror";
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank2Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(2, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentsTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    #endregion 2pointsTalents

    #region 1pointTalents
    public class DemonicSacrificeTests
    {
        private static string rank = "Rank";
        private static string propertyName = "DemonicSacrifice" + rank;
        private static string talentName = "Demonic Sacrifice";
        [Fact]
        public void AddTests()
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank2Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Fact]
        public void RemoveTests()
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, 0);

            Assert.Empty(wl.Talents);
        }
    }
    public class SoulLinkTests
    {
        private static string rank = "Rank";
        private static string propertyName = "SoulLink" + rank;
        private static string talentName = "Soul Link";
        [Fact]
        public void AddTests()
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank2Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Fact]
        public void RemoveTests()
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, 0);

            Assert.Empty(wl.Talents);
        }
    }
    public class FelguardTests
    {
        private static string rank = "Rank";
        private static string propertyName = "Felguard" + rank;
        private static string talentName = "Felguard";
        [Fact]
        public void AddTests()
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank2Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal(talentName, wl.Talents.First().Name);
        }

        [Fact]
        public void RemoveTests()
        {
            Warlock wl = new();
            wl.GetType().GetProperty(propertyName).SetValue(wl, 1);
            wl.GetType().GetProperty(propertyName).SetValue(wl, 0);

            Assert.Empty(wl.Talents);
        }
    }
    #endregion 1pointTalents
}
