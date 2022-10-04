using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AmenityTypes;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;

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
            AmenityType amenityType = await _unit.AmenityTypeRepository.GetByIdAsync(request.Id, null,true);
            if (amenityType is null) throw new AmenityTypeNotFoundException();
            await _unit.AmenityTypeRepository.DeleteAsync(amenityType);
            return await Task.FromResult(Unit.Value);
        }
    }
}
