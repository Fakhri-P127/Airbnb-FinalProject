using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class User_PhoneNumberNotConfirmedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage => "PhoneNumber hasn't been verified yet. First, click the message that's been sent to your phone!";
    }
}
