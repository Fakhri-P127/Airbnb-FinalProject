using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Commands.Create
{
    public class CreateAmenityTypeCommandHandler : IRequestHandler<CreateAmenityTypeCommand, AmenityTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateAmenityTypeCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<AmenityTypeResponse> Handle(CreateAmenityTypeCommand request, CancellationToken cancellationToken)
        {
            AmenityType amenityType = new()
            {
                Name = request.Name
            };
            await _unit.AmenityTypeRepository.AddAsync(amenityType);
            return await AmenityTypeHelpers.ReturnResponse(amenityType, _unit, _mapper);

        }
    }
}
