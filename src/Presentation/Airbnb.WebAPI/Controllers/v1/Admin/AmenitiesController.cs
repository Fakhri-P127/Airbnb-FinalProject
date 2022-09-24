using Airbnb.Application.Contracts.v1.Admin.Amenities.Responses;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Update;
using Airbnb.Application.Features.Admin.Amenities.Commands.Create;
using Airbnb.Application.Features.Admin.Amenities.Commands.Delete;
using Airbnb.Application.Features.Admin.Amenities.Queries.GetAll;
using Airbnb.Application.Features.Admin.Amenities.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [ResponseCache(Duration =30)]
        public async Task<IActionResult> GetAllAmenities()
        {
            List<GetAmenityResponse> result = await _mediatr.Send(new GetAllAmenityQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAmenityById([FromRoute] Guid id)
        {
            GetAmenityResponse result = await _mediatr.Send(new GetByIdAmenityQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateAmenity([FromBody] CreateAmenityCommand command)
        {
            PostAmenityResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetAmenityById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateAmenity([FromBody] UpdateAmenityCommand command)
        {
            PostAmenityResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteAmenity([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteAmenityCommand(id));
            return NoContent();
        }
    }
}
