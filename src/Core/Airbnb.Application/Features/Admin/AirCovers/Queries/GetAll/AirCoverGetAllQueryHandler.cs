using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.AirCovers.Queries.GetAll
{
    public class AirCoverGetAllQueryHandler : IRequestHandler<AirCoverGetAllQuery, List<AirCoverResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public AirCoverGetAllQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<AirCoverResponse>> Handle(AirCoverGetAllQuery request, CancellationToken cancellationToken)
        {
            List<AirCover> airCovers = await _unit.AirCoverRepository.GetAllAsync(request.Expression);
            List<AirCoverResponse> responses = _mapper.Map<List<AirCoverResponse>>(airCovers);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
