using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Exceptions.CancellationPolicies;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

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
                .GetByIdAsync(Id, null,true);
            if (cancellationPolicy is null) throw new CancellationPolicyNotFoundException();
            _unit.CancellationPolicyRepository.Update(cancellationPolicy);
            _mapper.Map(request, cancellationPolicy);
            await _unit.SaveChangesAsync();
            return await CancellationPolicyHelpers.ReturnResponse(cancellationPolicy, _unit, _mapper);
        }
    }
}
