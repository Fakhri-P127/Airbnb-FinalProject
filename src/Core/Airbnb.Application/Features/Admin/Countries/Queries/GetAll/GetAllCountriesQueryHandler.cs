using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Features.Admin.Regions.Queries.GetAll;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Countries.Queries.GetAll
{
    public class GetAllCountriesQueryHandler:IRequestHandler<GetAllCountriesQuery,List<CountryResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllCountriesQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<CountryResponse>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            List<Country> countries = await _unit.CountryRepository
               .GetAllAsync(request.Expression,request.Parameters,false, CountryHelper.AllCountryIncludes());

            List<CountryResponse> responses = _mapper.Map<List<CountryResponse>>(countries);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
