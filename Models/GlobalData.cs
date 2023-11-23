using Models;
using System.Runtime.CompilerServices;

namespace ProjServer
{
    public class GlobalData : IGlobalData
    {
        private static Dictionary<string, IGameSession> gameSessionDict = new Dictionary<string, IGameSession>();
        private static Dictionary<string, Player> playerDict = new Dictionary<string, Player>();
        private static Dictionary<string, IGameSession> playerGameSessionDict = new Dictionary<string, IGameSession>();

        public GlobalData() { }

        public bool AddGameSessionByPlayerId(string playerId, IGameSession gameSession)
        {
            if (playerGameSessionDict.ContainsKey(playerId))
            {
                return false;
            }
            playerGameSessionDict.Add(playerId, gameSession);
            return true;
        }

        public bool ContainsGameSessionByPlayerId(string playerId)
        {
            return playerGameSessionDict.ContainsKey(playerId);
        }

        public IGameSession? FindGameSessionByPlayerId(string playerId)
        {
            if (!playerGameSessionDict.ContainsKey(playerId))
            {
                return null;
            }
            return playerGameSessionDict[playerId];
        }

        public void RemoveGameSessionByPlayerId(string playerId)
        {
            playerGameSessionDict.Remove(playerId);
        }

        public bool AddGameSessionByCode(string id, IGameSession gameSession)
        {
            if (gameSessionDict.ContainsKey(id))
            {
                return false;
            }
            gameSessionDict.Add(id, gameSession);
            return true;
        }

        public bool ContainsGameSessionByCode(string id)
        {
            return gameSessionDict.ContainsKey(id);
        }

        public IGameSession? FindGameSessionByCode(string id)
        {
            if (!gameSessionDict.ContainsKey(id))
            {
                return null;
            }
            return gameSessionDict[id];
        }

        public void RemoveGameSessionByCode(string id)
        {
            gameSessionDict.Remove(id);
        }

        public bool AddPlayer(string id, Player player)
        {
            if (playerDict.ContainsKey(id))
            {
                return false;
            }
            playerDict.Add(id, player);
            return true;
        }

        public bool ContainsPlayer(string id)
        {
            return playerDict.ContainsKey(id);
        }

        public Player? FindPlayer(string id)
        {
            if (!playerDict.ContainsKey(id))
            {
                return null;
            }
            return playerDict[id];
        }

        public void RemovePlayer(string id)
        {
            playerDict.Remove(id);
        }
    }
}
