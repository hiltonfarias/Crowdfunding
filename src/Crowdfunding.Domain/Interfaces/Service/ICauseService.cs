using System.Collections.Generic;
using System.Threading.Tasks;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Domain
{
    public interface ICauseService
    {
         Task Add(CauseViewModel causeViewModel);
         Task<IEnumerable<CauseViewModel>> RecoverCauses();
    }
}