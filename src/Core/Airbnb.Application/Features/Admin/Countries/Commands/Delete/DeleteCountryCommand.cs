using MediatR;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Delete
{
    public class DeleteCountryCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteCountryCommand(Guid id)
        {
            Id = id;
        }
    }
}
