using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.Countries.Commands.Update
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateCountryCommandHandler(IUnitOfWork unit, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<CountryResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            Country country = await CheckNotFoundsThenReturnCountry(request, Id);
            _unit.CountryRepository.Update(country, false);
            _mapper.Map(request, country);
            await _unit.SaveChangesAsync();
            return await CountryHelper.ReturnResponse(country, _unit, _mapper);
        }

        private async Task<Country> CheckNotFoundsThenReturnCountry(UpdateCountryCommand request, Guid Id)
        {
            Country country = await _unit.CountryRepository.GetByIdAsync(Id, null,true);
            if (country is null) throw new CountryNotFoundException();
            if (await _unit.RegionRepository.GetByIdAsync(request.RegionId.TryParseNullableGuidIdToGuid(), null) is null)
                throw new RegionNotFoundException();
            
            if (await _unit.CountryRepository.GetSingleAsync(x => x.Name == request.Name) is not null)
                throw new Country_DuplicateNameException(request.Name);
            return country;
        }
    }
}
