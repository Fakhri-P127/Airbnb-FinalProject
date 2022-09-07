using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Delete
{
    public class DeleteCancellationPolicyCommand:IRequest
    {
        public Guid Id { get; set; }
        public DeleteCancellationPolicyCommand(Guid id)
        {
            Id = id;
        }
    }
}
