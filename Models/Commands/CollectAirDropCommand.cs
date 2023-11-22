using Models.Bridge;
using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Commands
{
    internal class CollectAirDropCommand : ICommand
    {
        private Player player;
        private Grid grid;
        private Vector2 coords;
        private Vector2 lastCoords;
        private AirdropCell airdrop;

        public CollectAirDropCommand(Player player, Grid grid, Vector2 coords, AirdropCell airdrop)
        {
            this.player = player;
            this.grid = grid;
            this.coords = coords;
            this.lastCoords = new Vector2(player.xPos, player.yPos);
            this.airdrop = airdrop;
        }

        public bool AbleToExecute()
        {
            return true;
        }

        public string CommandName()
        {
            return string.Format("Air drop collected on {0}, {1}", coords.getX(), coords.getY());
        }

        public bool Execute()
        {
            grid.gridState[player.xPos, player.yPos] = new EmptyCell();
            grid.gridState[coords.getX(), coords.getY()] = player;
            player.xPos = coords.getX();
            player.yPos = coords.getY();
            player.healthPoints += airdrop.armorPoints;
            return true;
        }

        public bool Undo()
        {
            grid.gridState[lastCoords.getX(), lastCoords.getY()] = player;
            grid.gridState[coords.getX(), coords.getY()] = airdrop;
            player.xPos = lastCoords.getX();
            player.yPos = lastCoords.getY();
            player.healthPoints -= airdrop.armorPoints;
            return true;
        }
    }
}
