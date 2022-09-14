using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.GuestReviews
{
    public class GuestReviewNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; set; }
        public GuestReviewNotFoundException(Guid guestReviewId)
        {
            ErrorMessage = $"Guest Review with this Id({guestReviewId}) doesn't exist. Please enter valid Id";
        }
    }
}
