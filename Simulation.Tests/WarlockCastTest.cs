using Simulation.Library;
using System;
using Xunit;

namespace Simulation.Tests
{
    public class WarlockCastTest
    {
        [Theory]
        [InlineData(0, 0, 0, 0, 544, 607)]
        [InlineData(0, 0, 0, 2000, 544, 607)]
        [InlineData(0, 0, 2000, 0, 2258, 2321)]
        [InlineData(0, 2000, 0, 0, 2258, 2321)]
        [InlineData(11, 0, 0, 0, 544, 607)]
        [InlineData(11, 0, 0, 2000, 544, 607)]
        [InlineData(11, 0, 2000, 0, 2258, 2321)]
        [InlineData(11, 2000, 0, 0, 2258, 2321)]
        public void ShadowBoltRankCastTest(int rank, int spellPower, int shadowPower, int firePower, double expectedMinDmg, double expectedMaxDmg)
        {
            Warlock wl = new();
            wl.AddSpellPower(spellPower);
            wl.AddFireSpellPower(firePower);
            wl.AddShadowSpellPower(shadowPower);            

            double dmg = wl.CastShadowBolt(rank);
            bool withinLimit = (expectedMinDmg <= dmg && expectedMaxDmg >= dmg) || ((expectedMinDmg * 1.5 <= dmg && expectedMaxDmg * 1.5 >= dmg)) || dmg == 0;

            Assert.True(withinLimit);
        }

        [Theory]
        [InlineData(0, 1582)]
        [InlineData(1, 1030)]
        [InlineData(2, 1075)]
        [InlineData(3, 1140)]
        [InlineData(4, 1220)]
        [InlineData(5, 1310)]
        [InlineData(6, 1430)]
        [InlineData(7, 1582)]
        public void LifeTapTests(int rank, double expectedMana)
        {
            Warlock wl = new();
            wl.AddMana(-10000);
            wl.AddMana(1000);

            wl.CastLifeTap(rank);

            Assert.Equal(expectedMana, wl.Mana);
        }

        [Fact]
        public void LastSpellLifeTapTest()
        {
            Warlock wl = new();
            wl.CastLifeTap();

            Assert.Equal(typeof(LifeTap7), wl.lastSpelledCasted.GetType());
        }

        [Fact]
        public void LastSpellShadowBoltTest()
        {
            Warlock wl = new();
            wl.CastShadowBolt();

            Assert.Equal(typeof(ShadowBolt11), wl.lastSpelledCasted.GetType());
        }
    }
}
