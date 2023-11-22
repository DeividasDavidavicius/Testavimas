using System.Text.Json.Serialization;
using Models.Cells;

namespace Models
{
    public class Grid
    {
        public int playerCount { get; set; }
        public int gridSize { get; set; }
        public int cellSize { get; set; }
        public Cell[,] gridState { get; set; }

        [JsonConstructor]
        public Grid(int gridSize, int cellSize, Cell[,] gridState, int playerCount)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.gridState = gridState;
            this.playerCount = playerCount;
        }

        public bool AddPlayer(Player player)
        {
            if (playerCount > 3)
            {
                return false;
            }

            Vector2 playerStartCoords = new Vector2(19 * (playerCount % 2), 19 * Math.Min(1, Math.Max(0, playerCount - 1)));
            playerCount++;
            player.xPos = playerStartCoords.getX();
            player.yPos = playerStartCoords.getY();
            gridState[playerStartCoords.getX(), playerStartCoords.getY()] = player;

            return true;
        }
    }
}
