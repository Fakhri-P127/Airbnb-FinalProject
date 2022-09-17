using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Update
{
    public class UpdateGuestReviewCommandValidator:AbstractValidator<UpdateGuestReviewCommand>
    {
        public UpdateGuestReviewCommandValidator()
        {
            RuleFor(x => x.GuestScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
            RuleFor(x => x.Text).Length(3, 250);
        }
    }
}
