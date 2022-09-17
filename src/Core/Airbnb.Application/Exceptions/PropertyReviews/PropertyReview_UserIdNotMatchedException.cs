using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.PropertyReviews
{
    public class PropertyReview_UserIdNotMatchedException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public PropertyReview_UserIdNotMatchedException(string propertyReviewUserId, string reservationUserId)
        {
            ErrorMessage = $"User Id of Property Review doesn't match with the Id of Reservation. {propertyReviewUserId} ❌ {reservationUserId}";
        }
    }
}
