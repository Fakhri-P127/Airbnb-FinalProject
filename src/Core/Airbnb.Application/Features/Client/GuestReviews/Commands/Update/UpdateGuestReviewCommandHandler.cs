using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Update
{
    public class UpdateGuestReviewCommandHandler : IRequestHandler<UpdateGuestReviewCommand, GuestReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdateGuestReviewCommandHandler(IUnitOfWork unit, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<GuestReviewResponse> Handle(UpdateGuestReviewCommand request, CancellationToken cancellationToken)
        {
            GuestReview guestReview = await CheckExceptionsThenReturnGuestReview();
            _unit.GuestReviewRepository.Update(guestReview);
            _mapper.Map(request, guestReview);
            await _unit.SaveChangesAsync();
            return await GuestReviewHelper.ReturnResponse(guestReview, _unit, _mapper);
        }

        private async Task<GuestReview> CheckExceptionsThenReturnGuestReview()
        {
            // guid olmadan gondersem evvelceden tutacaq ve bu error hech vaxt ishlemeyecek amma yenede her ehtimala qarshi yazdim
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            GuestReview guestReview = await _unit.GuestReviewRepository.GetByIdAsync(Id, null, true, "Reservation");
            if (guestReview is null) throw new GuestReviewNotFoundException(Id);
            Guid userId = _accessor.HttpContext.User.GetUserIdFromClaim().TryParseStringIdToGuid();
            Host host = await _unit.HostRepository.GetSingleAsync(x => x.AppUserId == userId);
            if (host is null) throw new HostNotFoundException();//authorize roles a gore bu null ola bilmez amma yenede yoxladim
            if (guestReview.Reservation.HostId != host.Id)
                throw new GuestReview_HostIdNotMatchedException(host.Id, guestReview.Reservation.HostId);
            return guestReview;
        }
    }
}
