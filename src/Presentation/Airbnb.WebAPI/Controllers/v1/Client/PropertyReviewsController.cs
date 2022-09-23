using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Create;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Delete;
using Airbnb.Application.Features.Client.PropertyReviews.Commands.Update;
using Airbnb.Application.Features.Client.PropertyReviews.Queries.GetAll;
using Airbnb.Application.Features.Client.PropertyReviews.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyReviews()
        {
            List<PropertyReviewResponse> result = await _mediatr.Send(new GetAllPropertyReviewsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyReviewById([FromRoute]Guid id)
        {
            PropertyReviewResponse result = await _mediatr.Send(new GetPropertyReviewByIdQuery(id));
            return Ok(result);
        }
        [HttpGet("[action]/{guestId}")]
        public async Task<IActionResult> GetPropertyReviewsWrittenByGuest([FromRoute] Guid guestId)
        {
            List<PropertyReviewResponse> result = await _mediatr
                .Send(new GetAllPropertyReviewsQuery(x => x.AppUserId == guestId));
            return Ok(result);
        }

        [HttpGet("[action]/{hostId}")]
        public async Task<IActionResult> GetPropertyReviewsOfAHost(Guid hostId)
        {
            List<PropertyReviewResponse> result = await _mediatr
                .Send(new GetAllPropertyReviewsQuery(x => x.HostId == hostId));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> WritePropertyReview([FromBody] CreatePropertyReviewCommand command)
        {
            PropertyReviewResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetPropertyReviewById),new { id = result.Id },result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePropertyReview([FromBody] UpdatePropertyReviewCommand command)
        {
            PropertyReviewResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyReview([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeletePropertyReviewCommand(id));
            return NoContent();
        }
    }
}
