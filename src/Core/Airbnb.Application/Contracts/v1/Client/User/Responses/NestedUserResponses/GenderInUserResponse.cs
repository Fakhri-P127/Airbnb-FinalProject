using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.User.Responses.NestedUserResponses
{
    public class GenderInUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
