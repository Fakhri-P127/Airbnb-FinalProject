using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses.NestedResponses;
using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses
{
    public class GetPropertyGroupResponse
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int PropertyCount { get; set; }
        public List<PropertyGroupTypeInPropertyGroup> PropertyGroupTypes { get; set; }
    }
}
