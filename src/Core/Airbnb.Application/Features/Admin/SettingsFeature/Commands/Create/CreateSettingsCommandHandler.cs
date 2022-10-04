using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Exceptions.Settings;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Commands.Create
{
    public class CreateSettingsCommandHandler : IRequestHandler<CreateSettingsCommand, SettingsResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateSettingsCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<SettingsResponse> Handle(CreateSettingsCommand request, CancellationToken cancellationToken)
        {
            Settings settings = await _unit.SettingsRepository.GetSingleAsync(x => x.Key == request.Key);
            if (settings is not null) throw new SettingsDuplicateKeyException();
            settings = _mapper.Map<Settings>(request);
            await _unit.SettingsRepository.AddAsync(settings);
            return await SettingsHelpers.ReturnResponse(settings, _unit, _mapper);
        }
    }
}
