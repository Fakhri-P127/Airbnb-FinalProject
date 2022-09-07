using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Update
{
    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand, PostAmenityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UpdateAmenityCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PostAmenityResponse> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            Amenity amenity = await _unit.AmenityRepository.GetByIdAsync(request.Id, null);
            if (amenity is null) throw new NotFoundException("Amenity");
            _unit.AmenityRepository.Update(amenity);
            _mapper.Map(request, amenity);
            await _unit.SaveChangesAsync();
            amenity = await _unit.AmenityRepository.GetByIdAsync(amenity.Id, null);
            PostAmenityResponse response = _mapper.Map<PostAmenityResponse>(amenity);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
