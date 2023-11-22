using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bridge
{
    public interface IWeaponSpawner
    {
        void Spawn(Color color, int damage, Grid grid, GameSession session);
    }
}
