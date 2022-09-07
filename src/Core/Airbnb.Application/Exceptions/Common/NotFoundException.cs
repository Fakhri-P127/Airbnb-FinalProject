using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Common
{
    public class NotFoundException:Exception,IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public string ErrorMessage { get; set; }
        public NotFoundException(string type)
        {
            ErrorMessage = $"{type} with this id doesn't exist. Please enter valid Id";
        }
    }
}
