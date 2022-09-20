using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses
{
    public class PropertyImagesInPropertyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Alternative { get; set; }
        public bool? IsMain { get; set; }
    }
}
