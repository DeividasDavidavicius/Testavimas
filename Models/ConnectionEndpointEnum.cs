using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{


    public enum ConnectionEndpointEnum
    {
        onCreateServer = 0,
        OnConnectToServer = 1,
        OnNewPlayerConnectedToServer = 2,
        OnGameStart = 3,
        OnPlayerTurn = 4,
        OnPlayerTurnInfo = 5,
        OnPlayerAvailableEndTurnHanlder = 6,
        OnEndTurn = 7,
        OnUndo = 8,
    } 
}
