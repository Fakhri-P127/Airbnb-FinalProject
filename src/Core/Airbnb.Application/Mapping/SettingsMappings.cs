using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using Airbnb.Application.Features.Admin.SettingsFeature.Commands.Create;
using Airbnb.Application.Features.Admin.SettingsFeature.Commands.Update;
using Airbnb.Domain.Entities.Common;
using AutoMapper;

namespace Airbnb.Application.Mapping
{
    public class SettingsMappings:Profile
    {
        public SettingsMappings()
        {
            CreateMap<CreateSettingsCommand, Settings>();
            CreateMap<UpdateSettingsCommand, Settings>();

            CreateMap<Settings, SettingsResponse>();
        }
    }
}
