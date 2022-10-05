using System.Net;

namespace Airbnb.Application.Exceptions.Properties
{
    public class Property_AuthenticatedHostIdDifferentFromPropertyHostIdException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Authenticated user id is not same with the property host Id. You can't change another users data.";
    }
}
