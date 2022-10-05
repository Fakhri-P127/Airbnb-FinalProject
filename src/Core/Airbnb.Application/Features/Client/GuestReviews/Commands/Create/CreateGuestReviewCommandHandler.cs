using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Enums.Reservations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Create
{
    public class CreateGuestReviewCommandHandler : IRequestHandler<CreateGuestReviewCommand, GuestReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public CreateGuestReviewCommandHandler(IUnitOfWork unit,IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<GuestReviewResponse> Handle(CreateGuestReviewCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = await CheckExceptionsThenReturnReservation(request,_accessor,cancellationToken);
            GuestReview guestReview = _mapper.Map<GuestReview>(request);
           
            guestReview.AppUserId = reservation.AppUserId;
            guestReview.HostId = reservation.HostId;
            await _unit.GuestReviewRepository.AddAsync(guestReview);
            return await GuestReviewHelper.ReturnResponse(guestReview, _unit, _mapper);
        }


        private async Task<Reservation> CheckExceptionsThenReturnReservation(CreateGuestReviewCommand request
            ,IHttpContextAccessor _accessor, CancellationToken cancellationToken=default)
        {
            Guid userId = _accessor.HttpContext.User.GetUserIdFromClaim().TryParseStringIdToGuid();
            Host host = await _unit.HostRepository.GetSingleAsync(x => x.AppUserId == userId,true,"AppUser");
            if (host is null) throw new HostNotFoundException();
            Reservation reservation = await _unit.ReservationRepository
                .GetByIdAsync(request.ReservationId, null,false, "GuestReview");
            if (reservation is null) throw new ReservationNotFoundException(request.ReservationId);
            if (reservation.HostId != host.Id) 
                throw new GuestReview_HostIdNotMatchedException(host.Id,reservation.HostId);
            if (reservation.Status != (int)Enum_ReservationStatus.ReservationFinished)
                throw new GuestReview_NotAvailableYetException(reservation.CheckOutDate);
            if (reservation.GuestReview is not null)
                throw new GuestReviewDuplicateValidationException(request.ReservationId);
            return reservation;
        }
    }
}
