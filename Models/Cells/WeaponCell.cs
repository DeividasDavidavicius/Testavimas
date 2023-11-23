using Models.Commands;
using Models.Observer;
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
    public class WeaponCell : Cell, IObserver
    {
        public int damagePoints;
        private GameSession session;

        public WeaponCell(Color color, int damagePoints, GameSession session) : base(color)
        {
            this.damagePoints = damagePoints;
            this.session = session;
        }

        public override List<ICommand> onPress(Player player, Grid grid, int x, int y)
        {
            List<ICommand> commands = new List<ICommand>()
            {
                new EquipWeaponCommand(player, grid, new Vector2(x, y), this, session)
            };
            return commands;
        }

        public void Update()
        {
            this.damagePoints += 1;
        }
    }
}
