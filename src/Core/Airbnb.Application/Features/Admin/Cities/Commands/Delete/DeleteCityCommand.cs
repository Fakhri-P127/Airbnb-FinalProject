using MediatR;

namespace Airbnb.Application.Features.Admin.Cities.Commands.Delete
{
    public class DeleteCityCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteCityCommand(Guid id)
        {
            Id = id;
        }
    }
}
