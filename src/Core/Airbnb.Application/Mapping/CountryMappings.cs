using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses.NestedResponses;
using Airbnb.Application.Features.Admin.Countries.Commands.Create;
using Airbnb.Application.Features.Admin.Countries.Commands.Update;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class CountryMappings : Profile
    {
        public CountryMappings()
        {
            CreateMap<Country, CountryResponse>();
            CreateMap<City, CityInCountryResponse>();
            CreateMap<Region, RegionInCountryResponse>()
                .ForMember(dest => dest.CountriesCount, opt => opt.MapFrom(src => src.Countries.Count));
            CreateMap<State, StateInCountryResponse>()
                .ForMember(dest => dest.PropertiesCount, opt => opt.MapFrom(src => src.Properties.Count));

            CreateMap<CreateCountryCommand, Country>();
            CreateMap<UpdateCountryCommand, Country>()
                .ForMember(dest => dest.RegionId, opt =>
                {
                    opt.PreCondition(src => src.RegionId != null);
                    opt.MapFrom(src => (Guid)src.RegionId);
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
