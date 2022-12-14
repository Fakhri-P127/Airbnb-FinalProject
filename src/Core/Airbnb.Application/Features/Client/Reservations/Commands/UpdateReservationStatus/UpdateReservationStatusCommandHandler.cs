using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Enums.Reservations;
using MediatR;

namespace Airbnb.Application.Features.Client.Reservations.Commands.UpdateReservationStatus
{
    public class UpdateReservationStatusCommandHandler : IRequestHandler<UpdateReservationStatusCommand>
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailSender _emailSender;
        private readonly CustomUserManager<AppUser> _userManager;

        public UpdateReservationStatusCommandHandler(IUnitOfWork unit,IEmailSender emailSender,
            CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            List<Reservation> reservations = await _unit.ReservationRepository.GetAllAsync(x=>
             x.Status != (int)Enum_ReservationStatus.ReservationCancelled
               && x.Status != (int)Enum_ReservationStatus.ReservationFinished,null,true);
            //Reservation reservation = await _unit.ReservationRepository.GetByIdAsync(request.Id, null,true);
            if (reservations is null || !reservations.Any())
                return await Task.FromResult(Unit.Value);
            reservations.ForEach( reservation =>
            {
                //CheckStatusExceptions(reservation);
                _unit.ReservationRepository.Update(reservation, false);
                 SetReservationStatus(reservation,_emailSender,_userManager);
            });
           
            await _unit.SaveChangesAsync();
            return await Task.FromResult(Unit.Value);
        }

        private static void SetReservationStatus(Reservation reservation,IEmailSender _emailSender,
            CustomUserManager<AppUser> _userManager)
        {
            int reservedDays = reservation.CheckOutDate.Subtract(reservation.CheckInDate).Days;
            int daysLeftTillCheckIn = reservation.CheckInDate.Subtract(DateTime.Now).Days;
            int daysLeftTillCheckOut = reservation.CheckOutDate.Subtract(DateTime.Now).Days;
            TimeSpan hoursLeftTillCheckIn = reservation.CheckInDate.Subtract(DateTime.Now);

            if (DateTime.Now > reservation.CheckOutDate)
            {
                reservation.Status = (int)Enum_ReservationStatus.ReservationFinished;
                SendReservationFinishedEmail(reservation, _emailSender, _userManager).GetAwaiter().GetResult();
            }
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
            {
                reservation.Status = (int)Enum_ReservationStatus.ReservationFinished;
                SendReservationFinishedEmail(reservation, _emailSender, _userManager).GetAwaiter().GetResult();
            }
            else
                // bura dushe bilmez amma yoxlamaq uchun yazmisham
                reservation.Status = 19;
           
}

        private static async Task SendReservationFinishedEmail(Reservation reservation, IEmailSender _emailSender, CustomUserManager<AppUser> _userManager)
        {
            AppUser user = await _userManager.FindByIdAsync(reservation.AppUserId.ToString());
            await EmailSenderHelpers.SendReservedFinishedEmail(user, reservation, _emailSender);
        }

        // buna ehtiyac yoxdu
        //private static void CheckStatusExceptions(Reservation reservation)
        //{
        //    if (reservation.Status == (int)Enum_ReservationStatus.ReservationCancelled)
        //        throw new Reservation_ActionToCancelledReservationException();
        //    if (reservation.Status == (int)Enum_ReservationStatus.ReservationFinished)
        //        throw new Reservation_StatusAlreadyFinishedException();
        //}
    }
}
