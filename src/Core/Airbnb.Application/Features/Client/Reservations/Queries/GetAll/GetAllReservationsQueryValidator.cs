using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Parameters;
using FluentValidation;

namespace Airbnb.Application.Features.Client.Reservations.Queries.GetAll
{
    public class GetAllReservationsQueryValidator:AbstractValidator<GetAllReservationsQuery>
    {
        public GetAllReservationsQueryValidator()
        {
            RuleFor(x => x.Parameters)
                .SetValidator(new ReservationParameterValidator());    
        }
    }
}
