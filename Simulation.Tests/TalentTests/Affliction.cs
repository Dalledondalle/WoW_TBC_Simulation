using Simulation.Library;
using System.Linq;
using Xunit;

namespace TalentTests.Affliction
{
    #region 5pointsTalents
    public class SuppressionTests
    {
        private static string name = "Suppression";
        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(name + "Rank").SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
            Assert.Equal(name, wl.Talents.First().Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(name + "Rank").SetValue(wl, rank);

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.GetType().GetProperty(name + "Rank").SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal(name, wl.Talents.First().Name);
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
            wl.GetType().GetProperty(name + "Rank").SetValue(wl, 1);
            wl.GetType().GetProperty(name + "Rank").SetValue(wl, rank);

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
            wl.GetType().GetProperty(name + "Rank").SetValue(wl, 1);
            wl.GetType().GetProperty(name + "Rank").SetValue(wl, rank);

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class ImprovedCorruptionTests
    {
        private static string rank = "Rank";
        private static string propertyName = "ImprovedCorruption" + rank;
        private static string talentName = "Improved Corruption";
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
    public class ShadowMasteryTests
    {
        private static string rank = "Rank";
        private static string propertyName = "ShadowMastery" + rank;
        private static string talentName = "Shadow Mastery";
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
    public class ContagionTests
    {
        private static string rank = "Rank";
        private static string propertyName = "Contagion" + rank;
        private static string talentName = "Contagion";
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
    public class EmpoweredCorruptionTests
    {
        private static string rank = "Rank";
        private static string propertyName = "EmpoweredCorruption" + rank;
        private static string talentName = "Empowered Corruption";
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
    public class ImprovedLifeTapTests
    {
        private static string propertyName = "ImprovedLifeTapRank";
        private static string talentName = "Improved Life Tap";
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
    public class SoulSiphonTests
    {
        private static string propertyName = "SoulSiphonRank";
        private static string talentName = "Soul Siphon";
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
    public class ImprovedCurseOfAgonyTests
    {
        private static string propertyName = "ImprovedCurseOfAgonyRank";
        private static string talentName = "Improved Curse of Agony";
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
    public class NightfallTests
    {
        private static string rank = "Rank";
        private static string propertyName = "Nightfall" + rank;
        private static string talentName = "Nightfall";
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
    public class AmplifyCurseTests
    {
        private static string rank = "Rank";
        private static string propertyName = "AmplifyCurse" + rank;
        private static string talentName = "Amplify Curse";
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
    public class SiphonLifeTests
    {
        private static string rank = "Rank";
        private static string propertyName = "SiphonLife" + rank;
        private static string talentName = "Siphon Life";
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
    public class UnstableAfflictionTests
    {
        private static string rank = "Rank";
        private static string propertyName = "UnstableAffliction" + rank;
        private static string talentName = "Unstable Affliction";
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