using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.AppUser
{
    public class UserIdNotFoundException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; set; } = "User with this Id doesn't exist. Please enter valid Id";
    }
}
