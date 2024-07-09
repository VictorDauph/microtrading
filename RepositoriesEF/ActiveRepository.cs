using microTrading.dto;
using microTrading.Models;

namespace microTrading.RepositoriesEF
{
    public class ActiveRepository
    {
        private readonly MicroTradingContext _dbContext;

        public ActiveRepository(MicroTradingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Active Add(CreateActiveDto activeDto)
        {
            Active activeToSave = new Active(activeDto.Symbol);
            _dbContext.Add(activeToSave);
            _dbContext.SaveChanges();
            return activeToSave;
        }




    }
}
