using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Features.Admin.Countries.Queries.GetAll;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Cities.Queries.GetAll
{
    public class GetAllCitiesQueryHandler:IRequestHandler<GetAllCitiesQuery,List<CityResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllCitiesQueryHandler(IUnitOfWork unit, IMapper mapper)
        { 
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<CityResponse>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            List<City> cities = await _unit.CityRepository
               .GetAllAsync(request.Expression,request.Parameters,false, CityHelper.AllCityIncludes());

            List<CityResponse> responses = _mapper.Map<List<CityResponse>>(cities);
            //if (!responses.Any()) throw new Exception("Internal server error");
            return responses;
        }
    }
}
