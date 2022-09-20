using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.Hosts.Commands.Create
{
    public class CreateHostCommand:IRequest<PostHostResponse>
    {
        // hansi usere aiddi
        public string AppUserId { get; set; }

    }
}
