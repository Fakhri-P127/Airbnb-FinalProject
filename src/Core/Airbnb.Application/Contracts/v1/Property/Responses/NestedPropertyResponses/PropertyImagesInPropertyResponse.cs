using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Property.Responses.NestedPropertyResponses
{
    public class PropertyImagesInPropertyResponse
    {
        public string Name { get; set; }
        //public string Alternative { get; set; }
        public bool IsMain { get; set; }
    }
}
