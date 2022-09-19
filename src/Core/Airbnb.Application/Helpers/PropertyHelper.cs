using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class PropertyHelper
    {
        public async static Task<CreatePropertyResponse> ReturnResponse(Property property,
          IUnitOfWork _unit, IMapper _mapper)
        {
            property = await _unit.PropertyRepository.GetByIdAsync(property.Id, null,
                AllPropertyIncludes());
            CreatePropertyResponse response = _mapper.Map<CreatePropertyResponse>(property);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }

        public static string[] AllPropertyIncludes()
        {
            string[] includes = new[] { "PropertyImages"
                , "PropertyAmenities", "PropertyAmenities.Amenity", "PropertyGroup", "PropertyType", "AirCover"
                , "CancellationPolicy", "PrivacyType","Host","Reservations","Host.AppUser" };

            return includes;
        }
    }
}
