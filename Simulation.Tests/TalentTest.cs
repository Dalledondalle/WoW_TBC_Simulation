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
    }
}
