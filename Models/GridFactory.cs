using Models.Cells;

namespace Models
{
    public abstract class GridFactory
    {
        public Cell CreateCell()
        {
            Cell cell = GenerateCell();
            return cell;
        }

        public abstract Cell GenerateCell();
    }
}
