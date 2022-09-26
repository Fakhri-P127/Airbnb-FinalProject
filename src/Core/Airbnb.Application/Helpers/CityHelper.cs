using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class CityHelper
    {
        public async static Task<CityResponse> ReturnResponse(City city,
        IUnitOfWork _unit, IMapper _mapper)
        {
            city = await _unit.CityRepository.GetByIdAsync(city.Id, null,false,
                AllCityIncludes());
            CityResponse response = _mapper.Map<CityResponse>(city);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }

        public static string[] AllCityIncludes()
        {
            string[] includes = new[] { "Country", "Country.Cities", "States", "States.Properties"};
            return includes;
        }
    }
}
