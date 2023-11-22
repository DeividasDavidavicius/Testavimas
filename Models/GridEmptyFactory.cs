using Models;
using Models.Cells;

namespace Models
{
    public class GridEmptyFactory : GridFactory
    {
        public override Cell GenerateCell()
        {
            return new EmptyCell();
        }
    }
}
