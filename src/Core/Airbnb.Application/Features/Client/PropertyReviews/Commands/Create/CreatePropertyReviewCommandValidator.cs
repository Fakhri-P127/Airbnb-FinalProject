using Airbnb.Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Create
{
    public class CreatePropertyReviewCommandValidator:AbstractValidator<CreatePropertyReviewCommand>
    {
        private readonly IUnitOfWork _unit;

        public CreatePropertyReviewCommandValidator(IUnitOfWork unit)
        {
            _unit = unit;
            
            RuleFor(x => x.CleanlinessScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.AccuracyScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.LocationScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.CommunicationScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.CheckInScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.ValueScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.Text).Length(3, 350).NotEmpty();
            RuleFor(x => x.ReservationId).NotEmpty();

            //RuleFor(x => x.ReservationId).NotEmpty().MustAsync(async (x, cancellationToken) =>
            //{
            //    bool exists = await _unit.PropertyReviewRepository.GetByIdAsync(x, null) is not null;
            //    return exists;
            //});
        }
    }
}
