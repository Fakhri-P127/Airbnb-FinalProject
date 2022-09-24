using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Delete;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Update;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetAll;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllPrivacyTypes()
        {
            List<PrivacyTypeResponse> result = await _mediatr.Send(new GetAllPrivacyTypeQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPrivacyTypeById([FromRoute] Guid id)
        {
            PrivacyTypeResponse result = await _mediatr.Send(new GetByIdPrivacyTypeQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePrivacyType([FromBody] CreatePrivacyTypeCommand command)
        {
            PrivacyTypeResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPrivacyTypeById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePrivacyType([FromBody] UpdatePrivacyTypeCommand command)
        {
            PrivacyTypeResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePrivacyType([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePrivacyTypeCommand(id));
            return NoContent();
        }
    }
}
