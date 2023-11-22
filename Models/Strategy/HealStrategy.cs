using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Strategy
{
    class HealStrategy : IStrategy
    {
        public void ExecuteAction(Grid grid, EnemyCell cell, int x, int y)
        {
            cell.healthPoints += cell.healAmount;
        }
    }
}
