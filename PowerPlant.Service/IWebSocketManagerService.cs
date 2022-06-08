using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlant.Services
{
    public interface IWebSocketManagerService
    {
        Task AddWebSocket(WebSocket webSocket);
        List<WebSocket> GetConnectedClients();
    }
}
