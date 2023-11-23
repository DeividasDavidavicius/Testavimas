using Models;

namespace ProjServer
{
    public interface IGlobalData
    {
        public bool AddGameSessionByCode(string id, IGameSession gameSession);
        public bool AddGameSessionByPlayerId(string playerId, IGameSession gameSession);
        public bool AddPlayer(string id, Player player);
        public bool ContainsGameSessionByCode(string id);
        public bool ContainsGameSessionByPlayerId(string playerId);
        public bool ContainsPlayer(string id);
        public IGameSession? FindGameSessionByCode(string id);
        public IGameSession? FindGameSessionByPlayerId(string playerId);
        public Player? FindPlayer(string id);
        public void RemoveGameSessionByCode(string id);
        public void RemoveGameSessionByPlayerId(string playerId);
        public void RemovePlayer(string id);
    }
}