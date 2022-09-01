using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Property.Responses;
using Airbnb.Application.Contracts.v1.User.Responses;
using Airbnb.Application.Features.User.Queries.GetAll;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.Property;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Properties.Queries.GetAll
{
    public class PropertyGetAllQueryHandler : IRequestHandler<PropertyGetAllQuery, List<PropertyResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public PropertyGetAllQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
       
        public async Task<List<PropertyResponse>> Handle(PropertyGetAllQuery request, CancellationToken cancellationToken)
        {
            List<Property> properties = await _unit.PropertyRepository
                .GetAllAsync(null, FileHelpers.AllPropertyRelationIncludes());

            List<PropertyResponse> response = _mapper.Map<List<PropertyResponse>>(properties);

            return response;
        }
    }
}
