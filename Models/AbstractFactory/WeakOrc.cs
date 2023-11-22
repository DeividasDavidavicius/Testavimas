using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class WeakOrc : AbstractWeakEnemy
    {
        public WeakOrc() : base(Color.GreenYellow, 20, 5)
        {

        }
    }
}
