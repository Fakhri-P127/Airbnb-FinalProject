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
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Create
{
    public class CreatePropertyReviewCommandHandler : IRequestHandler<CreatePropertyReviewCommand, PropertyReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreatePropertyReviewCommandHandler(IUnitOfWork unit,IMapper mapper,
            IHttpContextAccessor accessor, CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
            _userManager = userManager;
        }
        public async Task<PropertyReviewResponse> Handle(CreatePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = await CheckNotFoundsThenReturnReservation(request,cancellationToken);
            PropertyReview propertyReview = _mapper.Map<PropertyReview>(request);
            propertyReview.HostId = reservation.HostId;
            propertyReview.AppUserId = _accessor.HttpContext.User.GetUserIdFromClaim().TryParseStringIdToGuid();
            await _unit.PropertyReviewRepository.AddAsync(propertyReview);
            return await PropertyReviewHelper.ReturnResponse(propertyReview, _unit, _mapper);
        }

        private async Task<Reservation> CheckNotFoundsThenReturnReservation(CreatePropertyReviewCommand request,CancellationToken cancellationToken=default)
        {
            Reservation reservation = await _unit.ReservationRepository
                .GetByIdAsync(request.ReservationId, null,true);
            if (reservation is null) throw new ReservationNotFoundException(request.ReservationId);
            return reservation;
            //AppUser user = await _userManager.Users.GetUserByIdAsync(request.AppUserId,cancellationToken);
            //if (user is null) throw new UserIdNotFoundException(); 
            // bashqasininn datasini deyishe bilmerem authmiddleware de yazmisham deye bura dushe bilmez.
            //if (reservation.AppUserId != user.Id) throw new PropertyReview_UserIdNotMatchedException(request.AppUserId, (Guid)reservation.AppUserId);

        }
    }
}
