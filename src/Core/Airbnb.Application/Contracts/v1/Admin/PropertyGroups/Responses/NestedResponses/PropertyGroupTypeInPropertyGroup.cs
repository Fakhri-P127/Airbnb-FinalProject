using Airbnb.Domain.Entities.PropertyRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses.NestedResponses
{
    public class PropertyGroupTypeInPropertyGroup
    {
        public PropertyTypeInPropertyGroupType PropertyType { get; set; }
        public Guid PropertyTypeId { get; set; }
    }
}
