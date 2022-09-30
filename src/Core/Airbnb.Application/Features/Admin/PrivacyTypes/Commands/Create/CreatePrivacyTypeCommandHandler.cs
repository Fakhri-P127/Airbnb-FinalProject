using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create
{
    public class CreatePrivacyTypeCommandHandler : IRequestHandler<CreatePrivacyTypeCommand, PrivacyTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreatePrivacyTypeCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<PrivacyTypeResponse> Handle(CreatePrivacyTypeCommand request, CancellationToken cancellationToken)
        {
            var existed = await _unit.PrivacyTypeRepository.GetAllAsync(x => x.Name == request.Name, null);
            if (existed.Any())
                throw new DuplicatePrivacyTypeNameValidationException();
            PrivacyType privacyType = new()
            {
                Name = request.Name
            };
            await _unit.PrivacyTypeRepository.AddAsync(privacyType);
            return await PrivacyTypeHelpers.ReturnResponse(privacyType, _unit, _mapper);
        }
    }
}
