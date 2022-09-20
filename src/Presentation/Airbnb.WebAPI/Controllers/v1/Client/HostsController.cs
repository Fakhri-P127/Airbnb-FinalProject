﻿using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Features.Client.Hosts.Commands.Create;
using Airbnb.Application.Features.Client.Hosts.Queries.GetAll;
using Airbnb.Application.Features.Client.Hosts.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllHosts()
        {
            var result = await _mediatr.Send(new GetAllHostQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetHostById([FromRoute] Guid id)
        {
            GetHostResponse result = await _mediatr.Send(new GetHostByIdQuery(id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHost([FromBody] CreateHostCommand command)
        {
            var result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetHostById), new { id = result.Id }, result);
        }
    }
}
