using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Amenities.Commands.Create
{
    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, PostAmenityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateAmenityCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PostAmenityResponse> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            Amenity amenity = _mapper.Map<Amenity>(request);
            await _unit.AmenityRepository.AddAsync(amenity);
            amenity = await _unit.AmenityRepository.GetByIdAsync(amenity.Id, null);
            PostAmenityResponse response = _mapper.Map<PostAmenityResponse>(amenity);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
