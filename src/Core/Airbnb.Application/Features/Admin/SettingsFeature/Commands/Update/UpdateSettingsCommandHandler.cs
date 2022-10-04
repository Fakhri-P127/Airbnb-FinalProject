using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Exceptions.Settings;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Commands.Update
{
    public class UpdateSettingsCommandHandler : IRequestHandler<UpdateSettingsCommand, SettingsResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateSettingsCommandHandler(IUnitOfWork unit, IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<SettingsResponse> Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
        {
            Settings settings = await CheckExceptionsThenReturnSettings(request);
            _unit.SettingsRepository.Update(settings, false);
            settings = _mapper.Map<Settings>(request);
            await _unit.SaveChangesAsync();
            return await SettingsHelpers.ReturnResponse(settings, _unit, _mapper);
        }

        private async Task<Settings> CheckExceptionsThenReturnSettings(UpdateSettingsCommand request)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            Settings settings = await _unit.SettingsRepository.GetByIdAsync(Id, null, true);
            if (settings is null) throw new SettingsNotFoundException();
            if (await _unit.SettingsRepository.GetSingleAsync(x => x.Key == request.Key) is not null)
                throw new SettingsDuplicateKeyException();
            return settings;
        }
    }
}
