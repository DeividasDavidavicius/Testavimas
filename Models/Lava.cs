using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //TODO: SHOULD BE OBSOLETE
    public class Lava
    {
        public int xPos { get; set; }
        public int yPos { get; set; }
        public int damage { get; set; }

        public Lava(int x, int y)
        {
            this.xPos = x;
            this.yPos = y;
            this.damage = 10;
        }

        public Lava()
        {
            this.xPos = -1;
            this.yPos = -1;
            this.damage = 10;
        }
    }
}
