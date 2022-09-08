using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses.NestedResponses;
using Airbnb.Application.Contracts.v1.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses
{
    public class PostPropertyGroupResponse:BaseResponse
    {
        public string Image { get; set; }
        public string Name { get; set; }
      
    }
}
