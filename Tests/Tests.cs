using Models.Cells;
using Models.Strategy;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using Newtonsoft.Json;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Vector2_CorrectlySetsAndReturnsValues()
        {
            // Arrange
            Models.Vector2 vector = new Models.Vector2(1, 2);

            // Act
            var xValue = vector.getX();
            var yValue = vector.getY();
            vector.setX(3);
            vector.setY(4);

            // Assert
            Assert.AreEqual(1, xValue);
            Assert.AreEqual(2, yValue);
            Assert.AreEqual(3, vector.getX());
            Assert.AreEqual(4, vector.getY());
        }

        [TestMethod]
        public void Player_CorrectlyCreatesObject()
        {
            // Arrange, Act
            Player player = new Player("0", 5, 10, Color.Black, 1000, "Tom");

            // Assert
            Assert.AreEqual(player.id, "0");
            Assert.AreEqual(player.color, Color.Black);
        }

        [TestMethod]
        public void Mountain_NoData_CorrectlyCreatesObject()
        {
            // Arrange, Act
            Mountain mountain = new Mountain();

            // Assert
            Assert.AreEqual(mountain.xPos, -1);
            Assert.AreEqual(mountain.yPos, -1);
        }

        [TestMethod]
        public void Mountain_WithData_CorrectlyCreatesObject()
        {
            // Arrange
            var xPos = 5;
            var yPos = 9;

            // Act
            Mountain mountain = new Mountain(xPos, yPos);

            // Assert
            Assert.AreEqual(mountain.xPos, xPos);
            Assert.AreEqual(mountain.yPos, yPos);
        }

        [TestMethod]
        public void Lava_NoData_CorrectlyCreatesObject()
        {
            // Arrange, Act
            Lava lava = new Lava();

            // Assert
            Assert.AreEqual(lava.xPos, -1);
            Assert.AreEqual(lava.yPos, -1);
        }

        [TestMethod]
        public void Lava_WithData_CorrectlyCreatesObject()
        {
            // Arrange
            var xPos = 5;
            var yPos = 9;

            // Act
            Lava lava = new Lava(xPos, yPos);

            // Assert
            Assert.AreEqual(lava.xPos, xPos);
            Assert.AreEqual(lava.yPos, yPos);
        }

        [TestMethod]
        public void JsonConvertFacade_CorrectlyCreatesObject()
        {

            // Arrange, Act
            JsonConvertFacade facade = new JsonConvertFacade();

            // Assert
            Assert.IsTrue(facade.settings.TypeNameHandling == TypeNameHandling.Auto);
        }

        [TestMethod]
        public void GridObstacleFactory_CorrectlyCreatesObject()
        {
            // Arrange
            GridObstacleFactory factory = new GridObstacleFactory();

            // Act
            var cell = factory.GenerateCell();

            // Assert
            Assert.IsTrue(cell is ObstacleCell);
        }

        [TestMethod]
        public void GridEnemyFactory_CorrectlyCreatesObject()
        {
            // Arrange
            GridEnemyFactory factory = new GridEnemyFactory();

            // Act
            var cell = factory.GenerateCell();

            // Assert
            Assert.IsTrue(cell is EnemyCell);
        }

        [TestMethod]
        public void Grid_AddsPlayersWhenCountBelowThree()
        {
            // Arrange
            Cell[,] gridState = new Cell[5, 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    gridState[i, j] = new EmptyCell();
                }
            }

            Grid grid = new Grid(5, 5, gridState, 0);

            // Act
            var result = grid.AddPlayer(new Player());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Grid_DoesntAddPlayersWhenCountAboveThree()
        {
            // Arrange
            Cell[,] gridState = new Cell[5, 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    gridState[i, j] = new EmptyCell();
                }
            }

            Grid grid = new Grid(5, 5, gridState, 4);
            // Act
            var result = grid.AddPlayer(new Player());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
