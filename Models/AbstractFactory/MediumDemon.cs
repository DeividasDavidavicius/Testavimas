using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class MediumDemon : AbstractMediumEnemy
    {
        public MediumDemon() : base(Color.Red, 60, 15)
        {

        }
    }
}
