using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.Amenities.Queries.GetById
{
    public class GetByIdAmenityQueryHandler : IRequestHandler<GetByIdAmenityQuery, GetAmenityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetByIdAmenityQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GetAmenityResponse> Handle(GetByIdAmenityQuery request, CancellationToken cancellationToken)
        {
            Amenity amenity = await _unit.AmenityRepository.GetByIdAsync(request.Id, request.Expression);
            if (amenity is null) throw new NotFoundException("Amenity");
            GetAmenityResponse response = _mapper.Map<GetAmenityResponse>(amenity);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
