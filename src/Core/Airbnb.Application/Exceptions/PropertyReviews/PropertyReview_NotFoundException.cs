using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Exceptions.PropertyReviews
{
    public class PropertyReview_NotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; set; }
        public PropertyReview_NotFoundException(Guid propertyReviewId)
        {
            ErrorMessage = $"Property review with this Id({propertyReviewId}) doesn't exist. Please enter valid Id";
        }
    }
}
