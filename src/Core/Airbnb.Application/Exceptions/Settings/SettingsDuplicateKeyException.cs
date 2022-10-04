using System.Net;

namespace Airbnb.Application.Exceptions.Settings
{
    public class SettingsDuplicateKeyException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Setting with this key already exists";
    }
}
