using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.Properties.Queries.GetById
{
    public class PropertyGetByIdQueryHandler : IRequestHandler<PropertyGetByIdQuery, GetPropertyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public PropertyGetByIdQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GetPropertyResponse> Handle(PropertyGetByIdQuery request, CancellationToken cancellationToken)
        {
            Property property = await _unit.PropertyRepository.GetByIdAsync(request.Id, request.Expression,false,
                 PropertyHelper.AllPropertyIncludes());

            if (property is null) throw new PropertyNotFoundException();
            GetPropertyResponse response = _mapper.Map<GetPropertyResponse>(property);
            return response;
        }
    }
}
