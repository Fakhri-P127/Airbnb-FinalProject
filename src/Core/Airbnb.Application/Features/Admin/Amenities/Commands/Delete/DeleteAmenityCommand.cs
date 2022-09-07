using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Commands.Delete
{
    public class DeleteAmenityCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteAmenityCommand(Guid id) => Id = id;
    }
}
