using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class AmenityHelpers
    {
        public async static Task<PostAmenityResponse> ReturnResponse(Amenity amenity,
          IUnitOfWork _unit, IMapper _mapper)
        {
            amenity = await _unit.AmenityRepository.GetByIdAsync(amenity.Id, null, false, "AmenityType");
            PostAmenityResponse response = _mapper.Map<PostAmenityResponse>(amenity);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
    
}
