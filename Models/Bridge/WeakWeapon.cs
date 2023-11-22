using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bridge
{
    public class WeakWeapon : Weapon
    {
        private Color color;
        private int damage;
        public WeakWeapon(IWeaponSpawner spawner, Grid grid, GameSession session) : base(spawner, grid, session)
        {
            this.color = Color.LightGray;
            this.damage = 5;
        }

        public override void SpawnWeapon()
        {
            spawner.Spawn(color, damage, grid, session);
        }
    }
}
