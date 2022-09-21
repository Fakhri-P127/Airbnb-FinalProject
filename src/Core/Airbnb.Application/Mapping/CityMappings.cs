using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses.NestedResponses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class CityMappings:Profile
    {
        public CityMappings()
        {
            CreateMap<City, CityResponse>();
            CreateMap<Country, CountryInCityResponse>()
                .ForMember(dest=>dest.CitiesCount,opt=>opt.MapFrom(src=>src.Cities.Count));
            CreateMap<State, StateInCityResponse>()
                .ForMember(dest=>dest.PropertiesCount,opt=>opt.MapFrom(src=>src.Properties.Count));

        }
    }
}
