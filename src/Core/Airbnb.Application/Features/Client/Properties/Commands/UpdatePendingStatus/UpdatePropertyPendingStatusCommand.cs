using MediatR;

namespace Airbnb.Application.Features.Client.Properties.Commands.UpdatePendingStatus
{
    public class UpdatePropertyPendingStatusCommand:IRequest
    {
        public Guid Id { get; set; }
        public UpdatePropertyPendingStatusCommand(Guid id)
        {
            Id = id;
        }
    }
}
