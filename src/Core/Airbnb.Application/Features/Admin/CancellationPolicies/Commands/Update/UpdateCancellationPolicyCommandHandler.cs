using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Exceptions.CancellationPolicies;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _accessor;

        public UpdateCancellationPolicyCommandHandler(IUnitOfWork unit,IMapper mapper,
            IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<CancellationPolicyResponse> Handle(UpdateCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            // guid olmadan gondersem evvelceden tutacaq ve bu error hech vaxt ishlemeyecek amma yenede her ehtimala qarshi yazdim
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            CancellationPolicy cancellationPolicy = await _unit.CancellationPolicyRepository
                .GetByIdAsync(Id, null);
            if (cancellationPolicy is null) throw new CancellationPolicyNotFoundException();
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
