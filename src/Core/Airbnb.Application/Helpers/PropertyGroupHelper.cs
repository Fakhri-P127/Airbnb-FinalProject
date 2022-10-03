using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Helpers
{
    public static class PropertyGroupHelper
    {
        public async static Task<PostPropertyGroupResponse> ReturnResponse(PropertyGroup propertyGroup,
          IUnitOfWork _unit, IMapper _mapper)
        {
            propertyGroup = await _unit.PropertyGroupRepository.GetByIdAsync(propertyGroup.Id, null,false,
                AllPropertyGroupIncludes());
            PostPropertyGroupResponse response = _mapper.Map<PostPropertyGroupResponse>(propertyGroup);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
      
        public static string[] AllPropertyGroupIncludes()
        {
            string[] includes = new[] {  "Properties", "PropertyTypes",
              "PropertyTypes.Properties"};
            return includes;
        }
        
    }
}
