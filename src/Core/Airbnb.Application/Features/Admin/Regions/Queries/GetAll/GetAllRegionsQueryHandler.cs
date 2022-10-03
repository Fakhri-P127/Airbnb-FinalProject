using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Regions.Queries.GetAll
{
    public class GetAllRegionsQueryHandler : IRequestHandler<GetAllRegionsQuery, List<RegionResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllRegionsQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<RegionResponse>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            List<Region> regions = await _unit.RegionRepository
               .GetAllAsync(request.Expression,request.Parameters,false,RegionHelper.AllRegionIncludes());

            List<RegionResponse> responses = _mapper.Map<List<RegionResponse>>(regions);
            //if (!responses.Any()) throw new Exception("Internal server error");
            return responses;
        }
    }
}
