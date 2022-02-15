using Simulation.Library;
using System;
using Xunit;


namespace EquipTests
{
    public class WarlockEquipment
    {
        [Theory]
        [InlineData(31051)] //Hood of the malefic
        [InlineData(33677)] //Vengeful Gladiator's Dreadweave Hood
        [InlineData(32525)] //Cowl of the Illidari High Lord
        public void AddHeadTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipHead(wh.GetItem(itemId));

            Assert.NotNull(wl.Head);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToHead(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipHead(wh.GetItem(itemId));

            Assert.Null(wl.Head);
        }

        [Theory]
        [InlineData(34360)] //Amulet of Flowing Life
        [InlineData(34184)] //Brooch of the Highborne
        public void AddNeckTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipNeck(wh.GetItem(itemId));

            Assert.NotNull(wl.Neck);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToNeck(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipNeck(wh.GetItem(itemId));

            Assert.Null(wl.Neck);
        }

        [Theory]
        [InlineData(31054)] //Mantle of the Malefic
        [InlineData(35006)] //Brutal Gladiator's Dreadweave Mantle
        public void AddShouldersTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipShoulders(wh.GetItem(itemId));

            Assert.NotNull(wl.Shoulders);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToShoulders(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipShoulders(wh.GetItem(itemId));

            Assert.Null(wl.Shoulders);
        }

        [Theory]
        [InlineData(34242)] //Tattered Cape of Antonidas
        [InlineData(32331)] //Cloak of the Illidary Council
        public void AddBackTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipBack(wh.GetItem(itemId));

            Assert.NotNull(wl.Back);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToBack(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipBack(wh.GetItem(itemId));

            Assert.Null(wl.Back);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(31052)] //Robe of the Malefic
        public void AddChestTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipChest(wh.GetItem(itemId));

            Assert.NotNull(wl.Chest);
        }

        [Theory]
        [InlineData(32331)] //Cloak of the Illidary Council
        [InlineData(1)] //Not found
        public void AddWrongItemToChest(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipBack(wh.GetItem(itemId));

            Assert.Null(wl.Chest);
        }

        [Theory]
        [InlineData(34436)] //Bracers of the Malefic
        [InlineData(32586)] //Bracers of the Nimble Thought
        public void AddWristTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipWrist(wh.GetItem(itemId));

            Assert.NotNull(wl.Wrist);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToWrist(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipWrist(wh.GetItem(itemId));

            Assert.Null(wl.Wrist);
        }

        [Theory]
        [InlineData(31050)] //Gloves of the Malefic
        [InlineData(35003)] //Brutal Gladiator's Dreadweave Gloves
        public void AddHandsTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipHands(wh.GetItem(itemId));

            Assert.NotNull(wl.Hands);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToHands(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipHands(wh.GetItem(itemId));

            Assert.Null(wl.Hands);
        }

        [Theory]
        [InlineData(34541)] //Belt of the Malefic
        [InlineData(30888)] //Anetheron's Noose
        public void AddWaistTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipWaist(wh.GetItem(itemId));

            Assert.NotNull(wl.Waist);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToWaist(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipWaist(wh.GetItem(itemId));

            Assert.Null(wl.Waist);
        }

        [Theory]
        [InlineData(31053)] //Leggins of the Malefic
        [InlineData(35005)] //Brutal Gladiator's Dreadweave Leggings
        public void AddLegsTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipLegs(wh.GetItem(itemId));

            Assert.NotNull(wl.Legs);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToLegs(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipLegs(wh.GetItem(itemId));

            Assert.Null(wl.Legs);
        }

        [Theory]
        [InlineData(34564)] //Boots of the Malefic
        [InlineData(35138)] //Guardian's Dreadweave Stalkers
        public void AddFeetTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipFeet(wh.GetItem(itemId));

            Assert.NotNull(wl.Feet);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToFeet(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipFeet(wh.GetItem(itemId));

            Assert.Null(wl.Feet);
        }

        [Theory]
        [InlineData(34362)] //Loop of Forged Power
        [InlineData(35129)] //Guardian's Band of Dominance
        public void AddRingTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipRing1(wh.GetItem(itemId));
            wl.EquipRing2(wh.GetItem(itemId));

            Assert.NotNull(wl.Ring1);
            Assert.NotNull(wl.Ring2);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToRings(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipRing1(wh.GetItem(itemId));
            wl.EquipRing2(wh.GetItem(itemId));

            Assert.Null(wl.Ring1);
            Assert.Null(wl.Ring2);
        }

        [Theory]
        [InlineData(34429)] //Shifting Naaru Sliver
        [InlineData(35326)] //Battlemaster's Alacrity
        public void AddTrinketTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipTrinket1(wh.GetItem(itemId));
            wl.EquipTrinket2(wh.GetItem(itemId));

            Assert.NotNull(wl.Trinket1);
            Assert.NotNull(wl.Trinket2);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToTrinkets(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipTrinket1(wh.GetItem(itemId));
            wl.EquipTrinket2(wh.GetItem(itemId));

            Assert.Null(wl.Trinket1);
            Assert.Null(wl.Trinket2);
        }

        [Theory]
        [InlineData(34336)] //Sunflare (1h)
        [InlineData(37739)] //Brutal Gladiator's Blade of Alacrity(1h)
        public void AddMainhandTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipMainhand(wh.GetItem(itemId));

            Assert.NotNull(wl.Mainhand);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToMainhand(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipFeet(wh.GetItem(itemId));

            Assert.Null(wl.Mainhand);
        }

