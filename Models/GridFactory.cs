using Models.Cells;
using System.Diagnostics.CodeAnalysis;

namespace Models
{
    [ExcludeFromCodeCoverage]
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
