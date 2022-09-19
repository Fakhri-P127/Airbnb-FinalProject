using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Exceptions.PropertyGroups;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetById
{
    public class GetByIdPropertyGroupQueryHandler:IRequestHandler<GetByIdPropertyGroupQuery,GetPropertyGroupResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetByIdPropertyGroupQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<GetPropertyGroupResponse> Handle(GetByIdPropertyGroupQuery request, CancellationToken cancellationToken)
        {
            PropertyGroup propertyGroup = await _unit.PropertyGroupRepository
                .GetByIdAsync(request.Id, request.Expression,PropertyGroupHelper.AllPropertyGroupIncludes());
            if (propertyGroup is null) throw new PropertyGroupNotFoundException();
            GetPropertyGroupResponse response = _mapper.Map<GetPropertyGroupResponse>(propertyGroup);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
