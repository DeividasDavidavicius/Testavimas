using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Builder
{
    public interface IBuilder
    {
        public void BuildHealth();
        public void BuildDamage();
        public void BuildHealing();
        public void BuildFreeze();
        public void BuildArmour();
        public void BuildClass(string className);
    }
}
