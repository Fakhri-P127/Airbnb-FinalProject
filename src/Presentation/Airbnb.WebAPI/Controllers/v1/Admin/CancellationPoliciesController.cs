using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Create;
using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Delete;
using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Update;
using Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetAll;
using Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Admin
{
    public class CancellationPoliciesController : BaseController
    {

        private readonly ISender _mediatr;

        public CancellationPoliciesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCancellationPolicies()
        {
            var result = await _mediatr.Send(new GetAllCancellationPolicyQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCancellationPolicyById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetByIdCancellationPolicyQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCancellationPolicy([FromBody] CreateCancellationPolicyCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetCancellationPolicyById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCancellationPolicy([FromRoute] Guid id, [FromBody] UpdateCancellationPolicyCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCancellationPolicy([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteCancellationPolicyCommand(id));
            return NoContent();
        }
    }
}
