using Microsoft.EntityFrameworkCore;
using microTrading.dto;
using microTrading.Models;

namespace microTrading.RepositoriesEF
{
    public class RunRepository
    {

        private readonly MicroTradingContext _dbContext;


        public RunRepository(MicroTradingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RunPerfomance AddSingleFromSymbol(Active active)
        {
            RunPerfomance run = new RunPerfomance();
            run.IdActive = active.Id;
            run.RunStart = DateTime.Now;
            _dbContext.RunPerfomances.Add(run);
            _dbContext.SaveChanges();
            return run;
        }
    }
}
