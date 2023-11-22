using Models.Builder;
using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class DemonFactory : EnemyFactory
    {
        private WeakDemon weakDemon;
        private MediumDemon mediumDemon;

        public DemonFactory() 
        { 
            weakDemon = new WeakDemon();
            mediumDemon = new MediumDemon();
        }

        public override Cell GenerateCell()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            if(randomNumber <= 40)
            {
                return CreateStrongEnemy();
            }
            else if(randomNumber <= 70)
            {
                return CreateMediumEnemy();
            }

            return CreateWeakEnemy();
        }

        public override AbstractWeakEnemy CreateWeakEnemy()
        {
            return (WeakDemon)weakDemon.DeepClone();
        }

        public override AbstractMediumEnemy CreateMediumEnemy()
        {
            return (MediumDemon)mediumDemon.DeepClone();
        }

        public override AbstractStrongEnemy CreateStrongEnemy()
        {
            Director director = new Director();
            StrongDemonBuilder builder = new StrongDemonBuilder();

            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            if (randomNumber < 50)
            {
                director.ConstructAttacker(builder);
                return builder.GetEnemy();
            }
            else
            {
                director.ConstructTank(builder);
                return builder.GetEnemy();
            }
        }
    }
}
