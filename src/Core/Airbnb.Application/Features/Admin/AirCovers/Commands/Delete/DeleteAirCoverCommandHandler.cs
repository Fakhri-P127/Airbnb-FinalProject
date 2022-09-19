using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AirCovers;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Delete
{
    public class DeleteAirCoverCommandHandler : IRequestHandler<DeleteAirCoverCommand>
    {
        private readonly IUnitOfWork _unit;
        public DeleteAirCoverCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteAirCoverCommand request, CancellationToken cancellationToken)
        {
            AirCover airCover = await _unit.AirCoverRepository.GetByIdAsync(request.Id, null);
            if (airCover is null) throw new AirCoverNotFoundException();
            await _unit.AirCoverRepository.DeleteAsync(airCover);

            return await Task.FromResult(Unit.Value);
        }
    }
}
