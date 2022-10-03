using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Create
{
    public class CreateAirCoverCommandHandler : IRequestHandler<CreateAirCoverCommand, AirCoverResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateAirCoverCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<AirCoverResponse> Handle(CreateAirCoverCommand request, CancellationToken cancellationToken)
        {
            AirCover airCover = _mapper.Map<AirCover>(request);
            await _unit.AirCoverRepository.AddAsync(airCover);
            airCover= await _unit.AirCoverRepository.GetByIdAsync(airCover.Id, null);
            AirCoverResponse response = _mapper.Map<AirCoverResponse>(airCover);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
