using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Exceptions.CancellationPolicies;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetById
{
    public class GetByIdCancellationPolicyQueryHandler : IRequestHandler<GetByIdCancellationPolicyQuery, CancellationPolicyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetByIdCancellationPolicyQueryHandler(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }
        public async Task<CancellationPolicyResponse> Handle(GetByIdCancellationPolicyQuery request, CancellationToken cancellationToken)
        {
            CancellationPolicy cancellationPolicy = await _unit.CancellationPolicyRepository
                .GetByIdAsync(request.Id, request.Expression, "Properties");
            if (cancellationPolicy is null) throw new CancellationPolicyNotFoundException();
            CancellationPolicyResponse response = _mapper.Map<CancellationPolicyResponse>(cancellationPolicy);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
