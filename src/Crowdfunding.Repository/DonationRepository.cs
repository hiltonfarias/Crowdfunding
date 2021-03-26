using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Repository.Context;

namespace Crowdfunding.Repository
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ILogger<DonationRepository> _logger;
        private readonly GloballAppConfig _globalSettings;
        private readonly CrowdfundingOnLineDBContext _crowdfundingOnLineDBContext;

        public DonationRepository(
            ILogger<DonationRepository> logger, 
            GloballAppConfig globalSettings, 
            CrowdfundingOnLineDBContext crowdfundingOnLineDBContext
        )
        {
            _logger = logger;
            _globalSettings = globalSettings;
            _crowdfundingOnLineDBContext = crowdfundingOnLineDBContext;
        }

        public async Task AddAsync(Donation donation)
        {
            await _crowdfundingOnLineDBContext.Donations.AddAsync(donation);
            await _crowdfundingOnLineDBContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Donation>> RecoverDonorsAsync(int pageIndex = 0)
        {
            return await _crowdfundingOnLineDBContext.Donations.Include("PersonalData").ToListAsync();
        }
    }
}