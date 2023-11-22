using Models.Commands;
using System.Drawing;

namespace Models.Cells
{
    public class EmptyCell : Cell
    {
        public EmptyCell() : base(Color.White)
        {
            
        }

        public override List<ICommand> onPress(Player player, Grid grid, int x, int y)
        {
            return new List<ICommand> { new MoveCommand(player, grid, new Vector2(x, y)) };
        }
    }
}
