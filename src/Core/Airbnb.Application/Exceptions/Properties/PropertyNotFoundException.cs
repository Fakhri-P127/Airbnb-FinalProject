using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.Properties
{
    public class PropertyNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Property with this id doesn't exist.";
    }
}
