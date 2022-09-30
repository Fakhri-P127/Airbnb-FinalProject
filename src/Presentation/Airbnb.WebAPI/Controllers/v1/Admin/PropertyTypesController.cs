using Airbnb.Application.Contracts.v1.Admin.AirCovers.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Delete;
using Airbnb.Application.Features.Admin.PropertyTypes.Commands.Update;
using Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetAll;
using Airbnb.Application.Features.Admin.PropertyTypes.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllPropertyTypes([FromQuery] PropertyTypeParameters parameters)
        {
            List<GetPropertyTypeResponse> result = await _mediatr.Send(new GetAllPropertyTypesQuery(parameters));
            var salam = result.GetType().GetProperties();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPropertyTypeById([FromRoute] Guid id)
        {
            GetPropertyTypeResponse result = await _mediatr.Send(new GetByIdPropertyTypeQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreatePropertyType([FromBody] CreatePropertyTypeCommand command)
        {
            PostPropertyTypeResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertyTypeById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePropertyType([FromBody] UpdatePropertyTypeCommand command)
        {
            PostPropertyTypeResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePropertyType([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyTypeCommand(id));
            return NoContent();
        }
    }
}
