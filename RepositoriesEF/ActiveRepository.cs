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

        public Active Add(Active active)
        {
            _dbContext.Add(active);
            _dbContext.SaveChanges();
            return active;
        }




    }
}
