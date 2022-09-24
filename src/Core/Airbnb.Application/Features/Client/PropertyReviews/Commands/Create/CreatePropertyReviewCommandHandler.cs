using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.PropertyReviews;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Create
{
    public class CreatePropertyReviewCommandHandler : IRequestHandler<CreatePropertyReviewCommand, PropertyReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreatePropertyReviewCommandHandler(IUnitOfWork unit,IMapper mapper,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<PropertyReviewResponse> Handle(CreatePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            // APP USER ID ni yigisdhir ve onu client. suer den getir
            
            Reservation reservation = await CheckNotFoundsThenReturnReservation(request,cancellationToken);
            PropertyReview propertyReview = _mapper.Map<PropertyReview>(request);
            propertyReview.Host = reservation.Host;
            await _unit.PropertyReviewRepository.AddAsync(propertyReview);
            return await PropertyReviewHelper.ReturnResponse(propertyReview, _unit, _mapper);
        }

        private async Task<Reservation> CheckNotFoundsThenReturnReservation(CreatePropertyReviewCommand request,CancellationToken cancellationToken=default)
        {
            Reservation reservation = await _unit.ReservationRepository
                .GetByIdAsync(request.ReservationId, null,"Host","Property");
            if (reservation is null) throw new ReservationNotFoundException(request.ReservationId);
            AppUser user = await _userManager.Users.GetUserByIdAsync(request.AppUserId,cancellationToken);
            if (user is null) throw new UserIdNotFoundException(); 

            if (reservation.AppUserId != user.Id) throw new PropertyReview_UserIdNotMatchedException(request.AppUserId, (Guid)reservation.AppUserId);

            return reservation;
        }
    }
}
