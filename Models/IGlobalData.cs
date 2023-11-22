using Models;

namespace ProjServer
{
    public interface IGlobalData
    {
        public bool AddGameSessionByCode(string id, GameSession gameSession);
        public bool AddGameSessionByPlayerId(string playerId, GameSession gameSession);
        public bool AddPlayer(string id, Player player);
        public bool ContainsGameSessionByCode(string id);
        public bool ContainsGameSessionByPlayerId(string playerId);
        public bool ContainsPlayer(string id);
        public GameSession? FindGameSessionByCode(string id);
        public IGameSession? FindGameSessionByPlayerId(string playerId);
        public Player? FindPlayer(string id);
        public void RemoveGameSessionByCode(string id);
        public void RemoveGameSessionByPlayerId(string playerId);
        public void RemovePlayer(string id);
    }
}