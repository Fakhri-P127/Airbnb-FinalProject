using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Delete
{
    public class DeletePrivacyTypeCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeletePrivacyTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
