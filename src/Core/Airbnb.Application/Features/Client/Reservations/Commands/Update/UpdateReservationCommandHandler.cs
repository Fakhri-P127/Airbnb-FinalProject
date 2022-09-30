using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Features.Client.Reservations.Commands.Create;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Update
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, PostReservationResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateReservationCommandHandler(IUnitOfWork unit, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<PostReservationResponse> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            Reservation reservation = await _unit.ReservationRepository.GetByIdAsync(Id, null,true,
                "Property", "Host");

            if (reservation is null) throw new ReservationNotFoundException(Id);
            CheckMaxGuest(request, reservation.Property);
            SetReqCheckInAndOutTimes(request, reservation);

            int existedReservedDays = reservation.CheckOutDate.Subtract(reservation.CheckInDate).Days;
            int reservedDays = (request.CheckOutDate - request.CheckInDate).Value.Days;
            ReservationHelpers.CheckOutDateValidationChecker(reservation.Property, reservedDays);
            await CheckIfDateOccupied(request, reservation);
            _unit.ReservationRepository.Update(reservation);

            int requestGuestCount = request.AdultCount + request.ChildCount;
            // bu deyeri evvelceden yadda saxlamaliyam ki mapdan sonra if shertinde vere bilim, evvelceden menimsetmesem
            // onda if shertini yoxlamaq alinmayacaq
            int reservedGuestCount = reservation.AdultCount + reservation.ChildCount;
            _mapper.Map(request, reservation);
            ManuallySettingValuesToReservation(request, reservation);
            // eger guest countda deyishiklik varsa qiymeti yeniden hesablasin, yoxdusa ehtiyac yoxdu
            if (requestGuestCount != reservedGuestCount || existedReservedDays != reservedDays)
                ReservationHelpers.CalculatePrice(reservation, reservedDays);
            await _unit.SaveChangesAsync();
            return await ReservationHelpers.ReturnResponse(reservation, _unit, _mapper);

        }
        private static void ManuallySettingValuesToReservation(UpdateReservationCommand request, Reservation reservation)
        {
            if (request.PetCount != 0)
            {
                if (!reservation.Property.IsPetAllowed) throw new Reservation_PetsAreNotAllowedException();
                reservation.PetCount = request.PetCount;
            }
        }
        private static void SetReqCheckInAndOutTimes(UpdateReservationCommand request, Reservation reservation)
        {
            if (request.CheckInDate is not null)
            {
                DateTime reqCheckInDate = (DateTime)request.CheckInDate;
                request.CheckInDate = reqCheckInDate.Date + reservation.Property.CheckInTime;
            }
            if (request.CheckOutDate is not null)
            {
                DateTime reqCheckOutDate = (DateTime)request.CheckOutDate;
                request.CheckOutDate = reqCheckOutDate.Date + reservation.Property.CheckOutTime;
            }

            // eger gonderdilen requestde checkin ya da check out verilmeyibse rezervasiyadakina beraber olsun
            request.CheckInDate ??= reservation.CheckInDate;
            request.CheckOutDate ??= reservation.CheckOutDate;
        }

        private static void CheckMaxGuest(UpdateReservationCommand request,
            Property property)
        {
            int totalGuestCount = request.AdultCount + request.ChildCount;
            if (property.MaxGuestCount < totalGuestCount)
                throw new ReservationMaxGuestCountValidationException
                    (property.MaxGuestCount, totalGuestCount);
        }

        private async Task CheckIfDateOccupied(UpdateReservationCommand request, Reservation reservation)
        {
            await CheckIfCheckInIsOccupied(request, reservation);
            await CheckIfCheckOutIsOccupied(request, reservation);

            #region comment
            // meselcun: oktyabrin 1-inden 31-ne kimi rezerv edirsen amma icherisinde
            // oktyabrin 3-5; 7-9;10-15;20-26 kimi rezervler var. Bu zaman yuxaridaki yoxlamalar
            // exception tullamayacaq. Bashlangic ve sonu butun ehate edirse bu exceptiona dushun.
            // Bunu checkout la da ede bilerdik, bir ferqi yoxdu bildiyim qederile
            #endregion
            await CheckIfItContainsOccupiedDate(request, reservation);

        }

        private async Task CheckIfItContainsOccupiedDate(UpdateReservationCommand request, Reservation reservation)
        {
            List<Reservation> containsOccupiedDate = await _unit.ReservationRepository.GetAllAsync(x =>
                        x.CheckInDate >= request.CheckInDate && x.CheckInDate <= request.CheckOutDate
                        && x.Id != reservation.Id, null);
            if (containsOccupiedDate.Count != 0) throw new ReservationContainsOccupiedDateException();
        }

        private async Task CheckIfCheckOutIsOccupied(UpdateReservationCommand request, Reservation reservation)
        {
            List<Reservation> occupiedCheckOutTime = await _unit.ReservationRepository
                .GetAllAsync(x => x.CheckInDate <= request.CheckOutDate
                && x.CheckOutDate >= request.CheckOutDate && x.Id != reservation.Id, null);
            if (occupiedCheckOutTime.Count != 0)
                throw new ReservationCheckOutOccupiedException(request.CheckOutDate);
        }

        private async Task CheckIfCheckInIsOccupied(UpdateReservationCommand request, Reservation reservation)
        {
            List<Reservation> occupiedCheckInTime = await _unit.ReservationRepository
                            .GetAllAsync(x => x.CheckInDate <= request.CheckInDate
                            && x.CheckOutDate >= request.CheckInDate && x.Id != reservation.Id,null);
            if (occupiedCheckInTime.Count != 0)
                throw new ReservationCheckInOccupiedException(request.CheckInDate);
        }
    }
}
