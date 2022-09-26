using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;

namespace Airbnb.Application.Features.Client.Properties.Commands.UpdatePendingStatus
{
    public class UpdatePropertyPendingStatusCommandHandler : IRequestHandler<UpdatePropertyPendingStatusCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdatePropertyPendingStatusCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(UpdatePropertyPendingStatusCommand request, CancellationToken cancellationToken)
        {
            Property property = await _unit.PropertyRepository.GetByIdAsync(request.Id, null,true
                ,"Host","Host.AppUser");
            if (property is null) throw new PropertyNotFoundException();

            if (property.Host.AppUser.EmailConfirmed || property.Host.AppUser.PhoneNumberConfirmed)
            { 
                property.IsDisplayed = true;
            }
            else
            {
                throw new Property_PendingStatusNotChangedException();
            }
            await _unit.SaveChangesAsync();
            return await Task.FromResult(Unit.Value);
        }
    }
}
