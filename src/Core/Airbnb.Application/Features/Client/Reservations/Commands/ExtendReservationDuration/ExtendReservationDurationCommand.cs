using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.ExtendReservationDuration
{
    public class ExtendReservationDurationCommand:IRequest<PostReservationResponse>
    {
        public DateTime CheckOutDate { get; set; }
    }
}
