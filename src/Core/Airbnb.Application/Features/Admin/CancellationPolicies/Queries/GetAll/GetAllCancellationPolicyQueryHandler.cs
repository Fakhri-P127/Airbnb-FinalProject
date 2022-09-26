using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetAll
{
    public class GetAllCancellationPolicyQueryHandler : IRequestHandler<GetAllCancellationPolicyQuery, List<CancellationPolicyResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllCancellationPolicyQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<CancellationPolicyResponse>> Handle(GetAllCancellationPolicyQuery request, CancellationToken cancellationToken)
        {
            List<CancellationPolicy> cancellationPolicies = await _unit.CancellationPolicyRepository
                 .GetAllAsync(request.Expression,false, "Properties");
            List<CancellationPolicyResponse> responses = _mapper
                .Map<List<CancellationPolicyResponse>>(cancellationPolicies);
            if (responses is null) throw new Exception("Internal Server Error");
            return responses;
        }
    }
}
