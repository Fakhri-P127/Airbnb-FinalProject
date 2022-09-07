using Airbnb.Application.Contracts.v1.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses
{
    public class CancellationPolicyResponse:BaseResponse
    {
        public string Name { get; set; }
        public string FullRefund { get; set; }
        public string PartialRefund { get; set; }
        public string NoRefund { get; set; }
        public int PropertyCount { get; set; }
    }
}
