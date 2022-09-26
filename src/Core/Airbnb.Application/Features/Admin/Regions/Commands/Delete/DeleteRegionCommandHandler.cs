using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Regions.Commands.Delete
{
    public class DeleteRegionCommandHandler:IRequestHandler<DeleteRegionCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteRegionCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = await _unit.RegionRepository.GetByIdAsync(request.Id, null,true);
            if (region is null) throw new RegionNotFoundException();
            await _unit.RegionRepository.DeleteAsync(region);
            return await Task.FromResult(Unit.Value);
        }
    }
}
