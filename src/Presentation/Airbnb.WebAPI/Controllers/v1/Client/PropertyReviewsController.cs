using Airbnb.Application.Contracts.v1;
using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Parameters;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Create;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Delete;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Update;
using Airbnb.Application.Features.Client.PropertyReviews.Queries.GetAll;
using Airbnb.Application.Features.Client.PropertyReviews.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class PropertyReviewsController : BaseController
    {
        private readonly ISender _mediatr;

        public PropertyReviewsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }
        /// <summary>
        /// Gets all the Property reviews
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllPropertyReviews([FromQuery] PropertyReviewParameters parameters)
        {
            List<PropertyReviewResponse> result = await _mediatr.Send(new GetAllPropertyReviewsQuery(parameters));
            return Ok(result);
        }
        /// <summary>
        /// Gets a property review by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPropertyReviewById([FromRoute] Guid id)
        {
            PropertyReviewResponse result = await _mediatr.Send(new GetPropertyReviewByIdQuery(id));
            return Ok(result);
        }
        /// <summary>
        /// Gets property reviews of a specific guest
        /// </summary>
        /// <param name="guestId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route($"/{ApiRoutes.Root}/{ApiRoutes.Version}/{ApiRoutes.Users.Name}/{{guestId}}/propertyreviews")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPropertyReviewsWrittenByGuest([FromRoute] Guid guestId, [FromQuery] PropertyReviewParameters parameters)
        {
            List<PropertyReviewResponse> result = await _mediatr
                .Send(new GetAllPropertyReviewsQuery(parameters, x => x.AppUserId == guestId));
            return Ok(result);
        }
        /// <summary>
        ///  Gets property reviews of a specific host
        /// </summary>
        /// <param name="hostId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>

        [HttpGet]
        [Route($"/{ApiRoutes.Root}/{ApiRoutes.Version}/{ApiRoutes.Hosts.Name}/{{hostId}}/propertyreviews")]

        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetPropertyReviewsOfAHost([FromRoute] Guid hostId, [FromQuery] PropertyReviewParameters parameters)
        {
            List<PropertyReviewResponse> result = await _mediatr
                .Send(new GetAllPropertyReviewsQuery(parameters, x => x.HostId == hostId));
            return Ok(result);
        }
        /// <summary>
        /// Creates a property review for reservation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> WritePropertyReview([FromBody] CreatePropertyReviewCommand command)
        {
            PropertyReviewResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertyReviewById), new { id = result.Id }, result);
        }
        /// <summary>
        /// Updates the property review
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> UpdatePropertyReview([FromBody] UpdatePropertyReviewCommand command)
        {
            PropertyReviewResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        /// <summary>
        /// Deletes the property review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Guest,Host,Moderator,Admin")]
        public async Task<IActionResult> DeletePropertyReview([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyReviewCommand(id));
            return NoContent();
        }
    }
}
