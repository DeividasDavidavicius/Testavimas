using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Strategy
{
    class DamagePlayersStrategy : IStrategy
    {
        public void ExecuteAction(Grid grid, EnemyCell cell, int x, int y)
        {
            int gridSize = grid.gridSize;

            if(x + 1 < gridSize)
            {
                if(grid.gridState[x + 1, y] is Player)
                {
                    Player player = (Player)grid.gridState[x + 1, y];
                    player.healthPoints -= cell.damagePoints;
                }
            }

            if(x - 1 >= 0)
            {
                if (grid.gridState[x - 1, y] is Player)
                {
                    Player player = (Player)grid.gridState[x - 1, y];
                    player.healthPoints -= cell.damagePoints;
                }
            }

            if(y + 1 < gridSize)
            {
                if (grid.gridState[x, y + 1] is Player)
                {
                    Player player = (Player)grid.gridState[x, y + 1];
                    player.healthPoints -= cell.damagePoints;
                }
            }

            if(y - 1 >= 0)
            {
                if (grid.gridState[x, y - 1] is Player)
                {
                    Player player = (Player)grid.gridState[x, y - 1];
                    player.healthPoints -= cell.damagePoints;
                }
            }
        }
    }
}
