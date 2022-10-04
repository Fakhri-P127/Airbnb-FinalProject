using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
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

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Update
{
    public class UpdatePrivacyTypeCommandHandler : IRequestHandler<UpdatePrivacyTypeCommand, PrivacyTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdatePrivacyTypeCommandHandler(IUnitOfWork unit, IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<PrivacyTypeResponse> Handle(UpdatePrivacyTypeCommand request, CancellationToken cancellationToken)
        {
            PrivacyType privacyType = await CheckExceptionsThenReturnPrivacyType(request);
            _unit.PrivacyTypeRepository.Update(privacyType, false);
            privacyType.Name = request.Name;
            await _unit.SaveChangesAsync();
            return await PrivacyTypeHelpers.ReturnResponse(privacyType, _unit, _mapper);
        }

        private async Task<PrivacyType> CheckExceptionsThenReturnPrivacyType(UpdatePrivacyTypeCommand request)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            PrivacyType privacyType = await _unit.PrivacyTypeRepository.GetByIdAsync(Id, null, true);
            if (privacyType is null) throw new PrivacyTypeNotFoundException();
            if (await _unit.PrivacyTypeRepository.GetSingleAsync(x => x.Name == request.Name) is not null)
                throw new DuplicatePrivacyTypeNameValidationException();
            return privacyType;
        }
    }
}
