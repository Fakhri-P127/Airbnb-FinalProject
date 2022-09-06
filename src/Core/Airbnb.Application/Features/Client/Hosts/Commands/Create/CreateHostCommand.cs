using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Hosts.Commands.Create
{
    public class CreateHostCommand:IRequest<PostHostResponse>
    {
        // hansi usere aiddi
        public string AppUserId { get; set; }

    }
}
