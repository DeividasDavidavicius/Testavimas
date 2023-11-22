using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Decorator
{
    public class AirDropDecorator : Armor
    {
        protected Armor _armor;

        public AirDropDecorator(Armor armor)
        {
            _armor = armor;
        }
        public override int GiveArmor()
        {
            return _armor.armorCount;
        }
    }
}
