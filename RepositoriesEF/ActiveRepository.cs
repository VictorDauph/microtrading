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

        public Active AddSingleFromDto(CreateActiveDto activeDto)
        {
            Active activeToSave = new Active(activeDto.Symbol);
            _dbContext.Add(activeToSave);
            _dbContext.SaveChanges();
            return activeToSave;
        }

        public void AddActiveFromSymbol(string symbol) {
            Active activeToSave = new Active(symbol);
            _dbContext.Add(activeToSave);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Active> GetAll()
        {
            return _dbContext.Actives.ToList();
        }




    }
}
