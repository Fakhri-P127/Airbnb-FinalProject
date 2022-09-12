using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Mapping
{
    public class ReservationMappings:Profile
    {
        public ReservationMappings()
        {
            CreateMap<CreateReservationCommand, Reservation>();
            CreateMap<Reservation, PostReservationResponse>();

        }
    }
}
