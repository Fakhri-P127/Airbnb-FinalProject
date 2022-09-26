using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class AmenityTypeHelpers
    {
        public async static Task<AmenityTypeResponse> ReturnResponse(AmenityType amenityType,
          IUnitOfWork _unit, IMapper _mapper)
        {
            amenityType = await _unit.AmenityTypeRepository.GetByIdAsync(amenityType.Id, null,false, "Amenities");
            AmenityTypeResponse response = _mapper.Map<AmenityTypeResponse>(amenityType);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
       
    }
}
