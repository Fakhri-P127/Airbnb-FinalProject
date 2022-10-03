using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Regions.Queries.GetById
{
    public class GetRegionByIdQueryHandler : IRequestHandler<GetRegionByIdQuery, RegionResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetRegionByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<RegionResponse> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            Region region = await _unit.RegionRepository
               .GetByIdAsync(request.Id,request.Expression,false, RegionHelper.AllRegionIncludes());
            if (region is null) throw new RegionNotFoundException();
            RegionResponse response = _mapper.Map<RegionResponse>(region);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
