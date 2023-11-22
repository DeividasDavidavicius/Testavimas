using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public abstract class AbstractWeakEnemy : EnemyCell
    {
        public AbstractWeakEnemy(Color color, int health, int damage) : base(color, health, damage)
        {
        }
    }
}
