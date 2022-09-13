using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Application.Features.Client.Reservations.Commands.Delete;
using Airbnb.Application.Features.Client.Reservations.Commands.ExtendReservationDuration;
using Airbnb.Application.Features.Client.Reservations.Commands.Update;
using Airbnb.Application.Features.Client.Reservations.Queries.GetAll;
using Airbnb.Application.Features.Client.Reservations.Queries.GetById;
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
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            List<GetReservationResponse> result = await _mediatr.Send(new GetAllReservationsQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReservationById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetReservationByIdQuery(id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> MakeReservation(CreateReservationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReservation([FromRoute]Guid id,
            [FromBody]UpdateReservationCommand command)
        {
            command.Id = id;
            PostReservationResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpPatch("[action]/{id:guid}")]
        public async Task<IActionResult> ExtendReservationDuration([FromRoute] Guid id,
            [FromBody]ExtendReservationDurationCommand command)
        {
            command.Id = id;
            PostReservationResponse result = await _mediatr.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteReservationCommand(id));
            return NoContent();
        }
    }
}
