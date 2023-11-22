using Models;
using Models.Cells;
using System.Drawing;

namespace Models
{
    public class GridEnemyFactory : GridFactory
    {

        // SITA KLASE NUBRAIZIUS ISTRINT REIKES
        public GridEnemyFactory() { }

        public override Cell GenerateCell()
        {
            return new EnemyCell(Color.Brown, 1, 1);
        }
    }
}
