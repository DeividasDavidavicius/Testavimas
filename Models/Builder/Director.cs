using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Builder
{
    class Director
    {
        public void ConstructAttacker(IBuilder builder)
        {
            builder.BuildClass("Attack");
            builder.BuildDamage();
            builder.BuildFreeze();
            builder.BuildHealth();
        }

        public void ConstructTank(IBuilder builder)
        {
            builder.BuildClass("Tank");
            builder.BuildArmour();
            builder.BuildFreeze();
            builder.BuildHealth();
            builder.BuildHealing();
        }
    }
}
