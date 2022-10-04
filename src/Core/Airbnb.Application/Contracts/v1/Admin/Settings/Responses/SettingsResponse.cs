using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.Settings.Responses
{
    public class SettingsResponse:BaseResponse
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
