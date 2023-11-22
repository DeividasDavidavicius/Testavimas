using Models.AbstractFactory;
using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Builder
{
    public class StrongOrcBuilder : IBuilder
    {
        private StrongOrc enemy = new StrongOrc();

        public StrongOrcBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.enemy = new StrongOrc();
        }

        public void BuildArmour()
        {
            this.enemy.armourPoints = 30;
        }

        public void BuildDamage()
        {
            this.enemy.damagePoints = 60;
        }

        public void BuildFreeze()
        {
            this.enemy.freezeTime = 3;
        }

        public void BuildHealing()
        {
            this.enemy.healAmount = 15;
        }

        public void BuildHealth()
        {
            this.enemy.healthPoints = 150;
        }

        public void BuildClass(string className)
        {
            this.enemy.className = className;
        }

        public StrongOrc GetEnemy()
        {
            return enemy;
        }
    }
}
