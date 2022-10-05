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
using Serilog;

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
            Log.Information($"Returned properties with count: {parameters.PageSize}");
            return Ok(result);
        }
        /// <summary>
        /// Gets all the pending properties.
        /// </summary>
        /// <param name="hostId">Id of the host</param>
        /// <response code="200">Returns all the pending properties</response>
        /// <response code="200">Returns all the pending properties</response>
        /// <response code="404">Host with given Id doesn't exist</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <returns>All the pending properties</returns>
        [HttpGet]
        [Route($"/{ApiRoutes.Root}/{ApiRoutes.Version}/{ApiRoutes.Hosts.Name}/{{hostId}}/getallpendingproperties")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllPendingPropertiesOfHost([FromRoute] Guid hostId)
        {
            List<GetPropertyResponse> result = await _mediatr
                .Send(new PropertyGetAllQuery(null, x => x.IsDisplayed == null
                 && x.HostId == hostId));
            Log.Information($"Got all the pending properties for ({hostId}) host");
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
            Log.Information($"Got property by {id} id");
            return Ok(result);
        }
        /// <summary>
        /// Creates Property
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Creates a new Property</response>
        /// <response code="400">Validation errors or state errors</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="404">Given Id for one of the parameters does not exist</response>
        /// <returns>Newly created property</returns>
        [HttpPost]
        [Authorize(Roles = "Host")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyCommand command)
        {
            CreatePropertyResponse result = await _mediatr.Send(command);
            Log.Information($"Created a new property({result.Id})");
            return CreatedAtAction(nameof(GetPropertById), routeValues: new { id = result.Id }, result);
        }
        /// <summary>
        /// Updates the property
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Updates the Property successfuly</response>
        /// <response code="400">Validation errors or state errors</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="404">Given Id for one of the parameters does not exist</response>
        /// <returns>Updated property</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> UpdateProperty([FromForm] UpdatePropertyCommand command)
        {
            CreatePropertyResponse result = await _mediatr.Send(command);
            Log.Information($"Updated the property({result.Id})");
            return Ok(result);
        }
        /// <summary>
        /// This endpoint is for the background service to update the statuses daily
        /// </summary>
        /// <param name="id">Id of the property</param>
        /// <response code="204">Updates the status of property</response>
        /// <response code="400">User verifications are still lacking. Property status has not been changed</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="404">Property with this Id doesn't exist</response>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "Host")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdatePropertyPendingStatus([FromRoute] Guid id)
        {
            await _mediatr.Send(new UpdatePropertyPendingStatusCommand(id));
            return NoContent();
        }
        /// <summary>
        /// Deletes the property
        /// </summary>
        /// <param name="id">Id of the property</param>
        /// <response code="204">Deletes the property successfuly</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="404">Property with this Id doesn't exist</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Host,Admin,Moderator")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProperty([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyCommand(id));
            Log.Information($"Deleted property({id})");
            return NoContent();
        }
    }
}
