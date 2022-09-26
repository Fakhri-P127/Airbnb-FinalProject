using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Exceptions.PropertyTypes;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Delete
{
    public class DeletePropertyTypeCommandHandler : IRequestHandler<DeletePropertyTypeCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeletePropertyTypeCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeletePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            PropertyType propertyType = await _unit.PropertyTypeRepository.GetByIdAsync(request.Id, null,true);
            if (propertyType is null) throw new PropertyTypeNotFoundException();
            await _unit.PropertyTypeRepository.DeleteAsync(propertyType);

            return await Task.FromResult(Unit.Value);
        }
    }
}
