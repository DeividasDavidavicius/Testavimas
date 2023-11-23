using Microsoft.AspNetCore.SignalR;
using Models.Decorator;
using Models;
using Moq;
using ProjServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Strategy;
using Models.Cells;

namespace Tests
{

    [TestClass]
    public class StrategyTests
    {
        [TestMethod]
        public void DamageStrategy_DamagesPlayers()
        {
            // Arrange
            DamagePlayersStrategy strategy = new DamagePlayersStrategy();
            Cell[,] gridState = new Cell[5, 5];
            Grid grid = new Grid(5, 5, gridState, 2);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Player player = new Player();
                    player.healthPoints = 100;
                    gridState[i, j] = player;
                }
            }
            EnemyCell enemyCell = new EnemyCell();
            enemyCell.damagePoints = 25;
            gridState[2, 2] = enemyCell;

            // Act
            strategy.ExecuteAction(grid, enemyCell, 2, 2);

            // Assert
            Player damagedPlayer = (Player)grid.gridState[2, 1];
            Assert.AreEqual(damagedPlayer.healthPoints, 75);
        }

        [TestMethod]
        public void FreezeStrategy_SetsFreeze()
        {
            // Arrange
            FreezeStrategy strategy = new FreezeStrategy();
            Grid grid = new Grid(5, 5, null, 2);
            EnemyCell cell = new EnemyCell();

            // Act
            strategy.ExecuteAction(grid, cell, 0, 0);

            // Assert
            Assert.AreEqual(cell.freezeTime, 1);
        }

        [TestMethod]
        public void HealStrategy_HealsEnemy()
        {
            // Arrange
            HealStrategy strategy = new HealStrategy();
            Grid grid = new Grid(5, 5, null, 2);
            EnemyCell cell = new EnemyCell();
            cell.healAmount = 10;
            cell.healthPoints = 100;

            // Act
            strategy.ExecuteAction(grid, cell, 0, 0);

            // Assert
            Assert.AreEqual(cell.healthPoints, 110);
        }

        [TestMethod]
        public void MeltStrategy_ReducesFreezeTime()
        {
            // Arrange
            MeltStrategy strategy = new MeltStrategy();
            Grid grid = new Grid(5, 5, null, 2);
            EnemyCell cell = new EnemyCell();
            cell.freezeTime = 10;

            // Act
            strategy.ExecuteAction(grid, cell, 0, 0);

            // Assert
            Assert.AreEqual(cell.freezeTime, 9);
        }

        [TestMethod]
        public void MoveStrategy_MovesEnemy()
        {
            // Arrange
            MoveStrategy strategy = new MoveStrategy();
            Cell[,] gridState = new Cell[5,5];
            Grid grid = new Grid(5, 5, gridState, 2);

            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    gridState[i, j] = new EmptyCell();
                }
            }
            EnemyCell enemyCell = new EnemyCell();
            gridState[2, 2] = enemyCell;

            // Act
            strategy.ExecuteAction(grid, enemyCell, 2, 2);

            // Assert
            Assert.IsTrue(grid.gridState[2,2].GetType() == typeof(EmptyCell));
        }

        [TestMethod]
        public void MoveStrategy_MoveEnemyFail()
        {
            // Arrange
            MoveStrategy strategy = new MoveStrategy();
            Cell[,] gridState = new Cell[1, 1];
            Grid grid = new Grid(1, 1, gridState, 2);

            EnemyCell enemyCell = new EnemyCell();
            gridState[0, 0] = enemyCell;

            // Act
            strategy.ExecuteAction(grid, enemyCell, 0, 0);

            // Assert
            Assert.IsTrue(grid.gridState[0, 0].GetType() == typeof(EnemyCell));
        }
    }
}
