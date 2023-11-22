using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class Vector2
    {
        private int x;
        private int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void setX(int x)
        {
            this.x = x;
        }

        public void sety(int y)
        {
            this.y = y;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

    }
}
