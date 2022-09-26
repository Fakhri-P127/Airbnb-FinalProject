using MediatR;

namespace Airbnb.Application.Features.Admin.Regions.Commands.Delete
{
    public class DeleteRegionCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteRegionCommand(Guid id)
        {
            Id = id;
        }
    }
}
