using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using Crowdfunding.Domain;
using Crowdfunding.Domain.ViewModels;
using Crowdfunding.Repository.Context;

namespace Crowdfunding.Repository
{
    public class HomeInfoRepository : IHomeInfoRepository
    {
        private readonly CrowdfundingOnLineDBContext _dbContext;

        public HomeInfoRepository(CrowdfundingOnLineDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<HomeViewModel> RecoverInitialDataHomeAsync()
        {
            var totalDonors = _dbContext.Donations.CountAsync();
            var totalValue = _dbContext.Donations.SumAsync(value => value.Value);
            return new HomeViewModel
            {
                TotalAmountCollected = await totalValue,
                AmountOfDonors = await totalDonors
            };
        }
    }
}