        [Theory]
        [InlineData(30313)] //Staff of Disintegration(2h)
        [InlineData(34337)] //Golden Staff of the Sin'dorei(2h)
        public void AddTwoHandTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipOffhand(wh.GetItem(34206));
            wl.EquipMainhand(wh.GetItem(itemId));

            Assert.Null(wl.Offhand);
            Assert.NotNull(wl.Mainhand);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToTwoHand(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipMainhand(wh.GetItem(itemId));

            Assert.Null(wl.Mainhand);
        }

        [Theory]
        [InlineData(34206)] //Book of HighborneHymns
        [InlineData(35016)] //Brutal Gladiator's Grimoire
        public void AddOffhandTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipOffhand(wh.GetItem(itemId));

            Assert.NotNull(wl.Offhand);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToOffhand(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipOffhand(wh.GetItem(itemId));

            Assert.Null(wl.Offhand);
        }

        [Theory]
        [InlineData(34348)] //Wand of Cleansing Light
        [InlineData(34347)] //Wand of the Demonsoul
        public void AddRangedTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipRanged(wh.GetItem(itemId));

            Assert.NotNull(wl.Ranged);
        }

        [Theory]
        [InlineData(21871)] //Shadoweave Robe
        [InlineData(1)] //Not found
        public void AddWrongItemToRanged(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipRanged(wh.GetItem(itemId));

            Assert.Null(wl.Ranged);
        }

        [Theory]
        [InlineData(30313)] //Staff of Disintegration(2h)
        [InlineData(34337)] //Golden Staff of the Sin'dorei(2h)
        public void AddOffhandOnTwoHandTests(int itemId)
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipMainhand(wh.GetItem(itemId));
            wl.EquipOffhand(wh.GetItem(34206));

            Assert.Null(wl.Offhand);
            Assert.NotNull(wl.Mainhand);
        }

        [Fact]
        public void EquipFullSetWithTwohander()
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipHead(wh.GetItem(31051));
            wl.EquipNeck(wh.GetItem(34204));
            wl.EquipShoulders(wh.GetItem(31054));
            wl.EquipBack(wh.GetItem(32331));
            wl.EquipChest(wh.GetItem(31052));
            wl.EquipWrist(wh.GetItem(32586));
            wl.EquipHands(wh.GetItem(31050));
            wl.EquipWaist(wh.GetItem(34541));
            wl.EquipLegs(wh.GetItem(31053));
            wl.EquipFeet(wh.GetItem(34564));
            wl.EquipRing1(wh.GetItem(34362));
            wl.EquipRing2(wh.GetItem(29305));
            wl.EquipTrinket1(wh.GetItem(34429));
            wl.EquipTrinket2(wh.GetItem(35326));
            wl.EquipMainhand(wh.GetItem(34337));
            wl.EquipOffhand(wh.GetItem(34206));
            wl.EquipRanged(wh.GetItem(34347));
            wl.EquipOffhand(wh.GetItem(34206));

            Assert.NotNull(wl.Head);
            Assert.NotNull(wl.Neck);
            Assert.NotNull(wl.Shoulders);
            Assert.NotNull(wl.Back);
            Assert.NotNull(wl.Chest);
            Assert.NotNull(wl.Wrist);
            Assert.NotNull(wl.Hands);
            Assert.NotNull(wl.Waist);
            Assert.NotNull(wl.Legs);
            Assert.NotNull(wl.Feet);
            Assert.NotNull(wl.Ring1);
            Assert.NotNull(wl.Ring2);
            Assert.NotNull(wl.Trinket1);
            Assert.NotNull(wl.Trinket2);
            Assert.NotNull(wl.Mainhand);
            Assert.NotNull(wl.Ranged);
            Assert.Null(wl.Offhand);
        }

        [Fact]
        public void EquipFullSetWithMainHandAndOffHand()
        {
            Warlock wl = new();
            Wowhead wh = new();
            wl.EquipHead(wh.GetItem(31051));
            wl.EquipNeck(wh.GetItem(34204));
            wl.EquipShoulders(wh.GetItem(31054));
            wl.EquipBack(wh.GetItem(32331));
            wl.EquipChest(wh.GetItem(31052));
            wl.EquipWrist(wh.GetItem(32586));
            wl.EquipHands(wh.GetItem(31050));
            wl.EquipWaist(wh.GetItem(34541));
            wl.EquipLegs(wh.GetItem(31053));
            wl.EquipFeet(wh.GetItem(34564));
            wl.EquipRing1(wh.GetItem(34362));
            wl.EquipRing2(wh.GetItem(29305));
            wl.EquipTrinket1(wh.GetItem(34429));
            wl.EquipTrinket2(wh.GetItem(35326));
            wl.EquipMainhand(wh.GetItem(34336));
            wl.EquipRanged(wh.GetItem(34347));
            wl.EquipOffhand(wh.GetItem(34206));

            Assert.NotNull(wl.Head);
            Assert.NotNull(wl.Neck);
            Assert.NotNull(wl.Shoulders);
            Assert.NotNull(wl.Back);
            Assert.NotNull(wl.Chest);
            Assert.NotNull(wl.Wrist);
            Assert.NotNull(wl.Hands);
            Assert.NotNull(wl.Waist);
            Assert.NotNull(wl.Legs);
            Assert.NotNull(wl.Feet);
            Assert.NotNull(wl.Ring1);
            Assert.NotNull(wl.Ring2);
            Assert.NotNull(wl.Trinket1);
            Assert.NotNull(wl.Trinket2);
            Assert.NotNull(wl.Mainhand);
            Assert.NotNull(wl.Ranged);
            Assert.NotNull(wl.Offhand);
        }
    }
}
