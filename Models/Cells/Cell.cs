using System.Drawing;

namespace Models.Cells
{
    public abstract class Cell
    {
        public Color color { get; set; }

        public Cell(Color color)
        {
            this.color = color;
        }

        public abstract List<ICommand> onPress(Player player, Grid grid, int x, int y);
    }
}
