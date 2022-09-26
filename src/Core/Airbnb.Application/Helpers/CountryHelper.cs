using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class CountryHelper
    {
        public async static Task<CountryResponse> ReturnResponse(Country country,
        IUnitOfWork _unit, IMapper _mapper)
        {
            country = await _unit.CountryRepository.GetByIdAsync(country.Id, null,false,
                AllCountryIncludes());
            CountryResponse response = _mapper.Map<CountryResponse>(country);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }

        public static string[] AllCountryIncludes()
        {
            string[] includes = new[] { "Region", "Region.Countries","Cities","States",
                "States.Properties"};
            return includes;
        }
    }
}
