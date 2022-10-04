using Airbnb.Application.Contracts.v1;
using Airbnb.Application.Contracts.v1.Client.Property.Parameters;
using Airbnb.Application.Contracts.v1.Client.Property.Responses;
using Airbnb.Application.Features.Client.Properties.Commands.Create;
using Airbnb.Application.Features.Client.Properties.Commands.Delete;
using Airbnb.Application.Features.Client.Properties.Commands.Update;
using Airbnb.Application.Features.Client.Properties.Commands.UpdatePendingStatus;
using Airbnb.Application.Features.Client.Properties.Queries.GetAll;
using Airbnb.Application.Features.Client.Properties.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class PropertiesController : BaseController
    {
        private readonly ISender _mediatr;

        public PropertiesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Gets all the Properties
        /// </summary>
        /// <param name="parameters"></param>
        /// <response code="200">Gets all the properties</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllProperties([FromQuery] PropertyParameters parameters)
        {
            List<GetPropertyResponse> result = await _mediatr.Send(new PropertyGetAllQuery(parameters));
            return Ok(result);
        }
        /// <summary>
        /// Gets all the pending properties.
        /// </summary>
        /// <param name="hostId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route($"/{ApiRoutes.Root}/{ApiRoutes.Version}/{ApiRoutes.Hosts.Name}/{{hostId}}/getallpendingproperties")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllPendingPropertiesOfHost([FromRoute] Guid hostId)
        {
            List<GetPropertyResponse> result = await _mediatr
                .Send(new PropertyGetAllQuery(null, x => x.IsDisplayed == null
                 && x.HostId == hostId));
            return Ok(result);
        }
        /// <summary>
        /// Gets property by Id
        /// </summary>
        /// <param name="id">Id of the property</param>
        /// <response code="200">Gets the property</response>
        /// <response code="404">Property not found</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPropertById([FromRoute] Guid id)
        {
            GetPropertyResponse result = await _mediatr.Send(new PropertyGetByIdQuery(id));
            return Ok(result);
        }
        /// <summary>
        /// Creates Property
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyCommand command)
        {
            CreatePropertyResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertById), routeValues: new { id = result.Id }, result);
        }
        /// <summary>
        /// Updates the property
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> UpdateProperty([FromForm] UpdatePropertyCommand command)
        {
            CreatePropertyResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        /// <summary>
        /// This endpoint is for the background service to update the statuses daily
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> UpdatePropertyPendingStatus([FromRoute] Guid id)
        {
            await _mediatr.Send(new UpdatePropertyPendingStatusCommand(id));
            return NoContent();
        }
        /// <summary>
        /// Deletes the property
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Host,Admin,Moderator")]
        public async Task<IActionResult> DeleteProperty([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyCommand(id));
            return NoContent();
        }
    }
}
