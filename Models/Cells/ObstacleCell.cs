using Models.Commands;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Models.Cells
{
    [ExcludeFromCodeCoverage]
    public class ObstacleCell : Cell
    {
        public ObstacleCell() : base(Color.Black)
        {

        }

        public override List<ICommand> onPress(Player player, Grid grid, int x, int y)
        {
            return new List<ICommand> { new DenyCommand() };
        }
    }
}
