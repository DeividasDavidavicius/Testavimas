using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bridge
{
    public class DangerousWeaponSpawner : IWeaponSpawner
    {
        public void Spawn(Color color, int damage, Grid grid, GameSession session)
        {
            Random random = new Random();

            List<EmptyCell> emptyCells = new List<EmptyCell>();
            List<int> emptyCellsX = new List<int>();
            List<int> emptyCellsY = new List<int>();

            for (int i = 0; i < grid.gridSize; i++)
            {
                for (int j = 0; j < grid.gridSize; j++)
                {
                    Cell cell = grid.gridState[i, j];
                    Cell leftCell = grid.gridState[i, j];
                    Cell upCell = grid.gridState[i, j];
                    Cell rightCell = grid.gridState[i, j];
                    Cell downCell = grid.gridState[i, j];

                    if (i > 0)
                    {
                        leftCell = grid.gridState[i - 1, j];
                    }

                    if (j > 0)
                    {
                        upCell = grid.gridState[i, j - 1];
                    }

                    if (i < grid.gridSize - 1)
                    {
                        rightCell = grid.gridState[i + 1, j];
                    }

                    if (j < grid.gridSize - 1)
                    {
                        downCell = grid.gridState[i, j + 1];
                    }

                    if (cell.GetType() == typeof(EmptyCell) && (leftCell is EnemyCell || upCell is EnemyCell || rightCell is EnemyCell || downCell is EnemyCell))
                    {
                        emptyCellsX.Add(i);
                        emptyCellsY.Add(j);
                        emptyCells.Add((EmptyCell)cell);
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                int randomCellNumber = random.Next(0, emptyCells.Count);

                WeaponCell weapon = new WeaponCell(color, damage, session);
                grid.gridState[emptyCellsX[randomCellNumber], emptyCellsY[randomCellNumber]] = weapon;

                session.Attach(weapon);
            }
        }
    }
}
