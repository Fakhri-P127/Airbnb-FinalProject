using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Update
{
    public class UpdateCancellationPolicyCommandHandler : IRequestHandler<UpdateCancellationPolicyCommand, CancellationPolicyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UpdateCancellationPolicyCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<CancellationPolicyResponse> Handle(UpdateCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            CancellationPolicy cancellationPolicy = await _unit.CancellationPolicyRepository.GetByIdAsync(request.Id, null);
            if (cancellationPolicy is null) throw new NotFoundException("CancellationPolicy");
            _unit.CancellationPolicyRepository.Update(cancellationPolicy);
            _mapper.Map(request, cancellationPolicy);
            await _unit.SaveChangesAsync();
            cancellationPolicy = await _unit.CancellationPolicyRepository
                .GetByIdAsync(cancellationPolicy.Id, null,"Properties");
            CancellationPolicyResponse response = _mapper.Map<CancellationPolicyResponse>(cancellationPolicy);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
