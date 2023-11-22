using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Strategy
{
    public interface IStrategy
    {
        void ExecuteAction(Grid grid, EnemyCell cell, int x, int y);
    }
}
