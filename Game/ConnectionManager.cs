using Microsoft.AspNetCore.SignalR.Client;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    //"https://localhost:7028/serverHub"
    public class ConnectionManager
    {
        private HubConnection connection;

        public ConnectionManager(string url)
        {
            connection = new HubConnectionBuilder().WithAutomaticReconnect().WithUrl(url).Build();
        }

        public void StartHubConnection()
        {
            connection.StartAsync();
        }

        public HubConnection getConnection()
        {
            return connection;
        }

        public void AddHandler(string handlerName, List<Action<string>> handlers)
        {
            connection.On<string>(handlerName, (paramArray) =>
            {
                foreach (Action<string> handler in handlers)
                {
                    handler.Invoke(paramArray);
                }
            });
        }

        public Task? InvokeConnectionMethod(ConnectionEndpointEnum connectionEndpoint, params object[] parameters)
        {
            string[] paramStrArray = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                paramStrArray[i] = parameters[i].ToString();
            }

            switch (connectionEndpoint)
            {
                case ConnectionEndpointEnum.onCreateServer:
                    return connection.InvokeAsync("CreateServer", paramStrArray[0]);
                case ConnectionEndpointEnum.OnConnectToServer:
                    return connection.InvokeAsync("JoinServer", paramStrArray[0], paramStrArray[1]);
                case ConnectionEndpointEnum.OnGameStart:
                    return connection.InvokeAsync("StartServer", paramStrArray[0]);
                case ConnectionEndpointEnum.OnPlayerTurn:
                    return connection.InvokeAsync("PlayerTurn", paramStrArray[0], paramStrArray[1]);
                case ConnectionEndpointEnum.OnEndTurn:
                    return connection.InvokeAsync("OnEndTurn");
                case ConnectionEndpointEnum.OnUndo:
                    return connection.InvokeAsync("OnUndo");
            }
            return null;
        }
    }
}
