using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetAll
{
    public class GetAllPrivacyTypeQueryHandler : IRequestHandler<GetAllPrivacyTypeQuery, List<PrivacyTypeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetAllPrivacyTypeQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<List<PrivacyTypeResponse>> Handle(GetAllPrivacyTypeQuery request, CancellationToken cancellationToken)
        {
            List<PrivacyType> privacyTypes = await _unit.PrivacyTypeRepository
                .GetAllAsync(request.Expression,"Properties");
            
            List<PrivacyTypeResponse> responses = _mapper.Map<List<PrivacyTypeResponse>>(privacyTypes);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
