using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses
{
    public class CancellationPolicyInPropertyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullRefund { get; set; }
        public string PartialRefund { get; set; }
        public string NoRefund { get; set; }
    }
}
