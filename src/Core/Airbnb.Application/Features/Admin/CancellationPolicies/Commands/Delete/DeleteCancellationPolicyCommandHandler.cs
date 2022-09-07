using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Delete
{
    public class DeleteCancellationPolicyCommandHandler : IRequestHandler<DeleteCancellationPolicyCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteCancellationPolicyCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            CancellationPolicy cancellationPolicy = await _unit.CancellationPolicyRepository.GetByIdAsync(request.Id, null);
            if (cancellationPolicy is null) throw new NotFoundException("CancellationPolicy");
            await _unit.CancellationPolicyRepository.DeleteAsync(cancellationPolicy);
            return await Task.FromResult(Unit.Value);
        }
    }
}
