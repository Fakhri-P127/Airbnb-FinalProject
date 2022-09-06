using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AirCovers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Delete
{
    public class DeleteAirCoverCommandHandler : IRequestHandler<DeleteAirCoverCommand>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public DeleteAirCoverCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
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
