using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Strategy
{
    class FreezeStrategy : IStrategy
    {
        public void ExecuteAction(Grid grid, EnemyCell cell, int x, int y)
        {
            cell.freezeTime = 1;
            cell.color = Color.LightBlue;
        }
    }
}
