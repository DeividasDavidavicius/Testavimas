using Models.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class DecoratorTests
    {
        [TestMethod]
        public void TestArmorFrame()
        {
            // Arrange
            Armor armor = new ArmorFrame(100);

            // Act
            int result = armor.GiveArmor();

            // Assert
            Assert.AreEqual(100, result);
        }
        [TestMethod]
        public void TestPlayerChestplate()
        {
            // Arrange
            Armor armor = new ArmorFrame(100);
            PlayerChestplate chestplate = new PlayerChestplate(armor);

            // Act
            int result = chestplate.GiveArmor();

            // Assert
            Assert.AreEqual(300, result);
        }
        [TestMethod]
        public void TestPlayerGlove()
        {
            // Arrange
            Armor armor = new ArmorFrame(100);
            PlayerGlove glove = new PlayerGlove(armor);

            // Act
            int result = glove.GiveArmor();

            // Assert
            Assert.AreEqual(150, result);
        }
        [TestMethod]
        public void TestPlayerPants()
        {
            // Arrange
            Armor armor = new ArmorFrame(100);
            PlayerPants pants = new PlayerPants(armor);

            // Act
            int result = pants.GiveArmor();

            // Assert
            Assert.AreEqual(200, result);
        }
        [TestMethod]
        public void TestAirDropDecorator()
        {
            // Arrange
            Armor armor = new ArmorFrame(100);
            AirDropDecorator airDropDecorator = new AirDropDecorator(armor);

            // Act
            int result = airDropDecorator.GiveArmor();

            // Assert
            Assert.AreEqual(100, result);
        }
    }
}
