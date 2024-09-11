using microTrading.dto;

namespace microTrading.Services
{
    public class ValueRecordService
    {
        public ValueRecordService() { }

        public void recordValue(RateInfoDto rateInfo)
        {
            Console.WriteLine(rateInfo.ToString());
        }
    }
}
