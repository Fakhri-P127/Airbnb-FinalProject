using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class RegionHelper
    {
        public async static Task<RegionResponse> ReturnResponse(Region region,
        IUnitOfWork _unit, IMapper _mapper)
        {
            region = await _unit.RegionRepository.GetByIdAsync(region.Id, null,false,
                AllRegionIncludes());
            RegionResponse response = _mapper.Map<RegionResponse>(region);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }

        public static string[] AllRegionIncludes()
        {
            string[] includes = new[] {  "Countries", "Countries.Cities","States","States.Properties" };
            return includes;
        }
    }
}
