using Simulation.Library;
using System;
using System.Linq;
using Xunit;

namespace TalentTests.Destruction
{
    #region 5pointsTalents
    public class BaneTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.BaneRank = rank;

            var Bane = wl.Talents.FirstOrDefault(b => b.Name == "Bane");
            Assert.Equal(rank, Bane.Level);
            Assert.Equal("Bane", Bane.Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.BaneRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.BaneRank = rank;

            var Bane = wl.Talents.FirstOrDefault(b => b.Name == "Bane");
            Assert.Equal(5, Bane.Level);
            Assert.Equal("Bane", Bane.Name);
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
            wl.BaneRank = 1;
            wl.BaneRank = rank;

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
            wl.BaneRank = 5;
            wl.BaneRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(6, 2500)]
        [InlineData(5, 2500)]
        [InlineData(4, 2600)]
        [InlineData(3, 2700)]
        [InlineData(2, 2800)]
        [InlineData(1, 2900)]
        [InlineData(0, 3000)]
        public void CastTimeOnSBTests(int rank, int expectedCasttime)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.BaneRank = rank;
            var shadowbolt = wh.GetSpell(27209);

            wl.CastSpell(shadowbolt, new Dummy(), 100, new Report());

            Assert.Equal(expectedCasttime, wl.lastSpelledCasted.CastTime);
        }
    }
    public class ImprovedShadowBoltTests
    {

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedShadowBoltsRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Improved Shadow Bolt", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedShadowBoltsRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedShadowBoltsRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal("Improved Shadow Bolt", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedShadowBoltsRank = 1;
            wl.ImprovedShadowBoltsRank = rank;

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
            wl.BaneRank = 5;
            wl.BaneRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class CataclysmTests
    {

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.CataclysmRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Cataclysm", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.CataclysmRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.CataclysmRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal("Cataclysm", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.CataclysmRank = 1;
            wl.CataclysmRank = rank;

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
            wl.CataclysmRank = 5;
            wl.CataclysmRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class DevastationTests
    {

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.DevastationRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Devastation", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.DevastationRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.DevastationRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal("Devastation", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void TalentTests(int rank)
        {
            Warlock wl = new();
            wl.DevastationRank = 1;
            wl.DevastationRank = rank;

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
            wl.DevastationRank = 5;
            wl.DevastationRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class ImprovedImmolateTests
    {

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedImmolateRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Improved Immolate", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedImmolateRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedImmolateRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal("Improved Immolate", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DevastationTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedImmolateRank = 1;
            wl.ImprovedImmolateRank = rank;

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
            wl.ImprovedImmolateRank = 5;
            wl.ImprovedImmolateRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class EmberstormTests
    {

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.EmberstormRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Emberstorm", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.EmberstormRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.EmberstormRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal("Emberstorm", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DevastationTalentTests(int rank)
        {
            Warlock wl = new();
            wl.EmberstormRank = 1;
            wl.EmberstormRank = rank;

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
            wl.EmberstormRank = 5;
            wl.EmberstormRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class ShadowAndFlameTests
    {

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.ShadowAndFlameRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Shadow and Flame", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ShadowAndFlameRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank5Tests(int rank)
        {
            Warlock wl = new();
            wl.ShadowAndFlameRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(5, wl.Talents.First().Level);
            Assert.Equal("Shadow and Flame", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void DevastationTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ShadowAndFlameRank = 1;
            wl.ShadowAndFlameRank = rank;

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
            wl.ShadowAndFlameRank = 5;
            wl.ShadowAndFlameRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    #endregion 5pointsTalents

    #region 3pointsTalents
    public class ImprovedSearingPainTest
    {

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedSearingPainRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Improved Searing Pain", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedSearingPainRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank3Tests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedSearingPainRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(3, wl.Talents.First().Level);
            Assert.Equal("Improved Searing Pain", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedSearingPainRank = 1;
            wl.ImprovedSearingPainRank = rank;

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
            wl.ImprovedSearingPainRank = 3;
            wl.ImprovedSearingPainRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    #endregion 3pointsTalents

    #region 2pointsTalents
    public class ImprovedFireboltTest
    {

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedFireboltRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Improved Firebolt", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedFireboltRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank2Tests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedFireboltRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(2, wl.Talents.First().Level);
            Assert.Equal("Improved Firebolt", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedFireboltRank = 1;
            wl.ImprovedFireboltRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedFireboltRank = 2;
            wl.ImprovedFireboltRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    public class ImprovedLashOfPainTest
    {

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void AddTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedLashOfPainRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal("Improved Lash of Pain", wl.Talents.First().Name);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedLashOfPainRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank2Tests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedLashOfPainRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(2, wl.Talents.First().Level);
            Assert.Equal("Improved Lash of Pain", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void IncreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedLashOfPainRank = 1;
            wl.ImprovedLashOfPainRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void DecreaseTalentTests(int rank)
        {
            Warlock wl = new();
            wl.ImprovedLashOfPainRank = 2;
            wl.ImprovedLashOfPainRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(rank, wl.Talents.First().Level);
        }
    }
    #endregion 2pointsTalents

    #region 1pointTalents
    public class ShadowburnTest
    {

        [Fact]
        public void AddTests()
        {
            Warlock wl = new();
            wl.ShadowburnRank = 1;

            Assert.Single(wl.Talents);
            Assert.Equal("Shadowburn", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ShadowburnRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank1Tests(int rank)
        {
            Warlock wl = new();
            wl.ShadowburnRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal("Shadowburn", wl.Talents.First().Name);
        }

        [Fact]
        public void RemoveTests()
        {
            Warlock wl = new();
            wl.ShadowburnRank = 1;

            wl.ShadowburnRank = 0;

            Assert.Empty(wl.Talents);
        }
    }
    public class RuinTest
    {

        [Fact]
        public void AddTests()
        {
            Warlock wl = new();
            wl.RuinRank = 1;

            Assert.Single(wl.Talents);
            Assert.Equal("Ruin", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.RuinRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank1Tests(int rank)
        {
            Warlock wl = new();
            wl.RuinRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal("Ruin", wl.Talents.First().Name);
        }

        [Fact]
        public void RemoveTests()
        {
            Warlock wl = new();
            wl.RuinRank = 1;

            wl.RuinRank = 0;

            Assert.Empty(wl.Talents);
        }
    }
    public class ConflagrateTest
    {

        [Fact]
        public void AddTests()
        {
            Warlock wl = new();
            wl.ConflagrateRank = 1;

            Assert.Single(wl.Talents);
            Assert.Equal("Conflagrate", wl.Talents.First().Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        public void NullTests(int rank)
        {
            Warlock wl = new();
            wl.ConflagrateRank = rank;

            Assert.Empty(wl.Talents);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void AboveRank1Tests(int rank)
        {
            Warlock wl = new();
            wl.ConflagrateRank = rank;

            Assert.Single(wl.Talents);
            Assert.Equal(1, wl.Talents.First().Level);
            Assert.Equal("Conflagrate", wl.Talents.First().Name);
        }

        [Fact]
        public void RemoveTests()
        {
            Warlock wl = new();
            wl.ConflagrateRank = 1;
            wl.ConflagrateRank = 0;

            Assert.Empty(wl.Talents);
        }
    }
    public class ShadowfuryTests
    {
        private static string rank = "Rank";
        private static string propertyName = "Shadowfury" + rank;
        private static string talentName = "Shadowfury";
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
