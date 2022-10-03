using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Exceptions.PropertyTypes;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetById
{
    public class GetByIdPropertyTypeQueryHandler : IRequestHandler<GetByIdPropertyTypeQuery, GetPropertyTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetByIdPropertyTypeQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<GetPropertyTypeResponse> Handle(GetByIdPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            PropertyType propertyType = await _unit.PropertyTypeRepository
               .GetByIdAsync(request.Id, request.Expression);
            if (propertyType is null) throw new PropertyTypeNotFoundException();
            GetPropertyTypeResponse response = _mapper.Map<GetPropertyTypeResponse>(propertyType);
            //if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
