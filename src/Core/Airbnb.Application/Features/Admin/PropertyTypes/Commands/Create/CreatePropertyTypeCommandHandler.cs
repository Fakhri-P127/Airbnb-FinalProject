using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Exceptions.PropertyTypes;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

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
            if (await _unit.PropertyTypeRepository.GetSingleAsync(x => x.Name == request.Name) is not null)
                throw new DuplicatePropertyTypeNameValidationException();
            PropertyType propertyType = _mapper.Map<PropertyType>(request);
            await _unit.PropertyTypeRepository.AddAsync(propertyType);
            return await PropertyTypeHelper.ReturnResponse(propertyType, _unit, _mapper);

        }
    }
}
