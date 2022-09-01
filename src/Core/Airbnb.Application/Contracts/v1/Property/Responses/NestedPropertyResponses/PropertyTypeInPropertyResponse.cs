using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Property.Responses.NestedPropertyResponses
{
    public class PropertyTypeInPropertyResponse
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
