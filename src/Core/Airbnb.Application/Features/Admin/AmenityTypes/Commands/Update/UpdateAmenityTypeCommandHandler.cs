using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Exceptions.Amenities;
using Airbnb.Application.Exceptions.AmenityTypes;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Commands.Update
{
    public class UpdateAmenityTypeCommandHandler:IRequestHandler<UpdateAmenityTypeCommand,AmenityTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateAmenityTypeCommandHandler(IUnitOfWork unit, IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<AmenityTypeResponse> Handle(UpdateAmenityTypeCommand request, CancellationToken cancellationToken)
        {
            AmenityType amenityType = await CheckThenReturnAmenityType(request);

            _unit.AmenityTypeRepository.Update(amenityType, false);
            amenityType.Name = request.Name;
            await _unit.SaveChangesAsync();
            return await AmenityTypeHelpers.ReturnResponse(amenityType, _unit, _mapper);

        }

        private async Task<AmenityType> CheckThenReturnAmenityType(UpdateAmenityTypeCommand request)
        {
            // guid olmadan gondersem evvelceden tutacaq ve bu error hech vaxt ishlemeyecek amma yenede her ehtimala qarshi yazdim
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            AmenityType amenityType = await _unit.AmenityTypeRepository.GetByIdAsync(Id, null, true);
            if (amenityType is null) throw new AmenityTypeNotFoundException();
            if (await _unit.AmenityTypeRepository.GetSingleAsync(x => x.Name == request.Name) is not null)
                throw new AmenityType_DuplicateNameException();
            return amenityType;
        }
    }
}
