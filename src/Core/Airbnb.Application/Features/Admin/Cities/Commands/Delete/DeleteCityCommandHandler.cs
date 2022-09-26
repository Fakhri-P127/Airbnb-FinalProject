using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Cities;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;

namespace Airbnb.Application.Features.Admin.Cities.Commands.Delete
{
    public class DeleteCityCommandHandler:IRequestHandler<DeleteCityCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteCityCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            City city = await _unit.CityRepository.GetByIdAsync(request.Id, null,true);
            if (city is null) throw new CityNotFoundException();
            await _unit.CityRepository.DeleteAsync(city);
            return await Task.FromResult(Unit.Value);
        }
    }
}
