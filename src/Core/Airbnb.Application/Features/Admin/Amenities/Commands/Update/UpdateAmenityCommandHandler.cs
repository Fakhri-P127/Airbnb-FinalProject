using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Exceptions.Amenities;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Update
{
    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand, PostAmenityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateAmenityCommandHandler(IUnitOfWork unit,IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<PostAmenityResponse> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        { 
            // guid olmadan gondersem evvelceden tutacaq ve bu error hech vaxt ishlemeyecek amma yenede her ehtimala qarshi yazdim
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            Amenity amenity = await _unit.AmenityRepository.GetByIdAsync(Id, null,true);
            if (amenity is null) throw new AmenityNotFoundException();
            _unit.AmenityRepository.Update(amenity);
            _mapper.Map(request, amenity);
            await _unit.SaveChangesAsync();
            return await AmenityHelpers.ReturnResponse(amenity, _unit, _mapper);
        }
    }
}
