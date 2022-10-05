using System.Net;

namespace Airbnb.Application.Exceptions.PropertyReviews
{
    public class PropertyReview_NotAvailableYetException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage { get; set; }
        public PropertyReview_NotAvailableYetException(DateTime checkOutDate)
        {
            ErrorMessage = $"Reservation hasn't finished yet. You can't leave a review until {checkOutDate} this date.";
        }
    }
}
