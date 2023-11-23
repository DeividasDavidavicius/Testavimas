using Models;
using Models.AbstractFactory;
using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class Integration_GameSession
    {
        [TestMethod]
        public void SetNewTurns_ExecutesTurnsSuccessfully()
        {
            // Arrange
            Player player = new Player();
            player.id = "0";
            Player[] players = new Player[] { player };
            Cell[,] cells = new Cell[20, 20];
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 20; j++)
                {
                    cells[i, j] = new EmptyCell();
                }
                cells[i, i] = new WeakOrc();
            }
            GameSession gameSession = new GameSession("0", players, cells, "0");

            // Act
            var result = gameSession.SetNewTurn("0");


            // Assert
            Assert.IsTrue(result);
        }
    }
}
