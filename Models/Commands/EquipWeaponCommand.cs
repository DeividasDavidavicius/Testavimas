using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Commands
{
    internal class EquipWeaponCommand : ICommand
    {
        private Player player;
        private Grid grid;
        private Vector2 coords;
        private Vector2 lastCoords;
        private WeaponCell weapon;
        private GameSession session;

        public EquipWeaponCommand(Player player, Grid grid, Vector2 coords, WeaponCell weapon, GameSession session)
        {
            this.player = player;
            this.grid = grid;
            this.weapon = weapon;
            this.coords = coords;
            this.lastCoords = new Vector2(player.xPos, player.yPos);
            this.session = session;
        }

        public bool AbleToExecute()
        {
            return true;
        }

        public string CommandName()
        {
            return string.Format("Equip weapon on {0}, {1}", coords.getX(), coords.getY());
        }

        public bool Execute()
        {
            grid.gridState[player.xPos, player.yPos] = new EmptyCell();
            grid.gridState[coords.getX(), coords.getY()] = player;
            player.xPos = coords.getX();
            player.yPos = coords.getY();
            player.damagePoints += weapon.damagePoints;
            session.Detach(weapon);

            return true;
        }

        public bool Undo()
        {
            grid.gridState[lastCoords.getX(), lastCoords.getY()] = player;
            grid.gridState[coords.getX(), coords.getY()] = weapon;
            player.xPos = lastCoords.getX();
            player.yPos = lastCoords.getY();
            player.damagePoints -= weapon.damagePoints;
            session.Attach(weapon);

            return true;
        }
    }
}
