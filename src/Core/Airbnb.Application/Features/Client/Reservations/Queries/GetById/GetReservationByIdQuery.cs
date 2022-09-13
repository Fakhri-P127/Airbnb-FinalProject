using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Queries.GetById
{
    public class GetReservationByIdQuery:IRequest<GetReservationResponse>
    {
        public Guid Id { get; set; }
        public Expression<Func<Reservation, bool>> Expression { get; set; }
        public GetReservationByIdQuery(Guid id,Expression<Func<Reservation, bool>> expression = null)
        {
            Id = id;
            Expression = expression;
        }
    }
}
