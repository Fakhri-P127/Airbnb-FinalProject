using MediatR;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Delete
{
    public class DeleteAirCoverCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteAirCoverCommand(Guid id)
        {
            Id = id;
        }
    }
}
