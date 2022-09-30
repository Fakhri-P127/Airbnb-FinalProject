using Airbnb.Application.Contracts.v1.Admin.AirCovers.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Delete;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update;
using Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetAll;
using Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class PropertyGroupsController : BaseController
    {
        private readonly ISender _mediatr;

        public PropertyGroupsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllPropertyGroups([FromQuery] PropertyGroupParameters parameters)
        {
            List<GetPropertyGroupResponse> result = await _mediatr.Send(new GetAllPropertyGroupsQuery(parameters));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPropertyGroupById([FromRoute] Guid id)
        {
            GetPropertyGroupResponse result = await _mediatr.Send(new GetByIdPropertyGroupQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePropertyGroup([FromForm] CreatePropertyGroupCommand command)
        {
            PostPropertyGroupResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertyGroupById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePropertyGroup([FromForm] UpdatePropertyGroupCommand command)
        {
            PostPropertyGroupResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePropertyGroup([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyGroupCommand(id));
            return NoContent();
        }
    }
}
