using Airbnb.Application.Contracts.v1.Admin.Cities.Responses;
using Airbnb.Application.Features.Admin.Cities.Commands.Create;
using Airbnb.Application.Features.Admin.Cities.Commands.Delete;
using Airbnb.Application.Features.Admin.Cities.Commands.Update;
using Airbnb.Application.Features.Admin.Cities.Queries.GetAll;
using Airbnb.Application.Features.Admin.Cities.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class CitiesController : BaseController
    {
        private readonly ISender _mediatr;

        public CitiesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllCities()
        {
            List<CityResponse> result = await _mediatr.Send(new GetAllCitiesQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetCityById([FromRoute] Guid id)
        {
            CityResponse result = await _mediatr.Send(new GetCityByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityCommand command)
        {
            CityResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetCityById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateCityCommand command)
        {
            CityResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCity([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteCityCommand(id));
            return NoContent();
        }
    }
}
