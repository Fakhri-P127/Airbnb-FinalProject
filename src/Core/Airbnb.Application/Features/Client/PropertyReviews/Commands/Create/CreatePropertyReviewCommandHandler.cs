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

        public CreatePropertyReviewCommandHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PropertyReviewResponse> Handle(CreatePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            // APP USER ID ni yigisdhir ve onu client. suer den getir
            
            Reservation reservation = await CheckNotFoundsThenReturnReservation(request);
            PropertyReview propertyReview = _mapper.Map<PropertyReview>(request);
            propertyReview.Host = reservation.Host;
            //propertyReview.Property = reservation.Property;
            await _unit.PropertyReviewRepository.AddAsync(propertyReview);
            return await PropertyReviewHelper.ReturnResponse(propertyReview, _unit, _mapper);
        }

        private async Task<Reservation> CheckNotFoundsThenReturnReservation(CreatePropertyReviewCommand request)
        {
            Reservation reservation = await _unit.ReservationRepository
                .GetByIdAsync(request.ReservationId, null,"Host","Property");
            if (reservation is null) throw new ReservationNotFoundException(request.ReservationId);
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.AppUserId, null);
            if (user is null) throw new UserNotFoundValidationException()
            { ErrorMessage = $"User with this Id({request.AppUserId} doesn't exist." };

            if (reservation.AppUserId != user.Id) throw new PropertyReview_UserIdNotMatchedException(request.AppUserId,reservation.AppUserId);

            return reservation;
        }
    }
}
