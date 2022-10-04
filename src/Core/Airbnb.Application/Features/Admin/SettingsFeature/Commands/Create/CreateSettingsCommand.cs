using Airbnb.Application.Contracts.v1.Admin.Settings.Responses;
using MediatR;

namespace Airbnb.Application.Features.Admin.SettingsFeature.Commands.Create
{
    public class CreateSettingsCommand:IRequest<SettingsResponse>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
