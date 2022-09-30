using Airbnb.Application.Contracts.v1.Admin.AirCovers.Parameters;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.AmenityTypes.Responses;
using Airbnb.Application.Features.Admin.AmenityTypes.Commands.Create;
using Airbnb.Application.Features.Admin.AmenityTypes.Commands.Delete;
using Airbnb.Application.Features.Admin.AmenityTypes.Commands.Update;
using Airbnb.Application.Features.Admin.AmenityTypes.Queries.GetAll;
using Airbnb.Application.Features.Admin.AmenityTypes.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllAmenityTypes([FromQuery] AmenityTypeParameters parameters)
        {
            List<AmenityTypeResponse> result = await _mediatr.Send(new GetAllAmenityTypesQuery(parameters));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAmenityTypeById([FromRoute] Guid id)
        {
            AmenityTypeResponse result = await _mediatr.Send(new GetByIdAmenityTypeQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAmenityType([FromBody] CreateAmenityTypeCommand command)
        {
            AmenityTypeResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetAmenityTypeById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAmenityType([FromBody] UpdateAmenityTypeCommand command)
        {
            AmenityTypeResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAmenityType([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteAmenityTypeCommand(id));
            return NoContent();
        }
    }
}
