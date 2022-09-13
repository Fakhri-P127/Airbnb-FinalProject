using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Delete
{
    public class DeleteReservationCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteReservationCommand(Guid id)
        {
            Id = id;
        }
    }
}
