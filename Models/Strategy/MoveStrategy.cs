using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Strategy
{
    public class MoveStrategy : IStrategy
    {
        public void ExecuteAction(Grid grid, EnemyCell cell, int x, int y)
        {
            int gridSize = grid.gridSize;
            List<EmptyCell> emptyCells = new List<EmptyCell>();
            List<int> emptyCellsX = new List<int>();
            List<int> emptyCellsY = new List<int>();

            if (x + 1 < gridSize)
            {
                if (grid.gridState[x + 1, y] is EmptyCell)
                {
                    emptyCells.Add((EmptyCell)grid.gridState[x + 1, y]);
                    emptyCellsX.Add(x + 1);
                    emptyCellsY.Add(y);
                }
            }

            if (x - 1 >= 0)
            {
                if (grid.gridState[x - 1, y] is EmptyCell)
                {
                    emptyCells.Add((EmptyCell)grid.gridState[x - 1, y]);
                    emptyCellsX.Add(x - 1);
                    emptyCellsY.Add(y);
                }
            }

            if (y + 1 < gridSize)
            {
                if (grid.gridState[x, y + 1] is EmptyCell)
                {
                    emptyCells.Add((EmptyCell)grid.gridState[x, y + 1]);
                    emptyCellsX.Add(x);
                    emptyCellsY.Add(y + 1);
                }
            }

            if (y - 1 >= 0)
            {
                if (grid.gridState[x, y - 1] is EmptyCell)
                {
                    emptyCells.Add((EmptyCell)grid.gridState[x, y - 1]);
                    emptyCellsX.Add(x);
                    emptyCellsY.Add(y - 1);
                }
            }

            if(emptyCells.Count <= 0)
            {
                return;
            }

            Random random = new Random();

            int randomEmptyCellNumber = random.Next(0, emptyCells.Count);

           grid.gridState[x, y] = new EmptyCell();
           grid.gridState[emptyCellsX[randomEmptyCellNumber], emptyCellsY[randomEmptyCellNumber]] = cell;

        }
    }
}
