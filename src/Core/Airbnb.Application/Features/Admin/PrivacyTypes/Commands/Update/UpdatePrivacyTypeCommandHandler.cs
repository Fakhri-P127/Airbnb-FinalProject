using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Update
{
    public class UpdatePrivacyTypeCommandHandler : IRequestHandler<UpdatePrivacyTypeCommand, PrivacyTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UpdatePrivacyTypeCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PrivacyTypeResponse> Handle(UpdatePrivacyTypeCommand request, CancellationToken cancellationToken)
        {
            PrivacyType privacyType = await _unit.PrivacyTypeRepository.GetByIdAsync(request.Id, null);
            if (privacyType is null) throw new PrivacyTypeNotFoundException();
            _unit.PrivacyTypeRepository.Update(privacyType, false);
            privacyType.Name = request.Name;
            await _unit.SaveChangesAsync();
            privacyType = await _unit.PrivacyTypeRepository.GetByIdAsync(privacyType.Id, null,"Properties");
            PrivacyTypeResponse response = _mapper.Map<PrivacyTypeResponse>(privacyType);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
