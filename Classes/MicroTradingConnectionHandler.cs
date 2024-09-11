using microTrading.Services;
using System;
using System.Net.WebSockets;
using System.Text;

namespace microTrading.Classes
{
    public class MicroTradingConnectionHandler
    {
        public string? _streamSessionId { get; set; }
        public ClientWebSocket _ws { get; set; }
        public ClientWebSocket _wsStream { get; set; }
        public MicroTradingConnectionHandler(ClientWebSocket ws, ClientWebSocket wsStream)
        {
            _ws = ws;
            _wsStream = wsStream;
        }
    }
}
