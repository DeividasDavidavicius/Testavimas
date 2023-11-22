using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Models.Decorator
{
    public class ArmorFrame : Armor
    {
        public ArmorFrame(int armorCount) {
            this.armorCount = armorCount;
        }
        public override int GiveArmor()
        {
            return armorCount;
        }
    }
}
