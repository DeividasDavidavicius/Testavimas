using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bridge
{
    public abstract class Weapon
    {
        protected IWeaponSpawner spawner;
        protected Grid grid;
        protected GameSession session;
        protected Weapon(IWeaponSpawner spawner, Grid grid, GameSession session)
        {
            this.spawner = spawner;
            this.grid = grid;
            this.session = session;
        }

        public abstract void SpawnWeapon();
    }
}
