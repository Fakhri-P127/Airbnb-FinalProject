using Airbnb.Application.Contracts.v1.Admin.Regions.Responses;
using Airbnb.Application.Features.Admin.Regions.Commands.Create;
using Airbnb.Application.Features.Admin.Regions.Commands.Delete;
using Airbnb.Application.Features.Admin.Regions.Commands.Update;
using Airbnb.Application.Features.Admin.Regions.Queries.GetAll;
using Airbnb.Application.Features.Admin.Regions.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class RegionsController : BaseController
    {
        private readonly ISender _mediatr;

        public RegionsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllRegions()
        {
            List<RegionResponse> result = await _mediatr.Send(new GetAllRegionsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            RegionResponse result = await _mediatr.Send(new GetRegionByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionCommand command)
        {
            RegionResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetRegionById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRegion([FromBody] UpdateRegionCommand command)
        {
            RegionResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteRegionCommand(id));
            return NoContent();
        }
    }
}
