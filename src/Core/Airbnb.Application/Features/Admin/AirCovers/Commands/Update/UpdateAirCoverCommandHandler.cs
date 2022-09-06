using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Exceptions.AirCovers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Update
{
    public class UpdateAirCoverCommandHandler : IRequestHandler<UpdateAirCoverCommand, AirCoverResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UpdateAirCoverCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<AirCoverResponse> Handle(UpdateAirCoverCommand request, CancellationToken cancellationToken)
        {
            AirCover airCover = await _unit.AirCoverRepository.GetByIdAsync(request.Id, null);
            if (airCover is null) throw new AirCoverNotFoundException();
            _unit.AirCoverRepository.Update(airCover);
            _mapper.Map(request, airCover);
            await _unit.SaveChangesAsync();
            airCover = await _unit.AirCoverRepository.GetByIdAsync(airCover.Id, null);
            AirCoverResponse response = _mapper.Map<AirCoverResponse>(airCover);
            if (response is null) throw new Exception("Internal server error");

            return response;
        }
    }
}
