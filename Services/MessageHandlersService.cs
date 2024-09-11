using microTrading.Classes;
using microTrading.dto;
using microTrading.Models;
using microTrading.RepositoriesEF;
using System.Text.Json;

namespace microTrading.Services
{
    public class MessageHandlersService
    {
        ActiveRepository _activeRepository;
        ValueRecordService _valueRecordService;
        public MessageHandlersService(ActiveRepository activeRepository, ValueRecordService valueRecordService) {
            _activeRepository = activeRepository;
            _valueRecordService = valueRecordService;
        }
        public string HandleGenericMessage(MicroTradingConnectionHandler connectionHandler, string message)
        {

            Object decryptedMessage = DetectType(message);
            switch (decryptedMessage)
            {
                case ConnectionObject connectionObject:
                    // Handle the case where decryptedMessage is a ConnectionObject
                    connectionHandler._streamSessionId = connectionObject.streamSessionId;
                    Console.WriteLine("stream session id " + connectionObject.streamSessionId);
                    break;

                case List<ActiveDto> activeArray:
                    Console.WriteLine("active array size " + activeArray.Count);
                    foreach (ActiveDto active in activeArray)
                    {
                        _activeRepository.AddActiveFromSymbol(active.symbol);
                    }

                    break;

                default:
                    // Safely get the first 1000 characters or the full message if it's shorter.
                    string outputMessage = message.Length > 1000 ? message.Substring(0, 1000) : message;
                    Console.WriteLine("Object type unknown: " + outputMessage);
                    break;
            }
            return "message handled";
        }

        private static Object? DetectType(string message)
        {
            // Deserialize the JSON message into a ConnectionObject
            try
            {
                JsonDocument doc = JsonDocument.Parse(message);
                JsonElement root = doc.RootElement;
                if (root.TryGetProperty("streamSessionId", out _))
                {
                    ConnectionObject? connectionObject = JsonSerializer.Deserialize<ConnectionObject>(message);
                    return connectionObject;
                }
                //si la réponse contien un array dans returnData
                if (root.TryGetProperty("returnData", out JsonElement returnData) && returnData.ValueKind == JsonValueKind.Array)
                {
                    JsonElement element = returnData.EnumerateArray().First();

                    //symbol field dans element = getAllSymbols command
                    if (element.TryGetProperty("symbol", out _))
                    {
                        List<ActiveDto> activeArray = new List<ActiveDto>();
                        foreach (JsonElement dataElement in returnData.EnumerateArray())
                        {
                            ActiveDto active = JsonSerializer.Deserialize<ActiveDto>(dataElement);
                            activeArray.Add(active);
                        }
                        return activeArray;

                    }
                }
                if (root.TryGetProperty("close", out _))
                {
                    Console.WriteLine(root);
                }
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(message);
                return message;
            }
        }

        public string HandleGetLastChartMessage(MicroTradingConnectionHandler connectionHandler, string message)
        { 
            JsonDocument doc = JsonDocument.Parse(message);
            if(doc.RootElement.TryGetProperty("returnData", out JsonElement returnData)){
                if(returnData.TryGetProperty("rateInfos", out JsonElement rateInfosJson)){
                    List<RateInfoDto> rateInfos = JsonSerializer.Deserialize<List<RateInfoDto>>(rateInfosJson);
                    rateInfos.ForEach(rateInfo =>
                    {
                        _valueRecordService.recordValue(rateInfo);
                    });
                    
                }
            }


            return "message handled";
        }
    }


}
