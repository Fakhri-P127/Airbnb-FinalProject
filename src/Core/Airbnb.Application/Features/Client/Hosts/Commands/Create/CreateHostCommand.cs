using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.Hosts.Commands.Create
{
    public class CreateHostCommand:IRequest<PostHostResponse>
    {
        // validasiya ya ehtiyac yoxdu chunki action filterde edirem AppUserId lerin validasiyasini
        //public Guid AppUserId { get; set; }

    }
}
