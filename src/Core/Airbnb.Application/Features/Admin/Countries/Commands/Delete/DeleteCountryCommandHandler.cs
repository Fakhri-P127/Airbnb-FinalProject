using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Features.Admin.Regions.Commands.Delete;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Delete
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteCountryCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = await _unit.CountryRepository.GetByIdAsync(request.Id, null,true);
            if (country is null) throw new CountryNotFoundException();
            await _unit.CountryRepository.DeleteAsync(country);
            return await Task.FromResult(Unit.Value);
        }
    }
}
