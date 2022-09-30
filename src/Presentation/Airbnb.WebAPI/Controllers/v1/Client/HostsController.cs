using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Host.Parameters;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Features.Client.Hosts.Commands.Create;
using Airbnb.Application.Features.Client.Hosts.Commands.UpdateHostStatus;
using Airbnb.Application.Features.Client.Hosts.Queries.GetAll;
using Airbnb.Application.Features.Client.Hosts.Queries.GetById;
using Airbnb.Infrastructure.Common.Utilities;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class HostsController : BaseController
    {
        private readonly ISender _mediatr;

        public HostsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllHosts([FromQuery] HostParameters parameters)
        {

            List<GetHostResponse> result = await _mediatr.Send(new GetAllHostQuery(parameters, null));
            //var metadata = new
            //{
            //    result.TotalCount,result.PageSize,result.CurrentPage,result.TotalPages,
            //    result.HasNext,result.HasPrevious
            //};
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(result);
            //return Ok(result?.Skip((parameters.PageNumber-1)*parameters.PageSize).Take(parameters.PageSize));
        }
        //[HttpGet("[action]")]
        //[AllowAnonymous]
        //[ResponseCache(Duration = 30)]
        //public async Task<IActionResult> GetPopularHosts()
        //{
        //    List<GetHostResponse> result = await _mediatr
        //        .Send(new GetAllHostQuery(x => x.Reservations.Count > 5));
        //    return Ok(result);
        //}
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetHostById([FromRoute] Guid id)
        {
            GetHostResponse result = await _mediatr.Send(new GetHostByIdQuery(id));
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> BecomeHost([FromBody] CreateHostCommand command)
        {
            PostHostResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetHostById), new { id = result.Id }, result);
        }
        [HttpPatch("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateHostStatus()
        {
            await _mediatr.Send(new UpdateHostStatusCommand());
            return NoContent();
        }
    }
}
