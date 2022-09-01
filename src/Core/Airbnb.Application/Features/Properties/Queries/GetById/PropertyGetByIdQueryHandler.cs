using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Property.Responses;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Property;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Properties.Queries.GetById
{
    public class PropertyGetByIdQueryHandler : IRequestHandler<PropertyGetByIdQuery, PropertyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public PropertyGetByIdQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PropertyResponse> Handle(PropertyGetByIdQuery request, CancellationToken cancellationToken)
        {
            Property property = await _unit.PropertyRepository.GetByIdAsync(request.Id, null
                , FileHelpers.AllPropertyRelationIncludes());

            if (property is null) throw new PropertyNotFoundException();
            PropertyResponse response = _mapper.Map<PropertyResponse>(property);
            return response;
        }
    }
}
