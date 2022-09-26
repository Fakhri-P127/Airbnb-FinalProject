using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Delete
{
    public class DeletePrivacyTypeCommandHandler:IRequestHandler<DeletePrivacyTypeCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeletePrivacyTypeCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeletePrivacyTypeCommand request, CancellationToken cancellationToken)
        {
            PrivacyType privacyType = await _unit.PrivacyTypeRepository.GetByIdAsync(request.Id, null,true);
            if (privacyType is null) throw new PrivacyTypeNotFoundException();
            await _unit.PrivacyTypeRepository.DeleteAsync(privacyType);

            return await Task.FromResult(Unit.Value);
        }
    }
}
