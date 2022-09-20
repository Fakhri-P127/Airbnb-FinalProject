using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Delete;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update;
using Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetAll;
using Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllPropertyGroups()
        {
            var result = await _mediatr.Send(new GetAllPropertyGroupsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyGroupById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetByIdPropertyGroupQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertyGroup([FromForm] CreatePropertyGroupCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertyGroupById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        //[EnsureIdIsGuidActionFilter]
        public async Task<IActionResult> UpdatePropertyGroup([FromForm] UpdatePropertyGroupCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyGroup([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyGroupCommand(id));
            return NoContent();
        }
    }
}
