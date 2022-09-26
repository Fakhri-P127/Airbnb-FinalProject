using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Create
{
    public class CreateGuestReviewCommandHandler : IRequestHandler<CreateGuestReviewCommand, GuestReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreateGuestReviewCommandHandler(IUnitOfWork unit,IMapper mapper,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<GuestReviewResponse> Handle(CreateGuestReviewCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = await CheckIfNotFoundThenReturnReservation(request,cancellationToken);
            ExceptionChecks(request, reservation);
            GuestReview guestReview = _mapper.Map<GuestReview>(request);
            //guestReview.Reservation.AppUserId   mappingde bunu yoxla gor ozu include edir ya yo
            guestReview.AppUserId = reservation.AppUserId;
            await _unit.GuestReviewRepository.AddAsync(guestReview);
            return await GuestReviewHelper.ReturnResponse(guestReview, _unit, _mapper);
        }

        private static void ExceptionChecks(CreateGuestReviewCommand request, Reservation reservation)
        {
            if (reservation.GuestReview is not null)
                throw new GuestReviewDuplicateValidationException(request.ReservationId);
            //if (request.AppUserId != reservation.AppUserId)
            //    throw new GuestReview_UserIdNotMatchedException(request.AppUserId, reservation.AppUserId);
        }

        private async Task<Reservation> CheckIfNotFoundThenReturnReservation(CreateGuestReviewCommand request, CancellationToken cancellationToken=default)
        {
            Host host = await _unit.HostRepository.GetByIdAsync(request.HostId, null);
            if (host is null) throw new HostNotFoundException(request.HostId);
            Reservation reservation = await _unit.ReservationRepository
                .GetByIdAsync(request.ReservationId, null,false, "GuestReview");
            if (reservation is null) throw new ReservationNotFoundException(request.ReservationId);
            
            //AppUser user = await _userManager.Users.GetUserByIdAsync(request.AppUserId, cancellationToken);
            //if (user is null) throw new UserIdNotFoundException();
            return reservation;
        }
    }
}
