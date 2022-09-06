using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Delete
{
    public class DeleteAirCoverCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteAirCoverCommand(Guid id)
        {
            Id = id;
        }
    }
}
