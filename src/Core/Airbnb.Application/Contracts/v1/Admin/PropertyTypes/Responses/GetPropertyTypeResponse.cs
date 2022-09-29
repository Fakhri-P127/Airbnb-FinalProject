using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses
{
    public class GetPropertyTypeResponse:BaseResponse
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PropertyCount { get; set; }
        public PropertyGroupInPropertyType PropertyGroup { get; set; }
    }
}
