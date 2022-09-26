using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Create
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<CountryResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = await _unit.CountryRepository.GetSingleAsync(x => x.Name == request.Name);
            if (country is not null) throw new Country_DuplicateNameException(request.Name);
            country = _mapper.Map<Country>(request);
            await _unit.CountryRepository.AddAsync(country);
            return await CountryHelper.ReturnResponse(country, _unit, _mapper);
        }
    }
}
