using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.AppUser;
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

        public CreateGuestReviewCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GuestReviewResponse> Handle(CreateGuestReviewCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = await CheckIfNotFoundThenReturnReservation(request);
            ExceptionChecks(request, reservation);
            GuestReview guestReview = _mapper.Map<GuestReview>(request);
            await _unit.GuestReviewRepository.AddAsync(guestReview);
            return await GuestReviewHelper.ReturnResponse(guestReview, _unit, _mapper);

        }

        private static void ExceptionChecks(CreateGuestReviewCommand request, Reservation reservation)
        {
            if (reservation.GuestReview is not null)
                throw new GuestReviewDuplicateValidationException(request.ReservationId);
            if (request.AppUserId != reservation.AppUserId)
                throw new GuestReview_UserIdNotMatchedException(request.AppUserId, (Guid)reservation.AppUserId);
        }

        private async Task<Reservation> CheckIfNotFoundThenReturnReservation(CreateGuestReviewCommand request)
        {
            Host host = await _unit.HostRepository.GetByIdAsync(request.HostId, null);
            if (host is null) throw new HostNotFoundException(request.HostId);
            Reservation reservation = await _unit.ReservationRepository
                .GetByIdAsync(request.ReservationId, null,"GuestReview");
            if (reservation is null) throw new ReservationNotFoundException(request.ReservationId);
            
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.AppUserId, null);
            if (user is null) throw new UserIdNotFoundException();
            return reservation;
        }
    }
}
