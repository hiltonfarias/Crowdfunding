using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Repository.Context;

namespace Crowdfunding.Repository
{
    public class CauseRepository : ICauseRepository
    {
        private readonly CrowdfundingOnLineDBContext _dbContext;

        public CauseRepository(CrowdfundingOnLineDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cause> Add(Cause cause)
        {
            await _dbContext.AddAsync(cause);
            await _dbContext.SaveChangesAsync();

            return cause;
        }
        public async Task<IEnumerable<Cause>> RecoverCause()
        {
            return await _dbContext.Causes.ToListAsync();
        }
    }
}