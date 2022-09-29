using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Property.Parameters;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Features.Client.Properties.Commands.Create;
using Airbnb.Application.Features.Client.Properties.Commands.Delete;
using Airbnb.Application.Features.Client.Properties.Commands.Update;
using Airbnb.Application.Features.Client.Properties.Commands.UpdatePendingStatus;
using Airbnb.Application.Features.Client.Properties.Queries.GetAll;
using Airbnb.Application.Features.Client.Properties.Queries.GetById;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Common;
using Airbnb.Persistance.Context;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class PropertiesController : BaseController
    {
        private readonly ISender _mediatr;
        private readonly AirbnbDbContext _context;
        private readonly CustomUserManager<AppUser> _userManager;

        public PropertiesController(ISender mediatr,AirbnbDbContext context,CustomUserManager<AppUser> userManager)
        {
            _mediatr = mediatr;
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles(Guid appUserId)
        {
            var list = await _context.Roles.ToListAsync();
            AppUser user = await _userManager.FindByIdAsync(appUserId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);
            return Ok(new { list, user, userRoles });
        }
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllProperties([FromQuery]PropertyParameters parameters)
        {
            List<GetPropertyResponse> result = await _mediatr.Send(new PropertyGetAllQuery(parameters));
            return Ok(result);
        }
        [HttpGet("{hostId}/[action]")]
        [ResponseCache(Duration = 30)]// remove this
        public async Task<IActionResult> GetAllPendingPropertiesOfHost([FromRoute] Guid hostId)
        {
            List<GetPropertyResponse> result = await _mediatr
                .Send(new PropertyGetAllQuery(null,x=> x.IsDisplayed == null
                 && x.HostId == hostId));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPropertById([FromRoute] Guid id)
        {
            GetPropertyResponse result = await _mediatr.Send(new PropertyGetByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyCommand command)
        {
            CreatePropertyResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertById), routeValues: new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> UpdateProperty([FromForm] UpdatePropertyCommand command)
        {   
            CreatePropertyResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpPatch("{id}")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> UpdatePropertyPendingStatus([FromRoute]Guid id)
        {
            await _mediatr.Send(new UpdatePropertyPendingStatusCommand(id));
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Host,Admin,Moderator")]
        public async Task<IActionResult> DeleteProperty([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyCommand(id));
            return NoContent();
        }
    }
}
