using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Enums.Reservations;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, PostReservationResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CreateReservationCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PostReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Property property = await CheckIfNotFoundThenReturnProperty(request);
            
            // host un verdiyi checkInTime i menimsedirik.
            request.CheckInDate = request.CheckInDate.Date + property.CheckInTime;
            request.CheckOutDate = request.CheckOutDate.Date + property.CheckOutTime;
            int reservedDays = CheckMaxGuestThenReturnInt(request, property);
           
            ReservationHelpers.CheckOutDateValidationChecker(property, reservedDays);
            await CheckIfDateOccupied(request);
            Reservation reservation = _mapper.Map<Reservation>(request);
            SetReservationStatus(reservation);
            reservation.Property = property;
            ReservationHelpers.CalculatePrice(reservation, reservedDays);
            
            await _unit.ReservationRepository.AddAsync(reservation);
            return await ReservationHelpers.ReturnResponse(reservation, _unit, _mapper);
        }

        private static void SetReservationStatus(Reservation reservation)
        {
            if (reservation.CheckInDate.Subtract(DateTime.Now).Days <= 1)
            {
                reservation.Status = (int)Enum_ReservationStatus.ArrivingSoon;
            }
            else
            {
                reservation.Status = (int)Enum_ReservationStatus.Upcoming;
            }
        }

        private static int CheckMaxGuestThenReturnInt(CreateReservationCommand request,
            Property property)
        {
            int reservedDays = request.CheckOutDate.Subtract(request.CheckInDate).Days;
            int totalGuestCount = request.AdultCount + request.ChildCount;
            if (property.MaxGuestCount < totalGuestCount)
                throw new ReservationMaxGuestCountValidationException
                    (property.MaxGuestCount, totalGuestCount);

            return reservedDays;
        }

        private async Task<Property> CheckIfNotFoundThenReturnProperty(CreateReservationCommand request)
        {
            Property property = await _unit.PropertyRepository
                .GetByIdAsync(request.PropertyId, null,"Host","Host.AppUser");
            if (property is null) throw new PropertyNotFoundException();
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.AppUserId, null);
            if (user is null) throw new UserIdNotFoundException();
            Host host = await _unit.HostRepository.GetByIdAsync(request.HostId, null);

            if (host is null) throw new HostNotFoundException(request.HostId);
            if (host.AppUserId == user.Id) throw new Reservation_CantReserveYourOwnPropertyException();

            return property;
        }

        private async Task CheckIfDateOccupied(CreateReservationCommand request)
        {
            await CheckIfCheckInIsOccupied(request);
            await CheckIfCheckOutIsOccupied(request);

            #region comment
            // meselcun: oktyabrin 1-inden 31-ne kimi rezerv edirsen amma icherisinde
            // oktyabrin 3-5; 7-9;10-15;20-26 kimi rezervler var. Bu zaman yuxaridaki yoxlamalar
            // exception tullamayacaq. Bashlangic ve sonu butun ehate edirse bu exceptiona dushun.
            // Bunu checkout la da ede bilerdik, bir ferqi yoxdu bildiyim qederile
            #endregion
            await CheckIfItContainsOccupiedDate(request);

        }

        private async Task CheckIfItContainsOccupiedDate(CreateReservationCommand request)
        {
            List<Reservation> containsOccupiedDate = await _unit.ReservationRepository.GetAllAsync(x =>
            x.CheckInDate >= request.CheckInDate && x.CheckInDate <= request.CheckOutDate);
            if (containsOccupiedDate.Count != 0) throw new ReservationContainsOccupiedDateException();
        }

        private async Task CheckIfCheckOutIsOccupied(CreateReservationCommand request)
        {
            List<Reservation> occupiedCheckOutTime = await _unit.ReservationRepository
                            .GetAllAsync(x => x.CheckInDate <= request.CheckOutDate
                            && x.CheckOutDate >= request.CheckOutDate);
            if (occupiedCheckOutTime.Count != 0)
                throw new ReservationCheckOutOccupiedException(request.CheckOutDate);
        }
        

        private async Task CheckIfCheckInIsOccupied(CreateReservationCommand request)
        {
            List<Reservation> occupiedCheckInTime = await _unit.ReservationRepository
                .GetAllAsync(x => x.CheckInDate <= request.CheckInDate
                && x.CheckOutDate >= request.CheckInDate);
            if (occupiedCheckInTime.Count != 0)
                throw new ReservationCheckInOccupiedException(request.CheckInDate);
        }
    }
}
