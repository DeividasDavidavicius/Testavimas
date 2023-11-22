using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //TODO: SHOULD BE OBSOLETE
    public class Mountain
    {
        public int xPos { get; set; }
        public int yPos { get; set; }

        public Mountain(int x, int y)
        {
            this.xPos = x;
            this.yPos = y;
        }

        public Mountain()
        {
            this.xPos = -1;
            this.yPos = -1;
        }
    }
}
