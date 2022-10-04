using MediatR;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Commands.Delete
{
    public class DeleteSettingsCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteSettingsCommand(Guid id)
        {
            Id = id;
        }
    }
}
