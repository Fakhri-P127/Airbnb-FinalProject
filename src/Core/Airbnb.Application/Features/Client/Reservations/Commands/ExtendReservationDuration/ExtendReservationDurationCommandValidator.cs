using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.ExtendReservationDuration
{
    public class ExtendReservationDurationCommandValidator:AbstractValidator<ExtendReservationDurationCommand>
    {
        public ExtendReservationDurationCommandValidator()
        {
            RuleFor(x => x.CheckOutDate).NotEmpty();
        }
    }
}
