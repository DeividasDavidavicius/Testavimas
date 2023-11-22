using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Decorator
{
    public abstract class Armor
    {
        public int armorCount { get; set; }
        public abstract int GiveArmor();
    }
}
