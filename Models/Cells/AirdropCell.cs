using Models.Commands;
using Models.Observer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cells
{
    public class AirdropCell : Cell
    {
        public int armorPoints;

        public AirdropCell(int armorpoints) : base(Color.Violet)
        {
            this.armorPoints = armorpoints;
        }
        public override List<ICommand> onPress(Player player, Grid grid, int x, int y)
        {
            return new List<ICommand> { new CollectAirDropCommand(player, grid, new Vector2(x, y), this) };
        }

    }
}
