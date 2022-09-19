using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // guid olmadan gondersem evvelceden tutacaq ve bu error hech vaxt ishlemeyecek amma yenede her ehtimala qarshi yazdim
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            GuestReview guestReview = await _unit.GuestReviewRepository.GetByIdAsync(Id, null);
            if (guestReview is null) throw new GuestReviewNotFoundException(Id);
            _unit.GuestReviewRepository.Update(guestReview);
            _mapper.Map(request, guestReview);
            await _unit.SaveChangesAsync();
            return await GuestReviewHelper.ReturnResponse(guestReview, _unit, _mapper);
        }
    }
}
