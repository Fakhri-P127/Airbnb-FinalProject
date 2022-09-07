﻿using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.Hosts.Queries.GetAll
{
    public class GetAllHostQueryHandler : IRequestHandler<GetAllHostQuery, List<GetHostResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllHostQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<GetHostResponse>> Handle(GetAllHostQuery request, CancellationToken cancellationToken)
        {
            List<Host> hosts = await _unit.HostRepository.GetAllAsync(request.Expression,"AppUser");

            List<GetHostResponse> responses = _mapper.Map<List<GetHostResponse>>(hosts);
            return responses;
                
        }
    }
}