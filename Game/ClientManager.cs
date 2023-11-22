using Models;
using System.Numerics;
using System.Runtime.Serialization;

namespace Game
{
    public class ClientManager
    {
        public string sessionCode { get; private set; }
        public Grid? grid { get; private set; }
        public Player player { get; private set; }
        public List<Player> opponents { get; private set; }
        public string currentPlayerId { get; private set; }

        public GameSession gameSession { get; private set; }

        private Action<string> OnCreateServerHandler;
        private Action<string> OnConnectToServerActionHandler;
        private Action<string> OnNewPlayerConnectedToServerHandler;
        private Action<string> OnGameStartHandler;
        private Action<string> OnPlayerTurnHandler;
        private Action<string> OnPlayerTurnInfoHandler;
        private Action<string> OnPlayerAvailableEndTurnHandler;

        private List<string> commandList = new List<string>();

        public ClientManager()
        {
            opponents = new List<Player>();
            SetupConnectionHandlers();
        }

        private void DeserializeAndUpdateGameSession(string gameSessionJson)
        {
            if (string.IsNullOrEmpty(gameSessionJson)) return;

            GameSession? gameSession = Deserialize<GameSession>(gameSessionJson);
            if (gameSession == null) return;

            sessionCode = gameSession.sessionCode;

            if (player == null)
            {
                player = gameSession.players.Last();
                return;
            }

            Player? _player = gameSession.players.Find((p) => p.id == player.id);
            player = _player;
            for (int i = 0; i < opponents.Count; i++)
            {
                opponents[i] = gameSession.players.Find((p) => p.id == opponents[i].id);
            }

            if (gameSession.gameEnd != null)
            {
                this.gameSession = gameSession;
            }
        }

        private void DeserializeAndUpdateGameSessionAfterTurn(string gameSessionJson)
        {
            if (string.IsNullOrEmpty(gameSessionJson)) return;

            GameSession? gameSession = Deserialize<GameSession>(gameSessionJson);
            if (gameSession == null) return;
            if(gameSession.gameEnd != null)
            {
                this.gameSession = gameSession;
            }

            Player? _player = gameSession.players.Find((p) => p.id == player.id);
            if (_player == null)
            {
                return;
            }
            for(int i = 0; i < opponents.Count; i++)
            {
                opponents[i] = gameSession.players.Find((p) => p.id == opponents[i].id);
            }
            player = _player;
            grid = gameSession.grid;
        }

        private void DeserializeAndConnectPlayer(string gameSessionJson)
        {
            if (string.IsNullOrEmpty(gameSessionJson)) return;

            GameSession? gameSession = Deserialize<GameSession>(gameSessionJson);
            if (gameSession == null) return;

            player = gameSession.players.Last();
            sessionCode = gameSession.sessionCode;

            opponents = new List<Player>();
            if (gameSession.players.Count > 1)
            {
                opponents.AddRange(gameSession.players.GetRange(0, gameSession.players.Count - 1));
            }
        }

        private void DeserializeAndUpdatePlayer(string playerJson)
        {
            if (string.IsNullOrEmpty(playerJson) || player == null) return;

            Player? newPlayer = Deserialize<Player>(playerJson);
            if (newPlayer == null) return;

            opponents.Add(newPlayer);
        }


        private void DeserializeAndUpdateGrid(string gameSessionJson)
        {
            if (string.IsNullOrEmpty(gameSessionJson)) return;

            GameSession? gameSession = Deserialize<GameSession>(gameSessionJson);
            if (gameSession == null || gameSession.grid == null) return;

            grid = gameSession.grid;
            currentPlayerId = gameSession.currentPlayerId;

            Player? _player = gameSession.players.Find((p) => p.id == player.id);
            if (_player == null)
            {
                return;
            }
            player = _player;
            for (int i = 0; i < opponents.Count; i++)
            {
                opponents[i] = gameSession.players.Find((p) => p.id == opponents[i].id);
            }

            if (gameSession.gameEnd != null)
            {
                this.gameSession = gameSession;
            }
        }

        private void DeserializeAndUpdateCommandList(string commandListJson)
        {
            if (string.IsNullOrEmpty(commandListJson)) return;

            List<string>? commands = Deserialize<List<string>>(commandListJson);

            if (commands == null) return;

            commandList = commands;
        }

        private T? Deserialize<T>(string json)
        {
            JsonConvertFacade jsonConvert = new JsonConvertFacade();
            return jsonConvert.Deserialize<T>(json);
        }

        private Action<string> CreateAction(Action<string> action)
        {
            return (paramArray) =>
            {
                if (paramArray.Length > 0)
                {
                    action(paramArray);
                }
            };
        }

        private void SetupConnectionHandlers()
        {
            OnCreateServerHandler = CreateAction(DeserializeAndUpdateGameSession);
            OnConnectToServerActionHandler = CreateAction(DeserializeAndConnectPlayer);
            OnNewPlayerConnectedToServerHandler = CreateAction(DeserializeAndUpdatePlayer);
            OnGameStartHandler = CreateAction(DeserializeAndUpdateGrid);
            OnPlayerTurnHandler = CreateAction(DeserializeAndUpdateGrid);
            OnPlayerAvailableEndTurnHandler = CreateAction(DeserializeAndUpdateGameSessionAfterTurn);
        }

        public (Action<string>, string)? GetLogicHandler(ConnectionEndpointEnum connectionEndpoint)
        {
            switch (connectionEndpoint)
            {
                case ConnectionEndpointEnum.onCreateServer:
                    return (OnCreateServerHandler, "OnCreateServer");
                case ConnectionEndpointEnum.OnConnectToServer:
                    return (OnConnectToServerActionHandler, "OnConnectToServer");
                case ConnectionEndpointEnum.OnNewPlayerConnectedToServer:
                    return (OnNewPlayerConnectedToServerHandler, "OnNewPlayerConnectedToServer");
                case ConnectionEndpointEnum.OnGameStart:
                    return (OnGameStartHandler, "OnGameStart");
                case ConnectionEndpointEnum.OnPlayerTurn:
                    return (OnPlayerTurnHandler, "OnPlayerTurn");
                case ConnectionEndpointEnum.OnPlayerTurnInfo:
                    return (OnPlayerTurnInfoHandler, "OnPlayerTurnInfo");
                case ConnectionEndpointEnum.OnPlayerAvailableEndTurnHanlder:
                    return (OnPlayerAvailableEndTurnHandler, "OnPlayerAvailableEndTurn");

            }
            return null;
        }

        public List<Player> GetOpponents()
        {
            return opponents;
        }

        public Player GetPlayer()
        {
            return player;
        }

        public bool IsPlayerTurn()
        {
            return currentPlayerId == player.id;
        }

        public Vector2 GetCoords(MouseEventArgs e)
        {
            return GetCellByCoordinates(e.X, e.Y);
        }

        private Vector2 GetCellByCoordinates(int xCoordinate, int yCoordinate)
        {
            int x = xCoordinate / grid.cellSize;
            int y = yCoordinate / grid.cellSize;

            if (x >= 0 && x < grid.gridSize && y >= 0 && y < grid.gridSize)
            {
                return new Vector2(x, y);
            }
            return new Vector2(-1, -1);
        }

        public List<string> GetCommandList()
        {
            return commandList;
        }
    }
}
