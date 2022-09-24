using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Application.Features.Client.Reservations.Commands.Delete;
using Airbnb.Application.Features.Client.Reservations.Commands.ExtendReservationDuration;
using Airbnb.Application.Features.Client.Reservations.Commands.Update;
using Airbnb.Application.Features.Client.Reservations.Commands.UpdateReservationStatus;
using Airbnb.Application.Features.Client.Reservations.Queries.GetAll;
using Airbnb.Application.Features.Client.Reservations.Queries.GetById;
using Airbnb.Domain.Enums.Reservations;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllReservations()
        {
            List<GetReservationResponse> result = await _mediatr.Send(new GetAllReservationsQuery());
            return Ok(result);
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAvailableReservations()
        {
            List<GetReservationResponse> result = await _mediatr
                .Send(new GetAllReservationsQuery(x =>
                x.Status != (int)Enum_ReservationStatus.ReservationCancelled
               && x.Status != (int)Enum_ReservationStatus.ReservationFinished));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetReservationById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetReservationByIdQuery(id));
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> MakeReservation(CreateReservationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Guest,Host")]

        public async Task<IActionResult> UpdateReservation(
            [FromBody]UpdateReservationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpPatch("[action]/{id}")]
        [Authorize(Roles = "Guest,Host")]
        public async Task<IActionResult> ExtendReservationDuration(
            [FromBody]ExtendReservationDurationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            return Ok(result);
        }
        [HttpPatch("[action]/{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> UpdateReservationStatus([FromRoute] Guid id)
        {
            await _mediatr.Send(new UpdateReservationStatusCommand(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Host,Moderator,Admin")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteReservationCommand(id));
            return NoContent();
        }
    }
}
