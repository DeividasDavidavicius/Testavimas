using Models.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cells
{
    [ExcludeFromCodeCoverage]
    public class EntityCell : Cell
    {
        public int healthPoints;
        public int damagePoints;
        public int armourPoints;

        public EntityCell(Color color, int healthPoints, int damagePoints) : base(color)
        {
            this.healthPoints = healthPoints;
            this.damagePoints = damagePoints;
            this.armourPoints = 0;
        }

        public int DamageEntity(int dmg)
        {
            if (dmg < armourPoints) return healthPoints;
            return healthPoints -= (dmg - armourPoints);
        }

        public override List<ICommand> onPress(Player player, Grid grid, int x, int y)
        {
            List<ICommand> commands = new List<ICommand>() 
            {
                new InflictDamageCommand(player, grid, new Vector2(x, y), this)
            };
            return commands;
        }
    }
}
