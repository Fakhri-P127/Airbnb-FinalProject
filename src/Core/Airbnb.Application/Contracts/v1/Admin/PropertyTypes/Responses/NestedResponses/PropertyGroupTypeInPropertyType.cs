using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses.NestedResponses
{
    public class PropertyGroupTypeInPropertyType
    {
        public PropertyGroupInPropertyGroupType PropertyGroup { get; set; }
        public Guid PropertyGroupId { get; set; }
    }
}
