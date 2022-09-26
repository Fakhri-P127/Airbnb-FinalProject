using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Exceptions.Cities;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.Regions;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.Cities.Commands.Update
{
    public class UpdateCityCommandHandler:IRequestHandler<UpdateCityCommand,CityResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateCityCommandHandler(IUnitOfWork unit, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<CityResponse> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            City city = await CheckNotFoundsThenReturnCity(request, Id);
            _unit.CityRepository.Update(city, false);
            _mapper.Map(request, city);
            await _unit.SaveChangesAsync();
            return await CityHelper.ReturnResponse(city, _unit, _mapper);
        }

        private async Task<City> CheckNotFoundsThenReturnCity(UpdateCityCommand request, Guid Id)
        {
            City city = await _unit.CityRepository.GetByIdAsync(Id, null,true);
            if (city is null) throw new CityNotFoundException();
            if (await _unit.CountryRepository.GetByIdAsync(request.CountryId.TryParseIdToGuid(), null) is null)
                throw new CountryNotFoundException();
            return city;
        }
    }
}
