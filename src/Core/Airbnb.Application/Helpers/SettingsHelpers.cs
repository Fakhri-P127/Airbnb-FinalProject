using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Domain.Entities.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class SettingsHelpers
    {
        public async static Task<SettingsResponse> ReturnResponse(Settings settings,
       IUnitOfWork _unit, IMapper _mapper)
        {
            settings = await _unit.SettingsRepository
                .GetByIdAsync(settings.Id, null, false);
            SettingsResponse response = _mapper.Map<SettingsResponse>(settings);
            return response;
        }
    }
}
