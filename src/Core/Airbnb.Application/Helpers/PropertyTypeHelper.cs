using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class PropertyTypeHelper
    {
        public async static Task<PostPropertyTypeResponse> ReturnResponse(PropertyType propertyType,
       IUnitOfWork _unit, IMapper _mapper)
        {
            propertyType = await _unit.PropertyTypeRepository.GetByIdAsync(propertyType.Id, null);
            PostPropertyTypeResponse response = _mapper.Map<PostPropertyTypeResponse>(propertyType);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
        public static string[] AllPropertyTypeIncludes()
        {
            string[] includes = new[] {  "Properties","PropertyGroup","PropertyGroup.Properties"};
            return includes;
        }
       
     
    }
}
