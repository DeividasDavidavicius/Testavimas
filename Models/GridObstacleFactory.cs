using Models;
using Models.Cells;

namespace Models
{
    public class GridObstacleFactory : GridFactory
    {
        public GridObstacleFactory() { }

        public override Cell GenerateCell()
        {
            return new ObstacleCell();
        }
    }
}
