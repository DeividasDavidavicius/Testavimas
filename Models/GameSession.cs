using Models.AbstractFactory;
using Models.Adapter;
using Models.Bridge;
using Models.Cells;
using Models.Decorator;
using Models.Observer;
using Models.Strategy;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class GameSession : ISubject, IGameSession
    {
        public string sessionCode { get; set; }
        public List<Player> players { get; set; }
        public int turnCount { get; set; }
        public int turnCycleCount { get; set; }
        public Grid grid { get; set; }
        public string currentPlayerId { get; set; }
        public string gameEnd { get; set; }

        private IWeaponSpawner safeWeaponSpawner;
        private IWeaponSpawner dangerousWeaponSpawner;
        private Weapon weakWeapon;
        private Weapon strongWeapon;
        private List<IObserver> observers;
        IAudioPlayer audioPlayer = new AudioPlayerAdapter(new AudioPlayer());

        [JsonIgnore]
        private Queue<ICommand> commandQueue { get; set; }

        [JsonIgnore]
        private bool moveAvailable { get; set; }

        public GameSession() { }

        [JsonConstructor]
        public GameSession(string sessionCode, Player[] players, Cell[,] cells, string currentPlayerId)
        {
            this.sessionCode = sessionCode;
            this.players = players.ToList();
            this.turnCount = 0;
            this.turnCycleCount = 0;
            this.grid = new Grid(20, 30, cells, 0);
            this.currentPlayerId = currentPlayerId;
            this.moveAvailable = true;
            safeWeaponSpawner = new SafeWeaponSpawner();
            dangerousWeaponSpawner = new DangerousWeaponSpawner();
            weakWeapon = new WeakWeapon(safeWeaponSpawner, grid, this);
            strongWeapon = new StrongWeapon(dangerousWeaponSpawner, grid, this);
            observers = new List<IObserver>();
            this.gameEnd = null;
        }

        public GameSession(Player host, Cell[,] cells)
        {
            string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            char[] codeChars = new char[2];
            Random random = new Random();
            this.sessionCode = "";
            for (int i = 0; i < codeChars.Length; i++)
            {
                codeChars[i] = chars[random.Next(chars.Length)];
            }
            this.sessionCode = new string(codeChars);
            this.players = new List<Player>
            {
                host
            };
            this.grid = new Grid(20, 30, cells, 0);
            this.grid.AddPlayer(host);
            this.turnCount = 0;
            this.turnCycleCount = 0;
            this.currentPlayerId = host.id;
            this.moveAvailable = true;
            safeWeaponSpawner = new SafeWeaponSpawner();
            dangerousWeaponSpawner = new DangerousWeaponSpawner();
            weakWeapon = new WeakWeapon(safeWeaponSpawner, grid, this);
            strongWeapon = new StrongWeapon(dangerousWeaponSpawner, grid, this);
            observers = new List<IObserver>();
            this.gameEnd = null;
        }

        public void addPlayer(Player player)
        {
            this.players.Add(player);
            this.grid.AddPlayer(player);
        }

        public bool ExecuteTurn(Player player, int x, int y, string id)
        {
            if (!moveAvailable || !players.Contains(player) || x < 0 || y < 0 || !id.Equals(currentPlayerId)) return false;

            int xMovement = Math.Abs(player.xPos - x);
            int yMovement = Math.Abs(player.yPos - y);

            //Self click is prevented
            if (xMovement == yMovement) return false;

            //Diagonal movement is prevented
            if (xMovement > 0 && yMovement > 0) return false;

            //Limit movement range
            //TODO: may add movement range variable
            if (xMovement > 1 || yMovement > 1) return false;

            Cell pressedCell = grid.gridState[x, y];
            List<ICommand> commands = pressedCell.onPress(player, grid, x, y);


            commandQueue = new Queue<ICommand>();
            foreach (ICommand command in commands)
            {
                if (!command.AbleToExecute()) return false;
                commandQueue.Enqueue(command);
                command.Execute();
            }
            audioPlayer.Play("footstep.wav");
            moveAvailable = false;
            return true;
        }


        public bool UndoCommand()
        {
            ICommand command = commandQueue.Dequeue();
            moveAvailable = true;
            return command.Undo();
        }

        public void CloneEnemies()
        {
            Random random = new Random();

            List<EmptyCell> emptyCells = new List<EmptyCell>();
            List<EnemyCell> enemyCells = new List<EnemyCell>();
            List<int> emptyCellsX = new List<int>();
            List<int> emptyCellsY = new List<int>();

            for (int i = 0; i < grid.gridSize; i++)
            {
                for (int j = 0; j < grid.gridSize; j++)
                {
                    Cell cell = grid.gridState[i, j];
                    if (cell.GetType() == typeof(EmptyCell))
                    {
                        emptyCellsX.Add(i);
                        emptyCellsY.Add(j);
                        emptyCells.Add((EmptyCell)cell);
                    }

                    if (cell is EnemyCell)
                    {
                        enemyCells.Add((EnemyCell)cell);
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                int randomEmptyCellNumber = random.Next(0, emptyCells.Count);
                int randomEnemyCellNumber = random.Next(0, enemyCells.Count);

                EnemyCell enemy = enemyCells[randomEnemyCellNumber].Clone();
                this.grid.gridState[emptyCellsX[randomEmptyCellNumber], emptyCellsY[randomEmptyCellNumber]] = enemy;
            }

            turnCycleCount = 0;
        }

        public void SpawnWeapons()
        {
            weakWeapon.SpawnWeapon();
            strongWeapon.SpawnWeapon();
        }

        public void ExecuteEnemies()
        {
            Random random = new Random();

            List<EnemyCell> enemyCells = new List<EnemyCell>();
            List<int> enemyCellsX = new List<int>();
            List<int> enemyCellsY = new List<int>();

            for (int i = 0; i < grid.gridSize; i++)
            {
                for (int j = 0; j < grid.gridSize; j++)
                {
                    Cell cell = grid.gridState[i, j];

                    if (cell is EnemyCell)
                    {
                        enemyCells.Add((EnemyCell)cell);
                        enemyCellsX.Add(i);
                        enemyCellsY.Add(j);
                    }
                }
            }

            for(int i = 0; i < enemyCells.Count; i++)
            {
                EnemyCell cell = enemyCells[i];
                int strategyGenerator = random.Next(0, 100);

                if (cell.freezeTime > 0)
                {
                    cell.SetStrategy(new MeltStrategy());
                }
                else
                {
                    cell.color = cell.otherColor;
                    if (strategyGenerator < 10)
                    {
                        cell.SetStrategy(new FreezeStrategy());
                    }
                    else if (strategyGenerator < 20)
                    {
                        cell.SetStrategy(new HealStrategy());
                    }
                    else if (strategyGenerator < 40)
                    {
                        cell.SetStrategy(new MoveStrategy());
                    }
                    else
                    {
                        cell.SetStrategy(new DamagePlayersStrategy());
                    }
                }
            }

            for (int i = 0; i < enemyCells.Count; i++)
            {
                EnemyCell cell = enemyCells[i];
                cell.ExecuteStrategy(grid, enemyCellsX[i], enemyCellsY[i]);
            }
        }

        public bool SetNewTurn(string id)
        {
            if (!id.Equals(currentPlayerId))
            {
                return false;
            }

            turnCount++;
            turnCycleCount++;
            currentPlayerId = players[turnCount % players.Count].id;
            moveAvailable = true;

            ExecuteEnemies();
            SpawnWeapons();
            Notify();
            CloneEnemies();
            return true;
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach(var observer in observers)
            {
                observer.Update();
            }
        }
    }
}
