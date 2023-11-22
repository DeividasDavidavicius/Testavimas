using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public abstract class EnemyFactory : GridFactory
    {
        public abstract AbstractWeakEnemy CreateWeakEnemy();
        public abstract AbstractMediumEnemy CreateMediumEnemy();
        public abstract AbstractStrongEnemy CreateStrongEnemy();
    }
}
