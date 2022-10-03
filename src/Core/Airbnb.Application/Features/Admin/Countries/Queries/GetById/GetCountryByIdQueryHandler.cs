using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Countries.Queries.GetById
{
    public class GetCountryByIdQueryHandler:IRequestHandler<GetCountryByIdQuery,CountryResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetCountryByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<CountryResponse> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            Country country = await _unit.CountryRepository
               .GetByIdAsync(request.Id, request.Expression,false, CountryHelper.AllCountryIncludes());
            if (country is null) throw new CountryNotFoundException();
            CountryResponse response = _mapper.Map<CountryResponse>(country);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
