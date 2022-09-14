using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Create
{
    public class CreateGuestReviewCommandValidator:AbstractValidator<CreateGuestReviewCommand>
    {
        public CreateGuestReviewCommandValidator()
        {
            RuleFor(x=>x.GuestScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotEmpty();
            RuleFor(x=>x.Text).Length(3,250).NotEmpty();
            RuleFor(x=>x.AppUserId).NotEmpty();
            RuleFor(x=>x.AppUserId).NotEmpty();
            RuleFor(x=>x.HostId).NotEmpty();
            RuleFor(x=>x.ReservationId).NotEmpty();
        }
    }
}
