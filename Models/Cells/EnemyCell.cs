using Models.Strategy;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Models.Cells
{
    [ExcludeFromCodeCoverage]
    public class EnemyCell : EntityCell, IPrototype<EnemyCell>
    {
        private IStrategy _strategy;
        public Color otherColor;

        public int freezeTime { get; set; }
        public int healAmount { get; set; }
        public string className { get; set; }

        public EnemyCell(Color color, int health, int damage) : base(color, health, damage)
        {
            this._strategy = new MeltStrategy();
            this.freezeTime = 0;
            this.otherColor = color;
            this.healAmount = 20;
            this.className = null;
        }

        public EnemyCell() : base(Color.Black, 10, 10)
        {
            this._strategy = new MeltStrategy();

        }

        public EnemyCell Clone()
        {
            //Console.WriteLine("Shallow Copy, A" + this.color.GetHashCode());
            EnemyCell copy = (EnemyCell)this.MemberwiseClone();
            //Console.WriteLine("Shallow Copy, B" + copy.color.GetHashCode());
            return copy;
        }

        public EnemyCell DeepClone()
        {
            Console.WriteLine("Deep Copy, A" + this.color.GetHashCode());
            EnemyCell copy = Clone();
            copy.color = Color.FromArgb(this.color.A, this.color.R, this.color.G, this.color.B);
            copy.healthPoints = this.healthPoints;
            copy.damagePoints = this.damagePoints;
            Console.WriteLine("Deep Copy, B" + copy.color.GetHashCode());
            Console.WriteLine(this.color.GetHashCode());
            return copy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void ExecuteStrategy(Grid grid, int x, int y)
        {
            this._strategy.ExecuteAction(grid, this, x, y);
        }
    }
}
