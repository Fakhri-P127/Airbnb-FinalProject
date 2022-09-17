using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Update
{
    public class UpdatePropertyReviewCommandValidator:AbstractValidator<UpdatePropertyReviewCommand>
    {
        public UpdatePropertyReviewCommandValidator()
        {
            RuleFor(x => x.CleanlinessScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.AccuracyScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.LocationScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.CommunicationScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.CheckInScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.ValueScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.Text).Length(3, 350).NotEmpty();
        }
    }
}
