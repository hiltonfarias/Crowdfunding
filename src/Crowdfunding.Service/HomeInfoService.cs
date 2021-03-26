using AutoMapper;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Service
{
    public class HomeInfoService : IHomeInfoService
    {
        private readonly IMapper _mapper;
        private readonly IDonationService _donationService;
        private readonly GloballAppConfig _globallAppConfig;
        private readonly IHomeInfoRepository _homeInfoRepository;
        private readonly ICauseRepository _causeRepository;

        public HomeInfoService(
            IMapper mapper, 
            IDonationService donationService, 
            GloballAppConfig globallAppConfig, 
            IHomeInfoRepository homeInfoRepository, 
            ICauseRepository causeRepository
        )
        {
            _mapper = mapper;
            _donationService = donationService;
            _globallAppConfig = globallAppConfig;
            _homeInfoRepository = homeInfoRepository;
            _causeRepository = causeRepository;
        }
        public async Task<HomeViewModel> RecoverDataInitialHomeAsync()
        {
            var initialDataHome = await RecoverTotalDataHome();
            var institution = RecoverCausesAsync();
            var donation = RecoverDonationAsync();

            var diffDate = _globallAppConfig.CampaignEndDate.Subtract(DateTime.Now);

            initialDataHome.TimeRemainingDays = diffDate.Days;
            initialDataHome.TimeRemainingHours = diffDate.Hours;
            initialDataHome.TimeRemainingMinutes = diffDate.Minutes;

            initialDataHome.RemainingTargetValue = 
                _globallAppConfig.CampaignObjective - initialDataHome.TotalAmountCollected;
            initialDataHome.TotalPercentageCollected = 
                initialDataHome.TotalAmountCollected * 100 / _globallAppConfig.CampaignObjective;
            
            await Task.WhenAll();

            initialDataHome.Donors = await donation;
            initialDataHome.Institutions = await institution;
            
            return initialDataHome;
        }
        private async Task<HomeViewModel> RecoverTotalDataHome()
        {
            return await _homeInfoRepository.RecoverInitialDataHomeAsync();
        }
        public async Task<IEnumerable<CauseViewModel>> RecoverCausesAsync()
        {
            return _mapper.Map<IEnumerable<Cause>,IEnumerable<CauseViewModel>>(await _causeRepository.RecoverCause());
        }
        private async Task<IEnumerable<DonorViewModel>> RecoverDonationAsync()
        {
            return await _donationService.RecoverDonorsAsync();
        }
    }
}