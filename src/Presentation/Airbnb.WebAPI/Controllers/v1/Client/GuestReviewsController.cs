using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Create;
using Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll;
using Airbnb.Application.Features.Client.GuestReviews.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
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
        public async Task<IActionResult> GetAllGuestReviews()
        {
            List<GuestReviewResponse> result = await _mediatr.Send(new GetAllGuestReviewsQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGuestReviewById([FromRoute]Guid id)
        {
            GuestReviewResponse result = await _mediatr.Send(new GetGuestReviewByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> WriteGuestReview([FromBody] CreateGuestReviewCommand command)
        {
            GuestReviewResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetGuestReviewById),new { id = result.Id}, result);
        }
    }
}
