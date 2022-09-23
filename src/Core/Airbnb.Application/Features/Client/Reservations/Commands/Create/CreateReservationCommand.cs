using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Create
{
    public class CreateReservationCommand:IRequest<PostReservationResponse>
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public int PetCount { get; set; }
        public Guid PropertyId { get; set; }

        // who's reserving
        public Guid AppUserId { get; set; }

        // who ownes it
        public Guid HostId { get; set; }
    }
}
