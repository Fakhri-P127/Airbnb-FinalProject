using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Update
{
    public class UpdateReservationCommand:IRequest<PostReservationResponse>
    {
        public Guid Id { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public int PetCount { get; set; }
    }
}
