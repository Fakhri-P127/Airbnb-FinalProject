using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Delete;
using Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update;
using Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetAll;
using Airbnb.Application.Features.Admin.PropertyGroups.Queries.GetById;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Delete;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Update;
using Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetAll;
using Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    
    public class PropertyTypesController : BaseController
    {
        private readonly ISender _mediatr;

        public PropertyTypesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPropertyTypes()
        {
            var result = await _mediatr.Send(new GetAllPropertyTypesQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPropertyTypeById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetByIdPropertyTypeQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertyType([FromBody] CreatePropertyTypeCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertyTypeById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePropertyType([FromRoute] Guid id, [FromBody] UpdatePropertyTypeCommand command)
        {
            command.Id = id;
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePropertyType([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyTypeCommand(id));
            return NoContent();
        }
    }
}
