using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Exceptions.CancellationPolicies;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

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
            if (await _unit.CancellationPolicyRepository.GetSingleAsync(x => x.Name == request.Name) is not null)
                throw new CancellationPolicy_DuplicateNameException();
            CancellationPolicy cancellationPolicy = _mapper.Map<CancellationPolicy>(request);
            await _unit.CancellationPolicyRepository.AddAsync(cancellationPolicy);
            return await CancellationPolicyHelpers.ReturnResponse(cancellationPolicy,_unit,_mapper);
        }
    }
}
