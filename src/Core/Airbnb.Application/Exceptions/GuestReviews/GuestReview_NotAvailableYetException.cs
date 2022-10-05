using System.Net;

namespace Airbnb.Application.Exceptions.GuestReviews
{
    public class GuestReview_NotAvailableYetException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; set; }
        public GuestReview_NotAvailableYetException(DateTime checkOutDate)
        {
            ErrorMessage = $"Reservation hasn't finished yet. You can't leave a review until {checkOutDate} this date. ";
        }
    }
}
