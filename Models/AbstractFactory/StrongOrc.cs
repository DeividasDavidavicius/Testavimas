using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class StrongOrc : AbstractStrongEnemy
    {
        public StrongOrc() : base(Color.Green, 80, 20)
        {

        }
    }
}
