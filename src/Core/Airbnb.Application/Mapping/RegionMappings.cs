using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses.NestedResponses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class RegionMappings : Profile
    {
        public RegionMappings()
        {
            CreateMap<Region, RegionResponse>();
            CreateMap<Country, CountryInRegionResponse>()
                .ForMember(dest => dest.CitiesCount, opt => opt.MapFrom(src => src.Cities.Count));
            CreateMap<State, StateInRegionResponse>()
                .ForMember(dest => dest.PropertiesCount, opt => opt.MapFrom(src => src.Properties.Count));
        }
    }
}
