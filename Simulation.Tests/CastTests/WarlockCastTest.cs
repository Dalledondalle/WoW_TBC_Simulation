using Simulation.Library;
using System;
using Xunit;

namespace CastTests
{
    public class WarlockCasts
    {
        [Theory]
        [InlineData(1454, 1030)]
        [InlineData(1455, 1075)]
        [InlineData(1456, 1140)]
        [InlineData(11687, 1220)]
        [InlineData(11688, 1310)]
        [InlineData(11689, 1430)]
        [InlineData(27222, 1582)]
        public void LifeTapTests(int id, double expectedMana)
        {
            Warlock wl = new();
            Wowhead wh = new();
            var lifetap = wh.GetSpell(id);
            wl.AddMana(-10000);
            wl.AddMana(1000);

            wl.CastLifeTap(lifetap);

            Assert.Equal(expectedMana, wl.Mana);
        }

        [Fact]
        public void LastSpellLifeTapTest()
        {
            Warlock wl = new();
            Wowhead wh = new();
            var lifetap = wh.GetSpell(1456);
            wl.CastLifeTap(lifetap);

            Assert.Equal(lifetap.Name, wl.lastSpelledCasted.Name);
        }

        [Fact]
        public void LastSpellShadowBoltTest()
        {
            Warlock wl = new();
            Wowhead wh = new();
            var shadowbolt = wh.GetSpell(27209);
            wl.CastShadowBolt(shadowbolt);

            Assert.Equal(shadowbolt.Name, wl.lastSpelledCasted.Name);
        }
    }
}
