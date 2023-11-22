using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class WeakDemon : AbstractWeakEnemy
    {
        public WeakDemon() : base(Color.IndianRed, 30, 8)
        {

        }
    }
}
