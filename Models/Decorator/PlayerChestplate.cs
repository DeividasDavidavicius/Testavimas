using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Decorator
{
    public class PlayerChestplate : AirDropDecorator
    {
        public PlayerChestplate(Armor armor) : base(armor)
        {
            armorCount = _armor.armorCount + 200;
        }
        public override int GiveArmor()
        {
            return armorCount;
        }
    }
}
