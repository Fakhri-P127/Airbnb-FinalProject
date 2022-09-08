using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses
{
    public class GetPropertyTypeResponse:BaseResponse
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PropertyCount { get; set; }
        public List<PropertyGroupTypeInPropertyGroup> PropertyGroupTypes { get; set; }
    }
}
