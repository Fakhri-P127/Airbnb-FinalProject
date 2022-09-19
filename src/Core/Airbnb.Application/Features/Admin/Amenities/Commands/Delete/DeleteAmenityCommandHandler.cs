using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Amenities;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Commands.Delete
{
    public class DeleteAmenityCommandHandler:IRequestHandler<DeleteAmenityCommand>
    {
        private readonly IUnitOfWork _unit;
        public DeleteAmenityCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            Amenity amenity = await _unit.AmenityRepository.GetByIdAsync(request.Id, null);
            if (amenity is null) throw new AmenityNotFoundException();
            await _unit.AmenityRepository.DeleteAsync(amenity);

            return await Task.FromResult(Unit.Value);
        }
    }
}
