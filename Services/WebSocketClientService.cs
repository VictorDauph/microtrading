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
        /*
        public void ListenToTestServer()
        {
            String uri = "ws://localhost:6666/ws";
            connectWebSocket(uri,"","");
        }
        */

        public async void LoginToXTBDemoServer() {

            ClientWebSocket? ws =await connectWebSocket(uri,userId,password);
            await ListenToAnswer(ws,HandleMessage);
            //await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
           
        }
        async public void getAllSymbols()
        {
            ClientWebSocket? ws = await connectWebSocket(uri, userId, password);
            if (ws != null)
            {
                sendSymbolRequest(ws);
                await ListenToAnswer(ws,HandleMessage);
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

        /*
        public async void ListenToAnswer(ClientWebSocket ws)
        {
            //comment retourner les données? avec un stream?
            // demande à  chatGPT avec ça: récupérer dans un stream les résultats reçus via un ClientWebSocket dans le framework dotnet
            var buffer = new byte[256];


            do
            {
                var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {

                    Console.WriteLine("fermeture websocket normale");
                }

                Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, result.Count));
            } while (ws.State == WebSocketState.Open);
            Console.WriteLine("end of while loop");
        }
        */
        static async Task ListenToAnswer(ClientWebSocket ws, Action<string> messageHandler)
        {
            // Définir la taille du buffer pour la réception des messages
            var buffer = new byte[1024 * 4];

            // Lire les messages en boucle
            while (ws.State == WebSocketState.Open)
            {
                // Utiliser un MemoryStream local pour construire le message complet
                using (var memoryStream = new MemoryStream())
                {
                    WebSocketReceiveResult result;
                    do
                    {
                        // Lire les données reçues
                        result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                        // Écrire les données reçues dans le MemoryStream
                        memoryStream.Write(buffer, 0, result.Count);

                    } while (!result.EndOfMessage); // Continuer à lire jusqu'à la fin du message

                    // Convertir les données en chaîne de caractères
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    string message = Encoding.UTF8.GetString(memoryStream.ToArray());

                    // Appeler le gestionnaire de messages pour traiter le message reçu
                    messageHandler(message);
                }
            }
        }

        static void HandleMessage(string message)
        {
            // Logique de traitement des messages
            Console.WriteLine("Traitement du message : " + message);
            // Par exemple, vous pouvez analyser le message JSON ici, ou déclencher une autre action
        }
    }
}
