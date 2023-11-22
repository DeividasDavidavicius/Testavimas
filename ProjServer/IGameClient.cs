using Models;

namespace ProjServer
{
    //This is an interface for client methods
    public interface IGameClient
    {
        //TODO: TURNS OUT YOU CANNOT SEND COMPLEX DATA STRUCTURES THROUGH SIGNALR. NEED TO PARSE EVERYTHING (or atleast I couldn't figure it out)

        Task OnCreateServer(string gameSession);
        Task OnConnectToServer(string gameSession);
        Task OnCannotFindServer();
        Task OnNewPlayerConnectedToServer(string newPlayer);
        //Task OnGameStart(Grid grid, int playerTurnId);
        Task OnGameStart(string gameSession);
        Task OnPlayerTurn(string gameSession);
        Task BadRequest(int code);
        Task OnPlayerTurnInfo(string commandListJson);
        Task OnPlayerAvailableEndTurn(string gameSession);
    }
}
