using Simulation.Library;
using System;
using Xunit;


namespace EquipTests
{
    public class Enchants
    {
        [Fact]
        public void SoulfrostOnWeaponTest()
        {
            Wowhead wh = new();
            Warlock wl = new();
            wl.EquipMainhand(wh.GetEquipment(32374));

            Assert.Equal(259, wl.SpellPower);

            var wep = wh.GetEquipment(32374);
            wep.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Equal("Soulfrost", wep.Enchant.Name);
            wl.EquipMainhand(wep);

            Assert.Equal(259 + 54, wl.ShadowPower);
            Assert.Equal(259 + 54, wl.FrostPower);
        }

        [Fact]
        public void WeaponEnchOnHeadTest()
        {
            Wowhead wh = new();

            var head = wh.GetEquipment(31051);
            head.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(head.Enchant);
        }

        [Fact]
        public void WeaponEnchOnNeckTest()
        {
            Wowhead wh = new();

            var neck = wh.GetEquipment(34204);
            neck.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(neck.Enchant);
        }

        [Fact]
        public void WeaponEnchOnShouldersTest()
        {
            Wowhead wh = new();

            var shoulders = wh.GetEquipment(31054);
            shoulders.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(shoulders.Enchant);
        }

        [Fact]
        public void WeaponEnchOnBackTest()
        {
            Wowhead wh = new();

            var back = wh.GetEquipment(32331);
            back.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(back.Enchant);
        }

        [Fact]
        public void WeaponEnchOnChestTest()
        {
            Wowhead wh = new();

            var chest = wh.GetEquipment(31052);
            chest.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(chest.Enchant);
        }

        [Fact]
        public void WeaponEnchOnWristTest()
        {
            Wowhead wh = new();

            var wrist = wh.GetEquipment(32586);
            wrist.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(wrist.Enchant);
        }

        [Fact]
        public void WeaponEnchOnHandsTest()
        {
            Wowhead wh = new();

            var hands = wh.GetEquipment(31050);
            hands.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(hands.Enchant);
        }

        [Fact]
        public void WeaponEnchOnWaistTest()
        {
            Wowhead wh = new();

            var waist = wh.GetEquipment(34541);
            waist.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(waist.Enchant);
        }

        [Fact]
        public void WeaponEnchOnLegsTest()
        {
            Wowhead wh = new();

            var legs = wh.GetEquipment(31053);
            legs.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(legs.Enchant);
        }

        [Fact]
        public void WeaponEnchOnFeetTest()
        {
            Wowhead wh = new();

            var feet = wh.GetEquipment(34564);
            feet.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(feet.Enchant);
        }

        [Fact]
        public void WeaponEnchOnFingerTest()
        {
            Wowhead wh = new();

            var finger = wh.GetEquipment(32527);
            finger.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(finger.Enchant);
        }

        [Fact]
        public void WeaponEnchOnTrinketTest()
        {
            Wowhead wh = new();

            var trinket = wh.GetEquipment(30449);
            trinket.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(trinket.Enchant);
        }

        [Fact]
        public void WeaponEnchOnOffhandTest()
        {
            Wowhead wh = new();

            var offhand = wh.GetEquipment(30872);
            offhand.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(offhand.Enchant);
        }

        [Fact]
        public void WeaponEnchOnRangedTest()
        {
            Wowhead wh = new();

            var ranged = wh.GetEquipment(34347);
            ranged.AddEnchant(Wowhead.WeaponEnchants[0]);

            Assert.Null(ranged.Enchant);
        }
    }
}
