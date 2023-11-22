using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class StrongDemon : AbstractStrongEnemy
    {
        public StrongDemon() : base(Color.DarkRed, 60, 30)
        {

        }
    }
}
