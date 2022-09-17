﻿using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Enums.Reservations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralService
{
    public class StatusChanger
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unit;

        public StatusChanger(ILogger<Worker> logger, IUnitOfWork unit)
        {
            _logger = logger;
            _unit = unit;
        }
        public async Task UpdateReservationStatus(Guid id)
        {
            Reservation reservation = await _unit.ReservationRepository.GetByIdAsync(id, null);
            if (reservation is null) throw new ReservationNotFoundException(id);
            CheckStatusExceptions(reservation);
            _unit.ReservationRepository.Update(reservation, false);

            SetReservationStatus(reservation);
            await _unit.SaveChangesAsync();
        }
        private static void SetReservationStatus(Reservation reservation)
        {
            int reservedDays = reservation.CheckOutDate.Subtract(reservation.CheckInDate).Days;
            int daysLeftTillCheckIn = reservation.CheckInDate.Subtract(DateTime.Now).Days;
            int daysLeftTillCheckOut = reservation.CheckOutDate.Subtract(DateTime.Now).Days;
            TimeSpan hoursLeftTillCheckIn = reservation.CheckInDate.Subtract(DateTime.Now);

            if (DateTime.Now > reservation.CheckOutDate.AddDays(1))
                reservation.Status = (int)Enum_ReservationStatus.ReservationFinished;
            else if (daysLeftTillCheckIn >= 2)
                reservation.Status = (int)Enum_ReservationStatus.Upcoming;
            else if (daysLeftTillCheckIn == 1 && reservation.Status != (int)Enum_ReservationStatus.ArrivingSoon)
                // ola biler rezervasiyanin ozu 1 gun qalmish edilib, o vaxt onsuzda arriving soon olacaq statusu
                reservation.Status = (int)Enum_ReservationStatus.ArrivingSoon;
            else if (daysLeftTillCheckIn == 0)
            {
                // eger hele saatlar qalibsa arriving soon qalir, yo eger saat uje menfi dise demeli check in olub
                if (hoursLeftTillCheckIn > TimeSpan.Zero)
                    reservation.Status = (int)Enum_ReservationStatus.ArrivingSoon;
                // check outa 2 ve ya daha chox gun qalibsa currently hosting, az qalibsa CheckinOut olur
                else
                {
                    if (daysLeftTillCheckOut == 0 || daysLeftTillCheckOut == 1)
                        reservation.Status = (int)Enum_ReservationStatus.CheckingOut;
                    else if (daysLeftTillCheckOut >= 2 && reservedDays >= daysLeftTillCheckOut)
                        reservation.Status = (int)Enum_ReservationStatus.CurrentlyHosting;
                }
            }
            else if (daysLeftTillCheckOut == 0 || daysLeftTillCheckOut == 1)
                reservation.Status = (int)Enum_ReservationStatus.CheckingOut;
            else if (daysLeftTillCheckOut >= 2 && reservedDays >= daysLeftTillCheckOut)
                reservation.Status = (int)Enum_ReservationStatus.CurrentlyHosting;
            else if (daysLeftTillCheckOut < 0)
                reservation.Status = (int)Enum_ReservationStatus.ReservationFinished;
            else
                // bura dushe bilmez amma yoxlamaq uchun yazmisham
                reservation.Status = 19;
        }

        private static void CheckStatusExceptions(Reservation reservation)
        {
            if (reservation.Status == (int)Enum_ReservationStatus.ReservationCancelled)
                throw new Reservation_ActionToCancelledReservationException();
            if (reservation.Status == (int)Enum_ReservationStatus.ReservationFinished)
                throw new Reservation_StatusAlreadyFinishedException();
        }
    }
       
}