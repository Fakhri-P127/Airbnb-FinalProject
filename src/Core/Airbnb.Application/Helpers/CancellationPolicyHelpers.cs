using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class CancellationPolicyHelpers
    {
        public async static Task<CancellationPolicyResponse> ReturnResponse(CancellationPolicy cancellationPolicy,
          IUnitOfWork _unit, IMapper _mapper)
        {
            cancellationPolicy = await _unit.CancellationPolicyRepository
                .GetByIdAsync(cancellationPolicy.Id, null, false, "Properties");
            CancellationPolicyResponse response = _mapper.Map<CancellationPolicyResponse>(cancellationPolicy);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
       
    }
}
