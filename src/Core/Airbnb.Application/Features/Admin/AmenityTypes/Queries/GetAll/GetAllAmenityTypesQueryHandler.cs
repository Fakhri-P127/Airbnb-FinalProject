using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.AmenityTypes.Queries.GetAll
{
    public class GetAllAmenityTypesQueryHandler : IRequestHandler<GetAllAmenityTypesQuery, List<AmenityTypeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public GetAllAmenityTypesQueryHandler(IMapper mapper,IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<List<AmenityTypeResponse>> Handle(GetAllAmenityTypesQuery request, CancellationToken cancellationToken)
        {
            List<AmenityType> amenityTypes = await _unit.AmenityTypeRepository
                .GetAllAsync(request.Expression);
           
            List<AmenityTypeResponse> responses = _mapper.Map<List<AmenityTypeResponse>>(amenityTypes);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
