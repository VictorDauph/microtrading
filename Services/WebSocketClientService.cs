using microTrading.utils;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace microTrading.Services
{
    public class WebSocketClientService
    {
        //Faut ouvrir et fermer une connection à chaque requête visiblement...
        string uri = EnvironmentUtils.getEnvironmentVariable("API_URI");
        string userId = EnvironmentUtils.getEnvironmentVariable("API_USERID");
        string password = EnvironmentUtils.getEnvironmentVariable("API_PWD");


        public WebSocketClientService()
        {
        }
        public void ListenToTestServer()
        {
            String uri = "ws://localhost:6666/ws";
            connectWebSocket(uri,"","");
        }

        public async void LoginToXTBDemoServer() {

            ClientWebSocket? ws =await connectWebSocket(uri,userId,password);
            if (ws != null)
            {
                ListenToAnswer(ws);
            }
        }
        async public void getAllSymbols()
        {
            ClientWebSocket? ws = await connectWebSocket(uri, userId, password);
            if (ws != null)
            {
                sendSymbolRequest(ws);
                ListenToAnswer(ws);
            }

        }

        async private Task<ClientWebSocket?> connectWebSocket(string uri, string userId, string password)
        {
            ClientWebSocket ws = new ClientWebSocket();

            //Web Socket Client
            Console.Title = "Client WebSocket";

            await ws.ConnectAsync(new Uri(uri), CancellationToken.None);
            

            if (ws == null)
            {
                Console.WriteLine("Connection to endpoint failed");
                return null;
            }
            else
            {
                login(ws, userId, password);
            }

            return ws;
        }

        private void login(ClientWebSocket ws, string userId, string password)
        {
            var body = new
            {
                command = "login",
                arguments = new
                {
                    userId = userId,
                    password = password
                }
            };

            sendRequest(body,ws);

        }

        public void sendSymbolRequest(ClientWebSocket ws)
        {
            var body = new
            {
                command = "getAllSymbols",
            };

            sendRequest(body, ws);

        }
        

        async public void sendRequest(Object body, ClientWebSocket ws)
        {
            string jsonBody = JsonSerializer.Serialize(body);
            var bytes = Encoding.UTF8.GetBytes(jsonBody);
            var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

            if (ws != null)
            {
                await ws.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async void ListenToAnswer(ClientWebSocket ws)
        {
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
        }
    }
}
