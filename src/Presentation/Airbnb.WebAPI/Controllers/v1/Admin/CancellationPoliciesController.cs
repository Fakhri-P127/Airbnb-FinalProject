using Airbnb.Application.Contracts.v1.Admin.AirCovers.Parameters;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Parameters;
using Airbnb.Application.Contracts.v1.Admin.CancellationPolicies.Responses;
using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Create;
using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Delete;
using Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Update;
using Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetAll;
using Airbnb.Application.Features.Admin.CancellationPolicies.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllCancellationPolicies([FromQuery] CancellationPolicyParameters parameters)
        {
            List<CancellationPolicyResponse> result = await _mediatr.Send(new GetAllCancellationPolicyQuery(parameters));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetCancellationPolicyById([FromRoute] Guid id)
        {
            CancellationPolicyResponse result = await _mediatr.Send(new GetByIdCancellationPolicyQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCancellationPolicy([FromBody] CreateCancellationPolicyCommand command)
        {
            CancellationPolicyResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetCancellationPolicyById), routeValues: new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCancellationPolicy([FromBody] UpdateCancellationPolicyCommand command)
        {
            CancellationPolicyResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCancellationPolicy([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteCancellationPolicyCommand(id));
            return NoContent();
        }
    }
}
