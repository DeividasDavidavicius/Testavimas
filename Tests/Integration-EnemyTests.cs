using Models.Cells;
using Models.Strategy;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AbstractFactory;

namespace Tests
{

    [TestClass]
    public class EnemyTests
    {
        [TestMethod]
        public void DemonFactory_CorrectlyGeneratesEnemies()
        {
            // Arrange
            DemonFactory factory = new DemonFactory();

            // Act
            var weakEnemy = factory.CreateWeakEnemy();
            var mediumEnemy = factory.CreateMediumEnemy();
            var strongEnemy = factory.CreateStrongEnemy();
            var randomEnemy = factory.GenerateCell();

            for (int i = 0; i < 100; i++)
            {
                factory.GenerateCell();
            }

            // Assert
            Assert.IsTrue(weakEnemy.GetType() == typeof(WeakDemon));
            Assert.IsTrue(mediumEnemy.GetType() == typeof(MediumDemon));
            Assert.IsTrue(strongEnemy.GetType() == typeof(StrongDemon));
            Assert.IsTrue(strongEnemy.healthPoints > 60);
            Assert.IsTrue(randomEnemy is EnemyCell);
        }

        [TestMethod]
        public void OrcFactory_CorrectlyGeneratesEnemies()
        {
            // Arrange
            OrcFactory factory = new OrcFactory();

            // Act
            var weakEnemy = factory.CreateWeakEnemy();
            var mediumEnemy = factory.CreateMediumEnemy();
            var strongEnemy = factory.CreateStrongEnemy();
            var randomEnemy = factory.GenerateCell();

            for (int i = 0; i < 100; i++)
            {
                factory.GenerateCell();
            }

            // Assert
            Assert.IsTrue(weakEnemy.GetType() == typeof(WeakOrc));
            Assert.IsTrue(mediumEnemy.GetType() == typeof(MediumOrc));
            Assert.IsTrue(strongEnemy.GetType() == typeof(StrongOrc));
            Assert.IsTrue(strongEnemy.healthPoints > 80);
            Assert.IsTrue(randomEnemy is EnemyCell);
        }
    }
}
