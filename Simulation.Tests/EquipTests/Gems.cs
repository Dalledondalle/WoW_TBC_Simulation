using Simulation.Library;
using System;
using Xunit;


namespace EquipTests
{
    public class Gems
    {
        [Fact]
        public void Blue15StaGemTest()
        {
            Wowhead wh = new();
            Gem gem = wh.GetGem(32200);

            Assert.Equal(15, gem.Stamina);
            Assert.Equal("Blue", gem.Color);
        }

        [Fact]
        public void Blue13SpellPenTest()
        {
            Wowhead wh = new();
            Gem gem = wh.GetGem(32203);

            Assert.Equal(13, gem.SpellPenetration);
            Assert.Equal("Blue", gem.Color);
        }

        [Fact]
        public void Red12SpellPowerTest()
        {
            Wowhead wh = new();
            Gem gem = wh.GetGem(32196);

            Assert.Equal(12, gem.SpellPower);
            Assert.Equal("Red", gem.Color);
        }

        [Fact]
        public void Red10AgilityTest()
        {
            Wowhead wh = new();
            Gem gem = wh.GetGem(32194);

            Assert.Equal(10, gem.Agility);
            Assert.Equal("Red", gem.Color);
        }

        [Fact]
        public void Yellow10IntellectTest()
        {
            Wowhead wh = new();
            Gem gem = wh.GetGem(32204);

            Assert.Equal(10, gem.Intellect);
            Assert.Equal("Yellow", gem.Color);
        }

        [Fact]
        public void Yellow10HitRatingTest()
        {
            Wowhead wh = new();
            Gem gem = wh.GetGem(32206);

            Assert.Equal(10, gem.MeleeHitRating);
            Assert.Equal(10, gem.RangedHitRating);
            Assert.Equal("Yellow", gem.Color);
        }

        [Fact]
        public void EquipmentWith1Socket1Correct1GemTest()
        {
            Wowhead wh = new();
            var equipment = wh.GetEquipment(34564);
            var gem = wh.GetGem(32218); //Orange Gem

            equipment.SocketGem(gem, 1);

            Assert.Equal(gem, equipment.Gem1);
            Assert.True(equipment.IsSocketBonusActive);
        }

        [Fact]
        public void SocketGemIntoSlotTheDoesNotExistTest()
        {
            Wowhead wh = new();
            var equipment = wh.GetEquipment(34564);
            var gem = wh.GetGem(32218); //Orange Gem

            equipment.SocketGem(gem, 2);

            Assert.Null(equipment.Gem1);
            Assert.False(equipment.IsSocketBonusActive);
        }

        [Fact]
        public void MetaGemInNotMetaSocket()
        {
            Wowhead wh = new();
            var equipment = wh.GetEquipment(34564);
            var gem = wh.GetGem(32409); //Meta

            equipment.SocketGem(gem, 1);

            Assert.Null(equipment.Gem1);
            Assert.False(equipment.IsSocketBonusActive);
        }

        [Theory]
        [InlineData(32218, 32218, true)] //double orange
        [InlineData(32215, 32215, false)] //double purple
        [InlineData(32196, 32196, false)] //double red
        [InlineData(32226, 32226, false)] //double green
        [InlineData(32206, 32206, false)] //double yellow
        [InlineData(24033, 24033, false)] //double blue
        [InlineData(32196, 32206, true)] // red yellow
        [InlineData(32206, 32196, false)] //double yellow red
        [InlineData(32196, 24033, false)] //double red blue
        [InlineData(24033, 32206, false)] //double blue yellow
        public void EquipmentWith2Sockets(int gem1Id, int gem2Id, bool expectedBonus)
        {
            Wowhead wh = new();
            var equipment = wh.GetEquipment(28517); //One red One Yellow
            var gem1 = wh.GetGem(gem1Id);
            var gem2 = wh.GetGem(gem2Id);

            equipment.SocketGem(gem1, 1);
            equipment.SocketGem(gem2, 2);

            Assert.Equal(gem1, equipment.Gem1);
            Assert.Equal(gem2, equipment.Gem2);
            Assert.Equal(expectedBonus, equipment.IsSocketBonusActive);
        }

        [Fact]
        public void MP5SocketBonusTest()
        {
            Warlock wl = new();
            Wowhead wh = new();

            var gem = wh.GetGem(32206); // Yellow
            var metaGem = wh.GetGem(32409); //Meta
            var equipment = wh.GetEquipment(30152);

            wl.EquipHead(equipment);

            Assert.Equal(8, wl.MP5);

            wl.Head.SocketGem(metaGem, 1);
            wl.Head.SocketGem(gem, 2);

            Assert.Equal(10, wl.MP5);
        }
    }
}
