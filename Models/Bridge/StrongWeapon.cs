using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bridge
{
    public class StrongWeapon : Weapon
    {
        private Color color;
        private int damage;
        public StrongWeapon(IWeaponSpawner spawner, Grid grid, GameSession session) : base(spawner, grid, session) 
        { 
            this.color = Color.Gray;
            this.damage = 10;
        }

        public override void SpawnWeapon()
        {
            spawner.Spawn(color, damage, grid, session);
        }
    }
}
