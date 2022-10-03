using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Enums.Reservations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, PostReservationResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreateReservationCommandHandler(IUnitOfWork unit, IMapper mapper
            ,IHttpContextAccessor accessor,IEmailSender emailSender,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public async Task<PostReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            //Property property = await _unit.PropertyRepository
            //    .GetByIdAsync(request.PropertyId, null, false, "Host");
            Guid userId = _accessor.HttpContext.User.GetUserIdFromClaim().TryParseStringIdToGuid();
            Property property = await CheckExceptionsThenReturnProperty(request,userId,cancellationToken);
            // host un verdiyi checkInTime i menimsedirik.
            request.CheckInDate = request.CheckInDate.Date + property.CheckInTime;
            request.CheckOutDate = request.CheckOutDate.Date + property.CheckOutTime;
            int reservedDays = CheckMaxGuestThenReturnInt(request, property);

            ReservationHelpers.CheckOutDateValidationChecker(property, reservedDays);
            await CheckIfDateOccupied(request);
            Reservation reservation = _mapper.Map<Reservation>(request);
            SetReservationStatus(reservation);
            ManuallySettingValuesToReservation(request, property, reservation, userId);
            ReservationHelpers.CalculatePrice(reservation, reservedDays);

            await _unit.ReservationRepository.AddAsync(reservation);
            #region send email to the user about successfuly reserving the property
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            await EmailSenderHelpers.SendPropertyReservedEmail(user, reservation, _emailSender);
            #endregion
            return await ReservationHelpers.ReturnResponse(reservation, _unit, _mapper);
        }

        private static void ManuallySettingValuesToReservation(CreateReservationCommand request, 
            Property property, Reservation reservation,Guid userId)
        {
            reservation.Property = property;
            if (request.PetCount != 0)
            {
                if (!reservation.Property.IsPetAllowed) throw new Reservation_PetsAreNotAllowedException();
                reservation.PetCount = request.PetCount;
            }
            reservation.HostId = property.HostId;
            reservation.AppUserId = userId;
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

        private async Task<Property> CheckExceptionsThenReturnProperty(CreateReservationCommand request,
            Guid userId, CancellationToken cancellationToken = default)
        {
            
            Property property = await _unit.PropertyRepository
                .GetByIdAsync(request.PropertyId, null, true, "Host","PropertyImages");
            if (property.Host.AppUserId == userId) 
                throw new Reservation_CantReserveYourOwnPropertyException();
            
            return property;
            //if (property is null) throw new PropertyNotFoundException();
            //AppUser user = await _userManager.Users.GetUserByIdAsync(request.AppUserId, cancellationToken);
            //if (user is null) throw new UserIdNotFoundException();
            //Host host = await _unit.HostRepository.GetByIdAsync(request.HostId, null);

            //if (host is null) throw new HostNotFoundException(request.HostId);

            //return new Property();
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
            x.CheckInDate >= request.CheckInDate && x.CheckInDate <= request.CheckOutDate, null);
            if (containsOccupiedDate.Count != 0) throw new ReservationContainsOccupiedDateException();
        }

        private async Task CheckIfCheckOutIsOccupied(CreateReservationCommand request)
        {
            Reservation occupiedCheckOutTime = await _unit.ReservationRepository
                            .GetSingleAsync(x => x.CheckInDate <= request.CheckOutDate
                            && x.CheckOutDate >= request.CheckOutDate,false);
            //if (occupiedCheckOutTime.Count != 0)
            if(occupiedCheckOutTime is not null)
                throw new ReservationCheckOutOccupiedException(request.CheckOutDate);
        }


        private async Task CheckIfCheckInIsOccupied(CreateReservationCommand request)
        {
            Reservation occupiedCheckInTime = await _unit.ReservationRepository
                .GetSingleAsync(x => x.CheckInDate <= request.CheckInDate
                && x.CheckOutDate >= request.CheckInDate,false);
            if (occupiedCheckInTime is not null)
                throw new ReservationCheckInOccupiedException(request.CheckInDate);
        }
    }
}
