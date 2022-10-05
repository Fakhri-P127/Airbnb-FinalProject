using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Create
{
    public class CreateGuestReviewCommand:IRequest<GuestReviewResponse>
    {
        public string Text { get; set; }
        public float GuestScore { get; set; }
        //public Guid HostId { get; set; }
        // guest
        //public Guid AppUserId { get; set; }
        // booking e gore bunu yaza bilsin.
        public Guid ReservationId { get; set; }
    }
}
