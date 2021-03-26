using AutoMapper;

using System.Collections.Generic;
using System.Threading.Tasks;

using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Service
{
    public class DonationService : IDonationService
    {
        private readonly IMapper _mapper;
        private readonly IDonationRepository _donationRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public DonationService(
            IMapper mapper, 
            IDonationRepository donationRepository, 
            IDomainNotificationService domainNotificationService
        )
        {
            _mapper = mapper;
            _donationRepository = donationRepository;
            _domainNotificationService = domainNotificationService;
        }

        public async Task MakeDonationAsync(DonationViewModel donationViewModel)
        {
            var entity = _mapper.Map<DonationViewModel, Donation>(donationViewModel);

            entity.UpdatePurchaseDate();

            if (entity.Valid())
            {
                await _donationRepository.AddAsync(entity);
                return;
            }
            _domainNotificationService.Add(entity);
        }

        public async Task<IEnumerable<DonorViewModel>> RecoverDonorsAsync(int pageIndex = 0)
        {
            return _mapper.Map<
                IEnumerable<Donation>,
                IEnumerable<DonorViewModel>
            >(await _donationRepository.RecoverDonorsAsync());
        }
    }
}