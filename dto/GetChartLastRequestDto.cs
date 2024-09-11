using System.Text.Json.Serialization;

namespace microTrading.dto
{
    public class GetChartLastRequestDto
    {
        [JsonConstructor]
        public GetChartLastRequestDto(string symbol, DateTime? start, int? period) {
            {
                this.symbol = symbol;
                // Use default values if parameters are null
                this.period = period ?? 1; // Default to 1 if null
                this.start = start ?? DateTime.Now.AddMinutes(-1); // Default to DateTime.Now if null
            }
        }

        public int? period { get; set; }
        public DateTime? start { get; set; }
        public string symbol { get; set; }
    }
}
