using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Exceptions.Cities;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Features.Admin.Countries.Queries.GetById;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Cities.Queries.GetById
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetCityByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<CityResponse> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            City city = await _unit.CityRepository
               .GetByIdAsync(request.Id, request.Expression,false, CityHelper.AllCityIncludes());
            if (city is null) throw new CityNotFoundException();
            CityResponse response = _mapper.Map<CityResponse>(city);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
