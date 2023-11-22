using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Decorator
{
    public class PlayerGlove : AirDropDecorator
    {

        public PlayerGlove(Armor armor) : base(armor)
        {
            armorCount = _armor.armorCount + 50;
        }
        public override int GiveArmor()
        {
            return armorCount;
        }
    }
}
