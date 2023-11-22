using Microsoft.AspNetCore.SignalR;
using Models;
using Models.AbstractFactory;
using Models.Cells;
using Models.Decorator;
using Newtonsoft.Json;
using System.Drawing;

namespace ProjServer
{
    public class ServerHub : Hub<IGameClient>
    {
        public ServerHub(IGlobalData globalData)
        {
            this.globalData = globalData;
        }

        public IGlobalData globalData;

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connection established");
            return base.OnConnectedAsync();
        }

        public async Task CreateServer(string username)
        {
            string? userId = Context.ConnectionId;
            if (userId == null || globalData.ContainsPlayer(userId))
            {
                await Clients.Caller.BadRequest(403);
                return;
            }
            Player host = new Player(userId, 0, 0, Color.Blue, 1000, username);
            Armor armor = new PlayerGlove(new PlayerPants(new PlayerChestplate(new ArmorFrame(1))));
            //FACTORY PATTERN
            Dictionary<GridFactory, double> factories;

            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            if (randomNumber < 50)
            {
                factories = new Dictionary<GridFactory, double>()
                {
                    { new GridEmptyFactory(), 0.8 },
                    { new GridObstacleFactory(), 0.15 },
                    { new DemonFactory(), 0.05 }
                };
            }
            else
            {
                factories = new Dictionary<GridFactory, double>()
                {
                    { new GridEmptyFactory(), 0.8 },
                    { new GridObstacleFactory(), 0.15 },
                    { new OrcFactory(), 0.05 }
                };
            }

            int xSize = 20;
            int ySize = 20;

            Cell[,] cells = new Cell[ySize, xSize];

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    GridFactory selectedFactory = factories.Keys.First();
                    double prob = random.NextDouble();
                    double cumulativeProb = 0.0;
                    foreach (KeyValuePair<GridFactory, double> factory in factories)
                    {
                        cumulativeProb += factory.Value;
                        if (prob <= cumulativeProb)
                        {
                            selectedFactory = factory.Key;
                            break;
                        }
                    }
                    cells[y, x] = selectedFactory.CreateCell();
                }
            }

            List<EmptyCell> emptyCells = new List<EmptyCell>();
            List<int> emptyCellsX = new List<int>();
            List<int> emptyCellsY = new List<int>();

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    Cell cell = cells[i, j];
                    if (cell.GetType() == typeof(EmptyCell))
                    {
                        emptyCellsX.Add(i);
                        emptyCellsY.Add(j);
                        emptyCells.Add((EmptyCell)cell);
                    }

                }
            }

            if (emptyCells.Count > 0)
            {
                int randomEmptyCellNumber = random.Next(0, emptyCells.Count);
                AirdropCell airdropCell = new AirdropCell(armor.GiveArmor());
                cells[emptyCellsX[randomEmptyCellNumber], emptyCellsY[randomEmptyCellNumber]] = airdropCell;
            }

            GameSession gameSession = new GameSession(host, cells);
            globalData.AddGameSessionByCode(gameSession.sessionCode, gameSession);
            globalData.AddPlayer(Context.ConnectionId, host);
            globalData.AddGameSessionByPlayerId(userId, gameSession);

            JsonConvertFacade jsonConvert = new JsonConvertFacade();
            string gameSessionJson = jsonConvert.Serialize(gameSession);
            await Console.Out.WriteLineAsync(gameSessionJson);
            await Clients.Caller.OnCreateServer(gameSessionJson);
        }

        public async Task JoinServer(string sessionCode, string username)
        {
            string? userId = Context.ConnectionId;
            if (userId == null || globalData.ContainsPlayer(userId))
            {
                await Clients.Caller.BadRequest(403);
                return;
            }
            GameSession? gameSession = globalData.FindGameSessionByCode(sessionCode);
            if (gameSession == null)
            {
                await Clients.Caller.BadRequest(404);
                return;
            }
            Player player = new Player(userId, 19, 0, Color.Orange, 1000, username);
            globalData.AddPlayer(userId, player);
            globalData.AddGameSessionByPlayerId(userId, gameSession);
            gameSession.addPlayer(player);
            JsonConvertFacade jsonConvert = new JsonConvertFacade();
            string gameSessionJson = jsonConvert.Serialize(gameSession);
            string newPlayerJson = jsonConvert.Serialize(player);
            await Console.Out.WriteLineAsync(gameSessionJson.ToString());
            await Clients.All.OnNewPlayerConnectedToServer(newPlayerJson);
            await Clients.Caller.OnConnectToServer(gameSessionJson);
        }

        public async Task StartServer(string sessionCode)
        {
            if (Context.UserIdentifier == null)
            {
                await Clients.Caller.BadRequest(403);
            }
            GameSession? gameSession = globalData.FindGameSessionByCode(sessionCode);
            if (gameSession == null)
            {
                await Clients.Caller.BadRequest(404);
                return;
            }
            JsonConvertFacade jsonConvert = new JsonConvertFacade();
            string gameSessionJson = jsonConvert.Serialize(gameSession);
            await Console.Out.WriteLineAsync("GameSession \n" + gameSessionJson);
            await Clients.All.OnGameStart(gameSessionJson);
        }

        public async Task PlayerTurn(string xStr, string yStr)
        {
            int x = int.Parse(xStr);
            int y = int.Parse(yStr);

            if (!ValidateUser())
            {
                await Clients.Caller.BadRequest(404);
                return;
            }
            string userId = Context.ConnectionId;
            GameSession? gameSession = globalData.FindGameSessionByPlayerId(userId);
            Player? player = globalData.FindPlayer(userId);
            bool moveSuccessful = gameSession.ExecuteTurn(player, x, y, userId);
            if (!moveSuccessful)
            {
                await Clients.Caller.BadRequest(404);
                return;
            }

            JsonConvertFacade jsonConvert = new JsonConvertFacade();
            string gameSessionJson = jsonConvert.Serialize(gameSession);
            await Clients.Caller.OnPlayerAvailableEndTurn(gameSessionJson);
        }

        public async Task OnUndo()
        {
            if (!ValidateUser())
            {
                await Clients.Caller.BadRequest(404);
                return;
            }
            string userId = Context.ConnectionId;
            GameSession? gameSession = globalData.FindGameSessionByPlayerId(userId);
            gameSession.UndoCommand();
            JsonConvertFacade jsonConvert = new JsonConvertFacade();
            string gameSessionJson = jsonConvert.Serialize(gameSession);
            await Clients.All.OnPlayerTurn(gameSessionJson);
        }

        public async Task OnEndTurn()
        {
            if (!ValidateUser())
            {
                await Clients.Caller.BadRequest(404);
                return;
            }

            string userId = Context.ConnectionId;
            Player player = globalData.FindPlayer(userId);
            GameSession gameSession = globalData.FindGameSessionByPlayerId(userId);
            bool success = gameSession.SetNewTurn(userId);
            if (!success)
            {
                await Clients.Caller.BadRequest(404);
                return;
            }

            for (int i = 0; i < gameSession.players.Count; i++)
            {
                if (gameSession.players[i].healthPoints <= 0)
                {
                    gameSession.gameEnd = "Game over!";

                    for (int j = 0; j < gameSession.players.Count; j++)
                    {
                        if (gameSession.players[j].healthPoints > 0)
                        {
                            gameSession.gameEnd += " The winner is " + gameSession.players[j].username + "!";
                        }
                    }
                }
            }

            JsonConvertFacade jsonConvert = new JsonConvertFacade();
            string gameSessionJson = jsonConvert.Serialize(gameSession);
            await Clients.All.OnPlayerTurn(gameSessionJson);
        }

        public bool ValidateUser()
        {
            string userId = Context.ConnectionId;
            if (userId == null) return false;

            Player? player = globalData.FindPlayer(userId);
            if (player == null) return false;

            GameSession? gameSession = globalData.FindGameSessionByPlayerId(userId);
            if (gameSession == null) return false;

            return true;
        }
    }
}
