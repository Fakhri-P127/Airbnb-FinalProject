using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses
{
    public class GetPropertyGroupResponse:BaseResponse
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int PropertyCount { get; set; }
        public List<PropertyTypeInPropertyGroup> PropertyTypes { get; set; }
    }
}
