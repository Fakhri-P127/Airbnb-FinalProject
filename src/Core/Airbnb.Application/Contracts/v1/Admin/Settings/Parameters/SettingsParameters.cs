using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Contracts.v1.Admin.Settings.Parameters
{
    public class SettingsParameters:BaseQueryStringParameters
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
