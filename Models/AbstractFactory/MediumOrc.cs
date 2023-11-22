using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class MediumOrc : AbstractMediumEnemy
    {
        public MediumOrc() : base(Color.LimeGreen, 40, 10)
        {

        }
    }
}
