using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses.NestedResponses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class CountryMappings:Profile
    {
        public CountryMappings()
        {
            CreateMap<Country, CountryResponse>();
            CreateMap<City, CityInCountryResponse>();
            CreateMap<Region, RegionInCountryResponse>()
                .ForMember(dest=>dest.CountriesCount,opt=>opt.MapFrom(src=>src.Countries.Count));
            CreateMap<State, StateInCountryResponse>()
                .ForMember(dest=>dest.PropertiesCount,opt=>opt.MapFrom(src=>src.Properties.Count));

        }
    }
}
