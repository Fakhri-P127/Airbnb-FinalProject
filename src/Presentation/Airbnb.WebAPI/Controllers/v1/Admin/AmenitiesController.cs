using Airbnb.Application.Features.Admin.AirCovers.Commands.Update;
using Airbnb.Application.Features.Admin.Amenities.Commands.Create;
using Airbnb.Application.Features.Admin.Amenities.Commands.Delete;
using Airbnb.Application.Features.Admin.Amenities.Queries.GetAll;
using Airbnb.Application.Features.Admin.Amenities.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{ 
    public class AmenitiesController : BaseController
    {
        private readonly ISender _mediatr;

        public AmenitiesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAmenities()
        {
            var result = await _mediatr.Send(new GetAllAmenityQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAmenityById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetByIdAmenityQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAmenity([FromBody] CreateAmenityCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetAmenityById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAmenity([FromRoute] Guid id, [FromBody] UpdateAmenityCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAmenity([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteAmenityCommand(id));
            return NoContent();
        }
    }
}
