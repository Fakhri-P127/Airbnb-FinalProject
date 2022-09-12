using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.WebAPI.Controllers.v1.Client
{
    
    public class ReservationsController : BaseController
    {
        private readonly ISender _mediatr;

        public ReservationsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReservationById([FromRoute] Guid id)
        {
            //var result = _mediatr.Send(command);
            return Ok(DateTime.UtcNow.Date);
        }
        [HttpPost]
        public async Task<IActionResult> MakeReservation(CreateReservationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, result);
        }
    }
}
