using Models.Cells;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Commands
{
    [ExcludeFromCodeCoverage]

    internal class TakeDamageCommand : ICommand
    {
        private Player player;
        private EntityCell enemy;

        public TakeDamageCommand(Player player, EntityCell enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public bool AbleToExecute()
        {
            return true;
        }

        public string CommandName()
        {
            return "Take damage";
        }

        public bool Execute()
        {
            player.DamageEntity(enemy.damagePoints);
            //TODO: kill player
            return true;
        }

        public bool Undo()
        {
            player.healthPoints += enemy.damagePoints;
            return true;
        }
    }
}
