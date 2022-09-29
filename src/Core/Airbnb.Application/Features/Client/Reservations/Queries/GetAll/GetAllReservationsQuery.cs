using Airbnb.Application.Contracts.v1.Client.Reservation.Parameters;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.Reservations.Queries.GetAll
{
    public class GetAllReservationsQuery:IRequest<List<GetReservationResponse>>
    {
        public ReservationParameters Parameters { get; set; }
        public Expression<Func<Reservation, bool>> Expression { get; set; }
        public GetAllReservationsQuery(ReservationParameters parameters,Expression<Func<Reservation, bool>> expression = null)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}
