using Simulation.Library;
using System;
using System.Linq;
using Xunit;

namespace Simulation.Tests
{
    public class TalentTest
    {
        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void BaneTests(int rank)
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
        public void BaneNullTests(int rank)
        {
            Warlock wl = new();
            wl.BaneRank = rank;

            var Bane = wl.Talents.FirstOrDefault(b => b.Name == "Bane");
            Assert.Null(Bane);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(6)]
        public void BaneAboveRank5Tests(int rank)
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
        public void IncreaseBaneTalent(int rank)
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
        public void DecreaseBaneTalent(int rank)
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
        public void BaneCastTimeOnSBTests(int rank, int expectedCasttime)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.BaneRank = rank;
            var shadowbolt = wh.GetSpell(27209);

            wl.CastShadowBolt(shadowbolt);

            Assert.Equal(expectedCasttime, wl.lastSpelledCasted.CastTime);
        }
    }
}
