using Airbnb.Application.Contracts.v1.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses
{
    public class PrivacyTypeResponse:BaseResponse
    {
        public string Name { get; set; }
        public int PropertyCount { get; set; }
    }
}
