using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Features.Client.Reservations.Commands.Update;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.ExtendReservationDuration
{
    public class ExtendReservationDurationCommandHandler : IRequestHandler<ExtendReservationDurationCommand, PostReservationResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public ExtendReservationDurationCommandHandler(IUnitOfWork unit, IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<PostReservationResponse> Handle(ExtendReservationDurationCommand request, CancellationToken cancellationToken)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            Reservation reservation = await _unit.ReservationRepository
                 .GetByIdAsync(Id, null,true, "Property");
            if (reservation is null) throw new ReservationNotFoundException(Id);

            request.CheckOutDate = request.CheckOutDate.Date + reservation.Property.CheckOutTime;

            int reservedDays = request.CheckOutDate.Subtract(reservation.CheckInDate).Days;
            int existedReservedDays = reservation.CheckOutDate.Subtract(reservation.CheckInDate).Days;
            ReservationHelpers.CheckOutDateValidationChecker(reservation.Property, reservedDays);
            await CheckIfCheckOutIsOccupied(request, reservation);
            await CheckIfItContainsOccupiedDate(request, reservation);
            _unit.ReservationRepository.Update(reservation);
            reservation.CheckOutDate = request.CheckOutDate;
            if (existedReservedDays != reservedDays)
                ReservationHelpers.CalculatePrice(reservation, reservedDays);
            await _unit.SaveChangesAsync();
            return await ReservationHelpers.ReturnResponse(reservation, _unit, _mapper);
        }
        private async Task CheckIfItContainsOccupiedDate(ExtendReservationDurationCommand request, Reservation reservation)
        {
            List<Reservation> containsOccupiedDate = await _unit.ReservationRepository.GetAllAsync(x =>
                        x.CheckInDate >= reservation.CheckInDate && x.CheckInDate <= request.CheckOutDate
                        && x.Id != reservation.Id, null);
            if (containsOccupiedDate.Count != 0) throw new ReservationContainsOccupiedDateException();
        }

        private async Task CheckIfCheckOutIsOccupied(ExtendReservationDurationCommand request, Reservation reservation)
        {
            List<Reservation> occupiedCheckOutTime = await _unit.ReservationRepository
                .GetAllAsync(x => x.CheckInDate <= request.CheckOutDate
                && x.CheckOutDate >= request.CheckOutDate && x.Id != reservation.Id, null);
            if (occupiedCheckOutTime.Count != 0)
                throw new ReservationCheckOutOccupiedException(request.CheckOutDate);
        }
    }
}
