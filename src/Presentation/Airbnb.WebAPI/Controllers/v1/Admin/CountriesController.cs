using Airbnb.Application.Contracts.v1.Admin.AirCovers.Parameters;
using Airbnb.Application.Contracts.v1.Admin.Countries.Parameters;
using Airbnb.Application.Contracts.v1.Admin.Countries.Responses;
using Airbnb.Application.Features.Admin.Countries.Commands.Create;
using Airbnb.Application.Features.Admin.Countries.Commands.Delete;
using Airbnb.Application.Features.Admin.Countries.Commands.Update;
using Airbnb.Application.Features.Admin.Countries.Queries.GetAll;
using Airbnb.Application.Features.Admin.Countries.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class CountriesController : BaseController
    {
        private readonly ISender _mediatr;

        public CountriesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllCountries([FromQuery] CountryParameters parameters)
        {
            List<CountryResponse> result = await _mediatr.Send(new GetAllCountriesQuery(parameters));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetCountryById([FromRoute] Guid id)
        {
            CountryResponse result = await _mediatr.Send(new GetCountryByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryCommand command)
        {
            CountryResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetCountryById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryCommand command)
        {
            CountryResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCountry([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteCountryCommand(id));
            return NoContent();
        }
    }
}
