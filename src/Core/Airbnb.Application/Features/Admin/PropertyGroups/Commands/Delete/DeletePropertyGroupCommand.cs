using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Delete
{
    public class DeletePropertyGroupCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeletePropertyGroupCommand(Guid id)
        {
            Id = id;
        }
    }
}
