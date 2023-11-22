using Models.Observer;

namespace Models
{
    public interface IGameSession
    {
        string currentPlayerId { get; set; }
        string gameEnd { get; set; }
        Grid grid { get; set; }
        List<Player> players { get; set; }
        string sessionCode { get; set; }
        int turnCycleCount { get; set; }
        int turnCount { get; set; }

        void addPlayer(Player player);
        void Attach(IObserver observer);
        void CloneEnemies();
        void Detach(IObserver observer);
        void ExecuteEnemies();
        bool ExecuteTurn(Player player, int x, int y, string id);
        void Notify();
        bool SetNewTurn(string id);
        void SpawnWeapons();
        bool UndoCommand();
    }
}