using Models.AbstractFactory;
using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Builder
{
    public class StrongDemonBuilder : IBuilder
    {
        private StrongDemon enemy = new StrongDemon();

        public StrongDemonBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.enemy = new StrongDemon();
        }

        public void BuildArmour()
        {
            this.enemy.armourPoints = 20;
        }

        public void BuildDamage()
        {
            this.enemy.damagePoints = 50;
        }

        public void BuildFreeze()
        {
            this.enemy.freezeTime = 1;
        }

        public void BuildHealing()
        {
            this.enemy.healAmount = 30;
        }

        public void BuildHealth()
        {
            this.enemy.healthPoints = 100;
        }

        public void BuildClass(string className)
        {
            this.enemy.className = className;
        }

        public StrongDemon GetEnemy()
        {
            return enemy;
        }
    }
}
