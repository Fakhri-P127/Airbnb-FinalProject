using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Update
{
    public class UpdateGuestReviewCommand:IRequest<GuestReviewResponse>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public float? GuestScore { get; set; }
    }
}
