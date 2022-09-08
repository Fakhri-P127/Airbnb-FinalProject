using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses.NestedResponses
{
    public class PropertyGroupInPropertyGroupType
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int PropertyCount { get; set; }

    }
}
