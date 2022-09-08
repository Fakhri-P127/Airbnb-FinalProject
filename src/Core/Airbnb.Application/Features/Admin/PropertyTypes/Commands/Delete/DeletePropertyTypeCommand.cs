using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Delete
{
    public class DeletePropertyTypeCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeletePropertyTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
