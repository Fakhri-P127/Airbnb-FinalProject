using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Commands.Delete
{
    public class DeleteAmenityTypeCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteAmenityTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
