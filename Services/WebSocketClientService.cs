using System.Net.WebSockets;
using System.Text;
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace microTrading.Services
{
    public class WebSocketClientService
    {
        ClientWebSocket? ws =null;
        public WebSocketClientService() { }

        public void ListenToTestServer()
        {
            String uri = "ws://localhost:6666/ws";
            connectWebSocket(uri,"","");
        }

        async public void LoginToXTBDemoServer() {
            string uri = "wss://ws.xtb.com/demo";
            string userId = "16593269";
            string password = "Q53Kie";
            await connectWebSocket(uri, userId,password);
            

        }

        async private Task<ClientWebSocket?> connectWebSocket(string uri,string userId, string password)
        {
            ws = new ClientWebSocket();

            //Web Socket Client
            Console.Title = "Client WebSocket";

            await ws.ConnectAsync(new Uri(uri), CancellationToken.None);
            

            if (ws == null)
            {
                Console.WriteLine("Connection to endpoint failed");
                return null;
            }

            await login(userId, password);


            var buffer = new byte[256];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    Console.WriteLine("fermeture websocket normale");
                }
                else
                {
                    Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, result.Count));
                }
            }
            Console.WriteLine("end of while loop");
            return ws;
            
        }

        async private Task<string> login(string userId, string password)
        {
            var body = new
            {
                command = "login",
                arguments = new
                {
                    userId = "16593269",
                    password = "Q53Kie"
                }
            };

            string jsonBody = JsonSerializer.Serialize(body);
            var bytes = Encoding.UTF8.GetBytes(jsonBody);
            var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

            if (ws != null)
            {
                await ws.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                return "logged in";
            }
            return "not logged in";

        }
    }
}
