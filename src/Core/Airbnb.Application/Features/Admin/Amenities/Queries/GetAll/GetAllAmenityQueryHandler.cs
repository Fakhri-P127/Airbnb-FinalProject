using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Amenities.Queries.GetAll
{
    public class GetAllAmenityQueryHandler : IRequestHandler<GetAllAmenityQuery, List<GetAmenityResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllAmenityQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<GetAmenityResponse>> Handle(GetAllAmenityQuery request, CancellationToken cancellationToken)
        {
            List<Amenity> amenities = await _unit.AmenityRepository.GetAllAsync(request.Expression,"PropertyAmenities");
            List<GetAmenityResponse> responses = _mapper.Map<List<GetAmenityResponse>>(amenities);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
