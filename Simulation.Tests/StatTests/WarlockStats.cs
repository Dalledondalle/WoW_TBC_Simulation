using Simulation.Library;
using System;
using Xunit;

namespace StatTests
{
    public class WarlockStats
    {
        [Theory]
        [InlineData(100, 100, 6.34)]
        [InlineData(158, 158, 10.02)]
        [InlineData(285, 285, 18.07)]
        [InlineData(489, 489, 31.01)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0.06)]
        [InlineData(-100, -100, -6.34)]
        [InlineData(-158, -158, -10.02)]
        [InlineData(-285, -285, -18.07)]
        [InlineData(-489, -489, -31.01)]
        [InlineData(-1, -1, -0.06)]
        public void AddHasteRatingTests(int hasteRating, int expectedHasteRating, double expectedHaste)
        {
            Warlock wl = new();
            wl.AddHasteRating(hasteRating);

            Assert.Equal(expectedHasteRating, wl.SpellHasteRating);
            Assert.Equal(expectedHaste, Math.Round(wl.SpellHaste, 2));
        }

        [Theory]
        [InlineData(100, 2821)]
        [InlineData(158, 2727)]
        [InlineData(285, 2541)]
        [InlineData(489, 2290)]
        [InlineData(0, 3000)]
        [InlineData(1, 2998)]
        [InlineData(-100, 3203)]
        [InlineData(-158, 3334)]
        [InlineData(-285, 3662)]
        [InlineData(-489, 4348)]
        [InlineData(-1, 3002)]
        [InlineData(1000, 1836)]
        [InlineData(1200, 1704)]
        [InlineData(1500, 1538)]
        public void ShadowBoltHasteRatingTests(int hasteRating, double expectedWait)
        {
            Warlock wl = new();
            Wowhead wh = new();
            var shadowbolt = wh.GetSpell(27209);
            wl.AddHasteRating(hasteRating);
            wl.CastSpell(shadowbolt, new Dummy(), 100, new Report());

            Assert.Equal(expectedWait, Math.Round(wl.WaitForNextCast()));
        }

        [Theory]
        [InlineData(100, 1411)]
        [InlineData(158, 1363)]
        [InlineData(285, 1270)]
        [InlineData(489, 1145)]
        [InlineData(0, 1500)]
        [InlineData(1, 1499)]
        [InlineData(-100, 1602)]
        [InlineData(-158, 1667)]
        [InlineData(-285, 1831)]
        [InlineData(-489, 2174)]
        [InlineData(-1, 1501)]
        [InlineData(1000, 1000)]
        [InlineData(1200, 1000)]
        [InlineData(1500, 1000)]
        public void LifeTapHasteRatingTests(int hasteRating, double expectedWait)
        {
            Warlock wl = new();
            Wowhead wh = new();
            var lifetap = wh.GetSpell(27222);
            wl.AddHasteRating(hasteRating);
            wl.CastLifeTap(lifetap);

            Assert.Equal(expectedWait, Math.Round(wl.WaitForNextCast()));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(30, 30)]
        [InlineData(-10, -10)]
        [InlineData(-30, -30)]
        public void AddHasteTests(double haste, double expectedHaste)
        {
            Warlock wl = new();
            wl.AddHaste(haste);

            Assert.Equal(expectedHaste, wl.SpellHaste);
        }

        [Theory]
        [InlineData(0, 3000)]
        [InlineData(1, 2970)]
        [InlineData(10, 2727)]
        [InlineData(30, 2308)]
        [InlineData(50, 2000)]
        [InlineData(100, 1500)]
        [InlineData(-1, 3030)]
        [InlineData(-10, 3333)]
        [InlineData(-30, 4286)]
        [InlineData(-50, 6000)]
        public void ShadowBoltHasteTests(double haste, double expectedWait)
        {
            Warlock wl = new();
            Wowhead wh = new();
            var shadowbolt = wh.GetSpell(27209);
            wl.AddHaste(haste);
            wl.CastSpell(shadowbolt, new Dummy(), 100, new Report());

            Assert.Equal(expectedWait, Math.Round(wl.WaitForNextCast()));
        }

        [Theory]
        [InlineData(0, 1500)]
        [InlineData(1, 1485)]
        [InlineData(10, 1364)]
        [InlineData(30, 1154)]
        [InlineData(50, 1000)]
        [InlineData(100, 1000)]
        [InlineData(-1, 1515)]
        [InlineData(-10, 1667)]
        [InlineData(-30, 2143)]
        [InlineData(-50, 3000)]
        public void LifeTapHasteTests(double haste, double expectedWait)
        {
            Warlock wl = new();
            Wowhead wh = new();
            var lifetap = wh.GetSpell(27222);
            wl.AddHaste(haste);
            wl.CastLifeTap(lifetap);

            Assert.Equal(expectedWait, Math.Round(wl.WaitForNextCast()));
        }

