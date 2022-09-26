using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses.NestedResponses;
using Airbnb.Application.Features.Admin.Cities.Commands.Create;
using Airbnb.Application.Features.Admin.Cities.Commands.Update;
using Airbnb.Application.Features.Admin.Countries.Commands.Create;
using Airbnb.Application.Features.Admin.Countries.Commands.Update;
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

            CreateMap<CreateCityCommand, City>();
            CreateMap<UpdateCityCommand, City>()
                .ForMember(dest => dest.CountryId, opt =>
                {
                    opt.PreCondition(src => src.CountryId != null);
                    opt.MapFrom(src => (Guid)src.CountryId);
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
