using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Exceptions.Amenities;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Amenities.Queries.GetById
{
    public class GetByIdAmenityQueryHandler : IRequestHandler<GetByIdAmenityQuery, GetAmenityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetByIdAmenityQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GetAmenityResponse> Handle(GetByIdAmenityQuery request, CancellationToken cancellationToken)
        {
            Amenity amenity = await _unit.AmenityRepository.GetByIdAsync(request.Id, request.Expression,false,
                "AmenityType", "PropertyAmenities");
            if (amenity is null) throw new AmenityNotFoundException();
            GetAmenityResponse response = _mapper.Map<GetAmenityResponse>(amenity);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
