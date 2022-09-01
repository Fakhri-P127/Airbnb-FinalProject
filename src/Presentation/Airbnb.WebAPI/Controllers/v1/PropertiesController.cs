using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Features.Properties.Commands.Create;
using Airbnb.Application.Features.Properties.Commands.Update;
using Airbnb.Application.Features.Properties.Queries.GetAll;
using Airbnb.Application.Features.Properties.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1
{
    public class PropertiesController : BaseController
    {
        private readonly ISender _mediatr;

        public PropertiesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var result = await _mediatr.Send(new PropertyGetAllQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPropertById([FromRoute]Guid id)
        {
            var result = await _mediatr.Send(new PropertyGetByIdQuery(id));
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyCommand command)
        {
            var result = await _mediatr.Send(command);
            if (result is null) throw new Exception("Internal server error");
            return CreatedAtAction(nameof(GetPropertById), routeValues: new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProperty([FromForm] UpdatePropertyCommand command)
        {
            var result = await _mediatr.Send(command);
            if (result is null) throw new Exception("Internal server error");
            return Ok(result);
        }
    }
}
