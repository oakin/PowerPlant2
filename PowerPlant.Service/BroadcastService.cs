using Serilog;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PowerPlant.Services
{
    public class BroadcastService : IBroadcastService
    {
        private readonly IWebSocketManagerService _webSocketManagerservice;
        private readonly ILogger _logger;

        public BroadcastService(IWebSocketManagerService webSocketManagerservice, ILogger logger)
        {
            _webSocketManagerservice = webSocketManagerservice;
            _logger = logger;
        }

        public async Task BroadcastRequestAndResponse(string request, string response)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(string.Join('|',request, response));
                List<WebSocket> clients = _webSocketManagerservice.GetConnectedClients();

                foreach (WebSocket ws in clients)
                {
                    try
                    {
                        await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    catch (Exception e)
                    {
                        _logger.Warning("Problem with connection!");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error while broadcasting message. Error message: {e.Message}");
            }
        }
    }
}
