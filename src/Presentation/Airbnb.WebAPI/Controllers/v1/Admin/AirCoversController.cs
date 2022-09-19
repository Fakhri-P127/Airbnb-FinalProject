using Airbnb.Application.Features.Admin.AirCovers.Commands.Create;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Delete;
using Airbnb.Application.Features.Admin.AirCovers.Commands.Update;
using Airbnb.Application.Features.Admin.AirCovers.Queries.GetAll;
using Airbnb.Application.Features.Admin.AirCovers.Queries.GetById;
using Airbnb.Application.Filters;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAllAirCovers()
        {
            var result = await _mediatr.Send(new AirCoverGetAllQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirCoverById([FromRoute]Guid id)
        {
            var result = await _mediatr.Send(new AirCoverGetByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAirCover([FromBody] CreateAirCoverCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetAirCoverById), routeValues: new {id=result.Id},result);
        }
        [HttpPut("{id}")]
        //[EnsureIdIsGuidActionFilter]
        public async Task<IActionResult> UpdateAirCover([FromBody] UpdateAirCoverCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirCover([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteAirCoverCommand(id));
            return NoContent();
        }

    }
}
