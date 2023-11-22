using Models.Builder;
using Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AbstractFactory
{
    public class OrcFactory : EnemyFactory
    {
        private WeakOrc weakOrc;
        private MediumOrc mediumOrc;

        public OrcFactory() 
        { 
            weakOrc = new WeakOrc();
            mediumOrc = new MediumOrc();
        }

        public override Cell GenerateCell()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            if (randomNumber <= 40)
            {
                return CreateStrongEnemy();
            }
            else if (randomNumber <= 70)
            {
                return CreateMediumEnemy();
            }

            return CreateWeakEnemy();
        }
        public override AbstractWeakEnemy CreateWeakEnemy()
        {
            return (WeakOrc)weakOrc.DeepClone();
        }

        public override AbstractMediumEnemy CreateMediumEnemy()
        {
            return (MediumOrc)mediumOrc.DeepClone();
        }

        public override AbstractStrongEnemy CreateStrongEnemy()
        {
            Director director = new Director();
            StrongOrcBuilder builder = new StrongOrcBuilder();

            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            if(randomNumber < 50)
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
