using Microsoft.AspNetCore.SignalR.Client;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Game
{
    public class GameClientFacade
    {
        public ClientManager clientManager;
        private ConnectionManager? connectionManager;
        private GraphicsManager graphicsManager;

        public GameClientFacade()
        {
            this.clientManager = new ClientManager();
            this.graphicsManager = new GraphicsManager();
        }

        public void InitializeConnection(string url)
        {
            if (connectionManager == null)
            {
                connectionManager = new ConnectionManager(url);
            }
            connectionManager.StartHubConnection();
        }

        public void SetupConnectionEndpoint(ConnectionEndpointEnum connectionEndpoint, Action uiHandler)
        {
            if (clientManager == null || connectionManager == null) return;
            
            (Action<string>, string)? logicHandler = clientManager.GetLogicHandler(connectionEndpoint);
            if (logicHandler == null) return;
            
            Action<string> tempHandler = (temp) => uiHandler();
            List<Action<string>> handlers = new List<Action<string>>()
            {
                logicHandler.Value.Item1,
                tempHandler
            };
            connectionManager.AddHandler(logicHandler.Value.Item2, handlers);
        }

        public string GetSessionCode()
        {
            return clientManager.sessionCode;
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>
            {
                clientManager.GetPlayer()
            };
            players.AddRange(clientManager.GetOpponents());
            return players;
        }

        public Task? InvokeConnectionMethod(ConnectionEndpointEnum connectionEndpoint, params object[] parameters)
        {
            return connectionManager.InvokeConnectionMethod(connectionEndpoint, parameters);
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (!clientManager.IsPlayerTurn()) return;
            Vector2 vector = clientManager.GetCoords(e);

            Task? invocation = InvokeConnectionMethod(ConnectionEndpointEnum.OnPlayerTurn, vector.X, vector.Y);
            if (invocation == null) return;

            invocation.Wait();
        }


        //TODO: Should handle null exceptions
        public void OnCreateServer(string username)
        {
            InvokeConnectionMethod(ConnectionEndpointEnum.onCreateServer, username).Wait();
        }

        public void OnJoinServer(string sessionCode, string username)
        {
            InvokeConnectionMethod(ConnectionEndpointEnum.OnConnectToServer, sessionCode, username).Wait();
        }

        public void OnStartServer()
        {
            InvokeConnectionMethod(ConnectionEndpointEnum.OnGameStart, clientManager.sessionCode).Wait();
        }

        public void DrawGrid(Panel panel)
        {
            graphicsManager.DrawGrid(panel, clientManager.grid);
        }

        public Player? GetPlayer()
        {
            return clientManager.player;
        }

        public List<string> GetCoomands()
        {
            return clientManager.GetCommandList();
        }

        public void OnEndTurn()
        {
            InvokeConnectionMethod(ConnectionEndpointEnum.OnEndTurn, new object[] {});
        }

        public void OnUndo()
        {
            InvokeConnectionMethod(ConnectionEndpointEnum.OnUndo, new object[] {});
        }
    }
}
