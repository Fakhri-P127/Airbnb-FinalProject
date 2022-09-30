using Airbnb.Application.Contracts.v1.Admin.AirCovers.Parameters;
using Airbnb.Application.Contracts.v1.Admin.AirCovers.Responses;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Create;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Delete;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Update;
using Airbnb.Application.Features.Admin.AirCovers.Queries.GetAll;
using Airbnb.Application.Features.Admin.AirCovers.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class AirCoversController : BaseController
    {
        private readonly ISender _mediatr;

        public AirCoversController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllAirCovers([FromQuery]AirCoverParameters parameters)
        {
            List<AirCoverResponse> result = await _mediatr.Send(new AirCoverGetAllQuery(parameters));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAirCoverById([FromRoute]Guid id)
        {
            AirCoverResponse result = await _mediatr.Send(new AirCoverGetByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateAirCover([FromBody] CreateAirCoverCommand command)
        {
            AirCoverResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetAirCoverById), routeValues: new {id=result.Id},result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAirCover([FromBody] UpdateAirCoverCommand command)
        {
            AirCoverResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAirCover([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteAirCoverCommand(id));
            return NoContent();
        }

    }
}
