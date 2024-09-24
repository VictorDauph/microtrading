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

        public Active AddSingleFromDto(ActiveDto activeDto)
        {
            Active activeToSave = new Active(activeDto.symbol);
            _dbContext.Add(activeToSave);
            _dbContext.SaveChanges();
            return activeToSave;
        }

        public void AddActiveFromSymbol(string symbol) {
            if (!CheckIfExists(symbol)) {
                Active activeToSave = new Active(symbol);
                _dbContext.Actives.Add(activeToSave);
                _dbContext.SaveChanges();
            }
        }

        public bool CheckIfExists(string symbol) {
            return _dbContext.Actives.Any(active => active.Symbol == symbol);
        }

        public Active getOneFromSymbol(string symbol) {
            return _dbContext.Actives.FirstOrDefault(active => active.Symbol == symbol);
        }
        public IEnumerable<Active> GetAll()
        {
            return _dbContext.Actives.ToList();
        }




    }
}
