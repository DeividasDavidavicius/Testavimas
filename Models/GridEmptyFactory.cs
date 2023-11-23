using Models;
using Models.Cells;
using System.Diagnostics.CodeAnalysis;

namespace Models
{
    [ExcludeFromCodeCoverage]
    public class GridEmptyFactory : GridFactory
    {
        public override Cell GenerateCell()
        {
            return new EmptyCell();
        }
    }
}
