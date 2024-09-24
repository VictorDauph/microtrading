using microTrading.Models;

namespace microTrading.RepositoriesEF
{
    public class ValueRecordRepository
    {
        MicroTradingContext _dbContext;
        public ValueRecordRepository(MicroTradingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ValueRecord addValueRecord(ValueRecord record) { 
            _dbContext.Add(record);
            _dbContext.SaveChanges();
            return record;
        }
    }
}
