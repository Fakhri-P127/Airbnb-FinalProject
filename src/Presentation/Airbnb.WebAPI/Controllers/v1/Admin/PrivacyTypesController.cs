using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Delete;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Update;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetAll;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class PrivacyTypesController : BaseController
    {
        private readonly ISender _mediatr;

        public PrivacyTypesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrivacyTypes()
        {
            var result = await _mediatr.Send(new GetAllPrivacyTypeQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrivacyTypeById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetByIdPrivacyTypeQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrivacyType([FromBody] CreatePrivacyTypeCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPrivacyTypeById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrivacyType([FromBody] UpdatePrivacyTypeCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrivacyType([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePrivacyTypeCommand(id));
            return NoContent();
        }
    }
}
