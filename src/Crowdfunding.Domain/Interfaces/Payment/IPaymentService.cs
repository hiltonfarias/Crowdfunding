using System.Collections.Generic;
using System.Threading.Tasks;

using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Domain.Interfaces.Payment
{
    public interface IPaymentService
    {
        Task<IEnumerable<CauseViewModel>> RecoverAsyncInstitutions(int page = 0);
        Task AddAsyncDonation(DonationViewModel donation);
    }
}