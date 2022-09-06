﻿using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Exceptions.AirCovers;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Features.Client.Properties.Queries.GetById;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AirCovers.Queries.GetById
{
    public class AirCoverGetByIdQueryHandler : IRequestHandler<AirCoverGetByIdQuery, AirCoverResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public AirCoverGetByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<AirCoverResponse> Handle(AirCoverGetByIdQuery request, CancellationToken cancellationToken)
        {
            AirCover airCover = await _unit.AirCoverRepository.GetByIdAsync(request.Id, request.Expression);
            if (airCover is null) throw new AirCoverNotFoundException();
            AirCoverResponse response = _mapper.Map<AirCoverResponse>(airCover);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
