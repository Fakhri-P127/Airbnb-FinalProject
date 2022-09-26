using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetById
{
    public class GetByIdPrivacyTypeQueryHandler : IRequestHandler<GetByIdPrivacyTypeQuery, PrivacyTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetByIdPrivacyTypeQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PrivacyTypeResponse> Handle(GetByIdPrivacyTypeQuery request, CancellationToken cancellationToken)
        {
            PrivacyType privacyType = await _unit.PrivacyTypeRepository
                .GetByIdAsync(request.Id, request.Expression,false, "Properties");
            if (privacyType is null) throw new PrivacyTypeNotFoundException();
            PrivacyTypeResponse response = _mapper.Map<PrivacyTypeResponse>(privacyType);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
