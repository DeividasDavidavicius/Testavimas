using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Commands
{
    internal class InflictDamageCommand : ICommand
    {
        private Player player;
        private Grid grid;
        private Vector2 coords;
        private Vector2 lastCoords;
        private EntityCell entity;
        private int remainingHp;

        public InflictDamageCommand(Player player, Grid grid, Vector2 coords, EntityCell entity)
        {
            this.player = player;
            this.grid = grid;
            this.entity = entity;
            this.coords = coords;
            this.lastCoords = new Vector2(player.xPos, player.yPos);
        }

        public bool AbleToExecute()
        {
            return true;
        }

        public string CommandName()
        {
            return string.Format("Inflict damage on {0}, {1}", coords.getX(), coords.getY());
        }

        public bool Execute()
        {
            remainingHp = entity.DamageEntity(player.damagePoints);

            if (remainingHp <= 0)
            {
                grid.gridState[player.xPos, player.yPos] = new EmptyCell();
                grid.gridState[coords.getX(), coords.getY()] = player;
                player.xPos = coords.getX();
                player.yPos = coords.getY();
            }
            return true;
        }

        public bool Undo()
        {
            entity.healthPoints += player.damagePoints - entity.armourPoints;
            if (remainingHp <= 0)
            {
                grid.gridState[lastCoords.getX(), lastCoords.getY()] = player;
                grid.gridState[coords.getX(), coords.getY()] = entity;
                player.xPos = lastCoords.getX();
                player.yPos = lastCoords.getY();
            }

            return true;
        }
    }
}
