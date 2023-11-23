using Models.Commands;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Models.Cells
{
    [ExcludeFromCodeCoverage]
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
