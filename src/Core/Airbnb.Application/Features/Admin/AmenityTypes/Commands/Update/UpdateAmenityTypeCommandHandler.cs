using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
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

        public UpdateAmenityTypeCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<AmenityTypeResponse> Handle(UpdateAmenityTypeCommand request, CancellationToken cancellationToken)
        {
            AmenityType amenityType = await _unit.AmenityTypeRepository.GetByIdAsync(request.Id, null);
            if (amenityType is null) throw new NotFoundException("AmenityType");
            _unit.AmenityTypeRepository.Update(amenityType,false);
            amenityType.Name = request.Name;
            await _unit.SaveChangesAsync();
            amenityType = await _unit.AmenityTypeRepository.GetByIdAsync(amenityType.Id, null);
            AmenityTypeResponse response = _mapper.Map<AmenityTypeResponse>(amenityType);
            if (response is null) throw new Exception("Internal server error");

            return response;

        }
    }
}
