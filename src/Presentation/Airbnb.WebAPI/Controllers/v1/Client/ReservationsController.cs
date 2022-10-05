using Airbnb.Application.Contracts.v1.Client.Reservation.Parameters;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Application.Features.Client.Reservations.Commands.Delete;
using Airbnb.Application.Features.Client.Reservations.Commands.ExtendReservationDuration;
using Airbnb.Application.Features.Client.Reservations.Commands.Update;
using Airbnb.Application.Features.Client.Reservations.Commands.UpdateReservationStatus;
using Airbnb.Application.Features.Client.Reservations.Queries.GetAll;
using Airbnb.Application.Features.Client.Reservations.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Airbnb.WebAPI.Controllers.v1.Client
{

    public class ReservationsController : BaseController
    {
        private readonly ISender _mediatr;
        public ReservationsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }
        /// <summary>
        /// Get all reservations. You can use the query parameters to filter the data you recieve.
        /// </summary>
        /// <param name="parameters"></param>
        /// <response code="200">List of reservations returned successfuly</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetAllReservations([FromQuery]ReservationParameters parameters)
        {
            List<GetReservationResponse> result = await _mediatr.Send(new GetAllReservationsQuery(parameters));
            Log.Information($"Returned reservation with count:{parameters.PageSize}");
            return Ok(result);
        }
        //[HttpGet("[action]")]
        //[AllowAnonymous]
        //[ResponseCache(Duration = 30)]
        //public async Task<IActionResult> GetAvailableReservations()
        //{
        //    List<GetReservationResponse> result = await _mediatr
        //        .Send(new GetAllReservationsQuery(x =>
        //        x.Status != (int)Enum_ReservationStatus.ReservationCancelled
        //       && x.Status != (int)Enum_ReservationStatus.ReservationFinished));
        //    return Ok(result);
        //}


        /// <summary>
        /// Get reservation by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Reservation returned successfuly</response>
        /// <response code="404">Reservation with given Id doesn't exist</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        [HttpGet("{id}")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetReservationById([FromRoute] Guid id)
        {
            var result = await _mediatr.Send(new GetReservationByIdQuery(id));
            Log.Information($"Returned reservation by {id} id");
            return Ok(result);
        }
        /// <summary>
        /// Make a reservation
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Reservation created successfuly</response>
        /// <response code="400">Invalid request, please input valid command</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="409">Given reservation dates are already occupied</response>
        /// <returns>Newly created reservation</returns>
        [HttpPost]
        [Authorize(Roles = "Guest")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> MakeReservation(CreateReservationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            Log.Information($"Created reservation({result.Id}) successfuly");
            return CreatedAtAction(nameof(GetReservationById), new { id = result.Id }, result);
        }
        /// <summary>
        /// Update reservation's data. Empty values will be ignored
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Reservation updated successfuly</response>
        /// <response code="400">Invalid request, please input valid command</response>
        /// <response code="401">You need to be authenticated to use this feature</response>
        /// <response code="404">Reservation with given Id doesn't exist</response>
        /// <response code="409">Given reservation dates are already occupied</response>
        /// <returns>Updated reservation</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Guest,Host")]
        public async Task<IActionResult> UpdateReservation(
            [FromBody]UpdateReservationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            Log.Information($"Updated reservation({result.Id}) successfuly");
            return Ok(result);
        }

        /// <summary>
        /// This endpoint is for background service to update the reservations status
        /// </summary>
        /// <response code="204">Updates status of reservations</response>
        [HttpPatch("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateReservationsStatus()
        {
            await _mediatr.Send(new UpdateReservationStatusCommand());
            return NoContent();
        }
        /// <summary>
        /// With this endpoint you can extend the duration of your trip
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Extended the duration of the reservation successfuly</response>
        /// <response code="404">Reservation with this Id doesn't exist</response>
        /// <response code="409">Given reservation dates are already occupied</response>
        [HttpPatch("{id}")]
        [Authorize(Roles = "Guest,Host")]
        public async Task<IActionResult> ExtendReservationDuration(
            [FromBody]ExtendReservationDurationCommand command)
        {
            PostReservationResponse result = await _mediatr.Send(command);
            Log.Information($"{result.Id} reservations duration has been extended to {result.CheckOutDate}");
            return Ok(result);
        }
      
        /// <summary>
        /// delete reservation by Id
        /// </summary>
        /// <param name="id">Id of the reservation</param>
        /// <response code="204">Deletes the reservation successfuly</response>
        /// <response code="404">Reservation with this Id doesn't exist</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Host,Moderator,Admin")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid id)
        {
            await _mediatr.Send(new DeleteReservationCommand(id));
            Log.Information($"Deleted reservation({id}) successfuly");
            return NoContent();
        }
    }
}
