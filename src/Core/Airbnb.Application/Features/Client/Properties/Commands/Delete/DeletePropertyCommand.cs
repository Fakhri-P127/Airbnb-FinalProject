using MediatR;

namespace Airbnb.Application.Features.Client.Properties.Commands.Delete
{
    public class DeletePropertyCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeletePropertyCommand(Guid id)
        {
            Id = id;
        }
    }
}
