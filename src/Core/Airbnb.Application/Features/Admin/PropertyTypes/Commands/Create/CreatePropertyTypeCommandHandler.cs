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

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Create
{
    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, PostPropertyTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreatePropertyTypeCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<PostPropertyTypeResponse> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var existed = await _unit.PropertyTypeRepository.GetAllAsync(x => x.Name == request.Name);
            if (existed.Any())
                throw new DuplicatePropertyTypeNameValidationException();
            PropertyType propertyType = _mapper.Map<PropertyType>(request);
            await _unit.PropertyTypeRepository.AddAsync(propertyType);
            propertyType = await _unit.PropertyTypeRepository.GetByIdAsync(propertyType.Id, null);
            PostPropertyTypeResponse response = _mapper.Map<PostPropertyTypeResponse>(propertyType);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
