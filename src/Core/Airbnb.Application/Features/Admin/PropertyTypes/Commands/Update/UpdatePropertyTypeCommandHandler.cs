using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Exceptions.PropertyTypes;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Update
{
    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, PostPropertyTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PostPropertyTypeResponse> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            PropertyType propertyType = await _unit.PropertyTypeRepository.GetByIdAsync(request.Id, null);
            if (propertyType is null) throw new PropertyTypeNotFoundException();
            _unit.PropertyTypeRepository.Update(propertyType, false);
            _mapper.Map(request, propertyType);
            await _unit.SaveChangesAsync();
            propertyType = await _unit.PropertyTypeRepository.GetByIdAsync(propertyType.Id, null);
            PostPropertyTypeResponse response = _mapper.Map<PostPropertyTypeResponse>(propertyType);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
