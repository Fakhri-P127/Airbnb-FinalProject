using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Exceptions.Settings;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Commands.Delete
{
    public class DeleteSettingsCommandHandler:IRequestHandler<DeleteSettingsCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteSettingsCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteSettingsCommand request, CancellationToken cancellationToken)
        {
            Settings settings = await _unit.SettingsRepository.GetByIdAsync(request.Id, null, true);
            if (settings is null) throw new SettingsNotFoundException();
            await _unit.SettingsRepository.DeleteAsync(settings);
            return await Task.FromResult(Unit.Value);
        }
    }
}
