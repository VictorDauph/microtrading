using microTrading.dto;
using microTrading.Models;
using microTrading.RepositoriesEF;

namespace microTrading.Services
{
    public class ValueRecordService
    {
        ValueRecordRepository _repository;
        public ValueRecordService(ValueRecordRepository repository) {
            _repository = repository;
        }

        public void recordValue(RateInfoDto rateInfo)
        {
            ValueRecord record = mapValueRecord(rateInfo);
            _repository.addValueRecord(record);
        }

        //Mettre la méthode de lecture et de record dans des instances séparées d'une classe pour pouvoir associer un symbol et un record?

        private ValueRecord mapValueRecord(RateInfoDto rateInfo) {
            ValueRecord valueRecord = new ValueRecord();
            //manque récupération de ActiveId ou symbol? quelquepart
            //manque runId

            valueRecord.OpenValue = rateInfo.open;
            valueRecord.CloseValue = rateInfo.open + rateInfo.close;
            valueRecord.HighValue = rateInfo.open + rateInfo.high;
            valueRecord.LowValue = rateInfo.open + rateInfo.low;
            valueRecord.RecordDate = DateTimeOffset.FromUnixTimeMilliseconds(rateInfo.ctm).UtcDateTime;
            valueRecord.vol = rateInfo.vol;

            //valeurs calculées
            valueRecord.TypicalPrice = (valueRecord.HighValue + valueRecord.LowValue + valueRecord.CloseValue) / 3;
            valueRecord.WeightedClosePrice = (valueRecord.HighValue + valueRecord.LowValue + valueRecord.CloseValue * 2) / 4;
            valueRecord.MedianPrice = (valueRecord.HighValue + valueRecord.LowValue) / 2;
            valueRecord.OHLCAverage = (valueRecord.OpenValue + valueRecord.CloseValue + valueRecord.HighValue + valueRecord.LowValue) / 4;


            return valueRecord;
        } 
    }
}