        [Theory]
        [InlineData(0, 0, 3000)]
        [InlineData(0, 1, 2970)]
        [InlineData(0, 10, 2727)]
        [InlineData(100, 0, 2821)]
        [InlineData(158, 0, 2727)]
        [InlineData(285, 0, 2541)]
        [InlineData(100, 10, 2579)]
        [InlineData(158, 20, 2307)]
        [InlineData(285, 5, 2438)]
        public void ShadowBoltHasteAndHasteRatingTests(int hasteRating, int haste, double expectedWait)
        {
            Warlock wl = new();
            Wowhead wh = new();
            var shadowbolt = wh.GetSpell(27209);
            wl.AddHaste(haste);
            wl.AddHasteRating(hasteRating);
            wl.CastSpell(shadowbolt, new Dummy(), 100, new Report());

            Assert.Equal(expectedWait, Math.Round(wl.WaitForNextCast()));
        }

        [Theory]
        [InlineData(0, 0, 0, 3.3)]
        [InlineData(32, 36, 32, 5.19)]
        [InlineData(45, 58, 45, 6.04)]
        [InlineData(85, 58, 85, 7.85)]
        public void AddCritRatingTests(int critRating, int intellect, int expectedCritRating, double expectedCritChance)
        {
            Warlock wl = new();
            wl.AddCritRating(critRating);
            wl.AddIntellect(intellect);

            Assert.Equal(expectedCritRating, wl.SpellCritRating);
            Assert.Equal(expectedCritChance, Math.Round(wl.SpellCrit, 2));
        }

        [Theory]
        [InlineData(0, 4300, 4300)]
        [InlineData(1000, 4300, 4300)]
        [InlineData(-500, 3800, 4300)]
        [InlineData(-1000, 3300, 4300)]
        public void ManaTests(int mana, int expectedMana, int expectedMaxMana)
        {
            Warlock wl = new();
            wl.AddMana(mana);

            Assert.Equal(expectedMana, wl.Mana);
            Assert.Equal(expectedMaxMana, wl.MaxMana);
        }

        [Theory]
        [InlineData(0, 4300)]
        [InlineData(36, 4840)]
        [InlineData(58, 5170)]
        [InlineData(235, 7825)]
        public void ManaAndIntellectTests(int intellect, int expectedMaxMana)
        {
            Warlock wl = new();
            wl.AddIntellect(intellect);

            Assert.Equal(expectedMaxMana, wl.MaxMana);
        }

        [Fact]
        public void DrumsOfBattleTests()
        {
            Warlock wl = new();
            wl.AddHasteRating(150);

            Aura DrumsOfBattle = new("35476", "Drums Of Battle", AuraType.Buff);
            DrumsOfBattle.SpellHasteRatingMod = 80;
            wl.AddAura(DrumsOfBattle);

            Assert.Equal(230, wl.SpellHasteRating);
        }

        [Fact]
        public void BloodlustTests()
        {
            Warlock wl = new();
            wl.AddHasteRating(158);

            Aura Bloodlust = new("2825", "Bloodlust", AuraType.Buff);
            Bloodlust.SpellHasteModifer = 30;
            wl.AddAura(Bloodlust);

            Assert.Equal(40.02, Math.Round(wl.SpellHaste, 2));
        }

        [Fact]
        public void BlessingOfKingsTests()
        {
            Warlock wl = new();
            wl.AddIntellect(123); //254 since warlock starts with 131 int base

            Aura BlessingOfKings = new("25898", "Greater Blessing of Kings", AuraType.Buff);
            BlessingOfKings.IntModifer = 10;
            wl.AddAura(BlessingOfKings);

            Assert.Equal(279, wl.Intellect);
        }

        [Fact]
        public void FelArmorUntalentedTests()
        {
            Warlock wl = new();
            wl.AddSpellPower(123);

            Aura FelArmor = new("28189", "Fel Armor", AuraType.Buff);
            FelArmor.FlatSpellMod = 100;
            wl.AddAura(FelArmor);

            Assert.Equal(223, wl.SpellPower);
        }

        [Fact]
        public void BlessingOfWisdomTests()
        {
            Warlock wl = new();
            wl.AddIntellect(123); //254 since warlock starts with 131 int base

            Aura BlessingOfWisdom = new("27143", "Greater Blessing of Wisdom", AuraType.Buff);
            BlessingOfWisdom.FlatManaRegenMod = 41;
            wl.AddAura(BlessingOfWisdom);

            Assert.Equal(41, wl.MP5);
        }

        [Fact]
        public void ArcaneIntellectTests()
        {
            Warlock wl = new();
            wl.AddIntellect(123); //254 since warlock starts with 131 int base

            Aura ArcaneIntellect = new("3738", "Arcane Brilliance", AuraType.Buff);
            ArcaneIntellect.FlatIntMod = 40;
            wl.AddAura(ArcaneIntellect);

            Assert.Equal(294, wl.Intellect);
        }

        [Fact]
        public void TotemOfWrathTests()
        {
            Warlock wl = new();
            wl.AddIntellect(123); //254 since warlock starts with 131 int base

            Aura TotemOfWrath = new("30706", "Totem Of Wrath", AuraType.Buff);
            TotemOfWrath.SpellHitModifer = 3;
            TotemOfWrath.SpellCritModifer = 3;
            wl.AddAura(TotemOfWrath);

            Assert.Equal(3, wl.SpellHit);
            Assert.Equal(7.80, Math.Round(wl.SpellCrit,2));
        }
    }
}
