using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace PowerPlant.Services
{
    public class WebSocketManagerService : IWebSocketManagerService
    {
        private readonly List<WebSocket> _webSockets;

        public WebSocketManagerService()
        {
            _webSockets = new List<WebSocket>();
        }

        public async Task AddWebSocket(WebSocket webSocket)
        {
            _webSockets.Add(webSocket);
            await KeepWebSocketConnectionAlive(webSocket).ConfigureAwait(false);
        }

        public List<WebSocket> GetConnectedClients()
        {
            return _webSockets;
        }

        private async Task KeepWebSocketConnectionAlive(WebSocket socket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _webSockets.Remove(socket);
        }
    }
}
