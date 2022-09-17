using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.PropertyReviews
{
    public class PropertyReview_DuplicateValidationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage { get; set; }
        public PropertyReview_DuplicateValidationException(Guid propertyReviewId)
        {
            ErrorMessage = $"You have already written a property review for this reservation({propertyReviewId}).";
        }
    }
}
