using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetAll
{
    public class GetAllPropertyGroupsQueryHandler : IRequestHandler<GetAllPropertyGroupsQuery, List<GetPropertyGroupResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetAllPropertyGroupsQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<List<GetPropertyGroupResponse>> Handle(GetAllPropertyGroupsQuery request, CancellationToken cancellationToken)
        {
            List<PropertyGroup> propertyGroups = await _unit.PropertyGroupRepository
              .GetAllAsync(request.Expression,request.Parameters,false, PropertyGroupHelper.AllPropertyGroupIncludes());
            List<GetPropertyGroupResponse> responses = _mapper.Map<List<GetPropertyGroupResponse>>(propertyGroups);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
