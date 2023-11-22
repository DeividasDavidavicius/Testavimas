using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models.Commands
{
    internal class MoveCommand : ICommand
    {
        private Player player;
        private Grid grid;
        private Vector2 moveCoords;
        private Vector2 lastCoords;
        private Cell pressedCell;

        public MoveCommand(Player player, Grid grid, Vector2 coords)
        {
            this.player = player;
            this.grid = grid;
            this.moveCoords = coords;
            this.lastCoords = new Vector2(player.xPos, player.yPos);
            this.pressedCell = grid.gridState[moveCoords.getX(), moveCoords.getY()];
        }

        public bool AbleToExecute()
        {
            return true;
        }

        public string CommandName()
        {
            return string.Format("Move to {0}, {1}", moveCoords.getX(), moveCoords.getY());
        }

        public bool Execute()
        {
            grid.gridState[player.xPos, player.yPos] = new EmptyCell();
            grid.gridState[moveCoords.getX(), moveCoords.getY()] = player;
            player.xPos = moveCoords.getX();
            player.yPos = moveCoords.getY();
            return true;
        }

        public bool Undo()
        {
            grid.gridState[lastCoords.getX(), lastCoords.getY()] = player;
            grid.gridState[moveCoords.getX(), moveCoords.getY()] = pressedCell;
            player.xPos = lastCoords.getX();
            player.yPos = lastCoords.getY();
            return true;
        }
    }
}
