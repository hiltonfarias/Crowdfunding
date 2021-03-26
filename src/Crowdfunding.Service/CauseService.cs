using AutoMapper;

using System.Collections.Generic;
using System.Threading.Tasks;

using Crowdfunding.Domain;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Service
{
    public class CauseService : ICauseService
    {
        private readonly IMapper _mapper;
        private readonly ICauseRepository _causeRepository;

        public CauseService(IMapper mapper, ICauseRepository causeRepository)
        {
            _mapper = mapper;
            _causeRepository = causeRepository;
        }

        public async Task Add(CauseViewModel causeViewModel)
        {
            await _causeRepository.Add(_mapper.Map<CauseViewModel, Cause>(causeViewModel));
        }

        public async Task<IEnumerable<CauseViewModel>> RecoverCauses()
        {
            return _mapper.Map<IEnumerable<Cause>, IEnumerable<CauseViewModel>>(await _causeRepository.RecoverCause());
        }
    }
}