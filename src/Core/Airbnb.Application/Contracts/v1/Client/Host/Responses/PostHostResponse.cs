using Airbnb.Application.Contracts.v1.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Host.Responses
{
    public class PostHostResponse:BaseResponse
    {
        // hansi usere aiddi
        public string AppUserId { get; set; }

        public bool IsSuperHost { get; set; } = false;

    }
}
