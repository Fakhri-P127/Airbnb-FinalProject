using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Create
{
    public class CreateCancellationPolicyCommandHandler : IRequestHandler<CreateCancellationPolicyCommand, CancellationPolicyResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateCancellationPolicyCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<CancellationPolicyResponse> Handle(CreateCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            CancellationPolicy cancellationPolicy = _mapper.Map<CancellationPolicy>(request);
            await _unit.CancellationPolicyRepository.AddAsync(cancellationPolicy);
            cancellationPolicy = await _unit.CancellationPolicyRepository
                .GetByIdAsync(cancellationPolicy.Id,null,"Property");
            CancellationPolicyResponse response = _mapper.Map<CancellationPolicyResponse>(cancellationPolicy);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
