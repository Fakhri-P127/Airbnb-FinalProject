using Microsoft.AspNetCore.Http;
using System.Net;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class User_IsLockedOutException : Exception, IServiceException
    {
        //419 yazmaq isteyirdim httpstatuscode da yoxdu
        public HttpStatusCode StatusCode => HttpStatusCode.Locked;

        public string ErrorMessage => "This user is locked out due to too many attempts of wrong loging in";
    }
}
