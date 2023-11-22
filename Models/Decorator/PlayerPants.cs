using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Decorator
{
    public class PlayerPants : AirDropDecorator
    {
        public PlayerPants(Armor armor) : base(armor)
        {
            armorCount = _armor.armorCount + 100;
        }
        public override int GiveArmor()
        {
            return armorCount;
        }
    }
}
