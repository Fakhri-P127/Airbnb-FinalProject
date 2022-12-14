using Airbnb.Application.Contracts.v1;
using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Parameters;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Create;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Delete;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Update;
using Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll;
using Airbnb.Application.Features.Client.GuestReviews.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    public class GuestReviewsController : BaseController
    {
        private readonly ISender _mediatr;

        public GuestReviewsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }
        
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllGuestReviews([FromQuery] GuestReviewParameters parameters)
        {
            List<GuestReviewResponse> result = await _mediatr.Send(new GetAllGuestReviewsQuery(parameters));
            return Ok(result);
        }
        [HttpGet]
        [Route($"/{ApiRoutes.Root}/{ApiRoutes.Version}/{ApiRoutes.Hosts.Name}/{{hostId}}/guestreviews")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetGuestReviewsWrittenByHost([FromRoute]Guid hostId, [FromQuery]GuestReviewParameters parameters)
        {
            List<GuestReviewResponse> result = await _mediatr
                .Send(new GetAllGuestReviewsQuery(parameters,x => x.HostId == hostId));
            return Ok(result);
        }
        [HttpGet]
        [Route($"/{ApiRoutes.Root}/{ApiRoutes.Version}/{ApiRoutes.Users.Name}/{{guestId}}/guestreviews")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetGuestReviewsOfUser([FromRoute]Guid guestId, [FromQuery]GuestReviewParameters parameters)
        {
            List<GuestReviewResponse> result = await _mediatr
                .Send(new GetAllGuestReviewsQuery(parameters,x => x.AppUserId == guestId));
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]

        public async Task<IActionResult> GetGuestReviewById([FromRoute]Guid id)
        {
            GuestReviewResponse result = await _mediatr.Send(new GetGuestReviewByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="Host")]
        public async Task<IActionResult> WriteGuestReview([FromBody] CreateGuestReviewCommand command)
        {
            GuestReviewResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetGuestReviewById),new { id = result.Id}, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> UpdateGuestReview(UpdateGuestReviewCommand command)
        {
            GuestReviewResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Host,Moderator,Admin")]
        public async Task<IActionResult> DeleteGuestReview([FromRoute]Guid id)
        {
            await _mediatr.Send(new DeleteGuestReviewCommand(id));
            return NoContent();
        }
    }
}
