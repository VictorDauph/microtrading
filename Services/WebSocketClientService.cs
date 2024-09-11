
using Microsoft.AspNetCore.Connections;
using microTrading.Classes;
using microTrading.dto;
using microTrading.Models;
using microTrading.RepositoriesEF;
using microTrading.utils;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;


namespace microTrading.Services
{
    public class WebSocketClientService
    {
        ActiveRepository _activeRepository;
        MessageHandlersService _messageHandlersService;

        //Faut ouvrir et fermer une connection à chaque requête visiblement...
        string uri = EnvironmentUtils.getEnvironmentVariable("API_URI");
        string userId = EnvironmentUtils.getEnvironmentVariable("API_USERID");
        string password = EnvironmentUtils.getEnvironmentVariable("API_PWD");

        List<MicroTradingConnectionHandler> connectionHandlers = [];
        
        public WebSocketClientService(ActiveRepository activeRepository, MessageHandlersService messageHandlersService)
        {
            _activeRepository = activeRepository;
            _messageHandlersService = messageHandlersService;
        }

        public async void LoginToXTBDemoServer() {

            MicroTradingConnectionHandler? connectionHandler = await connectWebSocket(uri, userId, password);
            if (connectionHandler != null && connectionHandler._ws != null)
            {
                await ListenToAnswer(connectionHandler, _messageHandlersService.HandleGenericMessage);
            }
        }
        async public Task<string> getAllSymbols()
        {

            MicroTradingConnectionHandler? connectionHandler = await connectWebSocket(uri, userId, password);
            if (connectionHandler != null && connectionHandler._ws != null)
            {
                sendSymbolRequest(connectionHandler, []);
                await ListenToAnswer(connectionHandler, _messageHandlersService.HandleGenericMessage);
                return "ok";
            }
            return "ws broken";
        }

        async public Task<string> getChartLast(GetChartLastRequestDto dto)
        {

            MicroTradingConnectionHandler? connectionHandler = await connectWebSocket(uri, userId, password);
            if (connectionHandler != null && connectionHandler._ws != null)
            {
                sendChartLastRequest(connectionHandler,dto);
               await ListenToAnswer(connectionHandler, _messageHandlersService.HandleGetLastChartMessage);
                return "ok";
            }
            return "ws broken";
        }

        async private Task<MicroTradingConnectionHandler?> connectWebSocket(string uri, string userId, string password)
        {
            ClientWebSocket ws = new ClientWebSocket();
            ClientWebSocket wsStream = new ClientWebSocket();
            MicroTradingConnectionHandler connectionHandler = new MicroTradingConnectionHandler(ws,wsStream);


            //Web Socket Client
            Console.Title = "Client WebSocket";

            await ws.ConnectAsync(new Uri(uri), CancellationToken.None);
            await wsStream.ConnectAsync(new Uri(uri+"Stream"), CancellationToken.None);


            if (connectionHandler._ws== null)
            {
                Console.WriteLine("Connection to endpoint failed");
                return null;
            }
            else
            {
                login(connectionHandler, userId, password);
            }

            return connectionHandler;
        }

        private void login(MicroTradingConnectionHandler connectionHandler, string userId, string password)
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

            sendRequest(body, connectionHandler);

        }

        public void sendSymbolRequest(MicroTradingConnectionHandler connectionHandler, List<string>? arguments )
        {
            var body = new
            {
                command = "getAllSymbols",
            };

            sendRequest(body, connectionHandler);

        }

        public void sendChartLastRequest(MicroTradingConnectionHandler connectionHandler, GetChartLastRequestDto dto)
        {
            var body = new
            {
                command = "getChartLastRequest",
                arguments = new
                {
                    info = new
                    {
                        period = dto.period,
                        start = new DateTimeOffset((DateTime)dto.start).ToUnixTimeMilliseconds(),
                        symbol = dto.symbol,
                    }
                }
            };
            sendRequest(body, connectionHandler);

        }

        async public void sendRequest(Object body, MicroTradingConnectionHandler connectionHandler)
        {
            string jsonBody = JsonSerializer.Serialize(body);
            var bytes = Encoding.UTF8.GetBytes(jsonBody);
            var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

            if (connectionHandler._ws != null)
            {
                await connectionHandler._ws.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
        public async Task<string> ListenToAnswer(
    MicroTradingConnectionHandler connectionHandler,
    Func<MicroTradingConnectionHandler, string, string> messageHandler,
    CancellationToken cancellationToken = default)
        {
            // Define the buffer size for receiving messages
            var buffer = new byte[1024 * 4];
            var timeout = TimeSpan.FromSeconds(5); // Set a timeout duration (10 seconds)

            // Read messages in a loop
            while (connectionHandler._ws.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result;
                using (var memoryStream = new MemoryStream())
                {
                    try
                    {
                        // Start the receive task
                        var receiveTask = connectionHandler._ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                        // Wait for either the receive task to complete or the timeout
                        if (await Task.WhenAny(receiveTask, Task.Delay(timeout, cancellationToken)) == receiveTask)
                        {
                            // ReceiveAsync completed successfully before the timeout
                            result = receiveTask.Result;

                            // Write the received data to the MemoryStream
                            memoryStream.Write(buffer, 0, result.Count);

                            // Continue receiving if the message is incomplete
                            while (!result.EndOfMessage && connectionHandler._ws.State == WebSocketState.Open)
                            {
                                result = await connectionHandler._ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                                memoryStream.Write(buffer, 0, result.Count);
                            }

                            // Convert the data to a string
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            string message2 = Encoding.UTF8.GetString(memoryStream.ToArray());

                            // Call the message handler to process the received message
                            messageHandler(connectionHandler, message2);
                        }
                        else
                        {
                            // The receive task timed out
                            Console.WriteLine("ReceiveAsync operation timed out.");
                            break; // Optionally, you can break or retry based on your logic
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Handle task cancellation (from timeout or cancellation token)
                        break;
                    }
                    catch (WebSocketException ex)
                    {
                        // Handle WebSocket errors (like disconnection)
                        Console.WriteLine($"WebSocket error: {ex.Message}");
                        break;
                    }
                }
            }
            return "ok";
        }
    }
}
