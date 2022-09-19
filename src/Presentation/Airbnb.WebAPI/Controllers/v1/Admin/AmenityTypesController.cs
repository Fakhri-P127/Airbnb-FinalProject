using Airbnb.Application.Features.Admin.AmenityTypes.Commands.Create;
using Airbnb.Application.Features.Admin.AmenityTypes.Commands.Delete;
using Airbnb.Application.Features.Admin.AmenityTypes.Commands.Update;
using Airbnb.Application.Features.Admin.AmenityTypes.Queries.GetAll;
using Airbnb.Application.Features.Admin.AmenityTypes.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class AmenityTypesController : BaseController
    {
        private readonly ISender _mediatr;

        public AmenityTypesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAmenityTypes()
        {
            var result = await _mediatr.Send(new GetAllAmenityTypesQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAmenityTypeById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetByIdAmenityTypeQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAmenityType([FromBody] CreateAmenityTypeCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetAmenityTypeById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAmenityType([FromRoute] Guid id, [FromBody] UpdateAmenityTypeCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAmenityType([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteAmenityTypeCommand(id));
            return NoContent();
        }
    }
}
