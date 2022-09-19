using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AmenityTypes;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Commands.Delete
{
    public class DeleteAmenityTypeCommandHandler : IRequestHandler<DeleteAmenityTypeCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteAmenityTypeCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteAmenityTypeCommand request, CancellationToken cancellationToken)
        {
            AmenityType amenityType = await _unit.AmenityTypeRepository.GetByIdAsync(request.Id, null);
            if (amenityType is null) throw new AmenityTypeNotFoundException();
            await _unit.AmenityTypeRepository.DeleteAsync(amenityType);
            return await Task.FromResult(Unit.Value);
        }
    }
}
