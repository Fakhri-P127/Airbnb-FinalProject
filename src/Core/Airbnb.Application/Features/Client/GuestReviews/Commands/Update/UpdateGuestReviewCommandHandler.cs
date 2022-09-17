using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
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

        public UpdateGuestReviewCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GuestReviewResponse> Handle(UpdateGuestReviewCommand request, CancellationToken cancellationToken)
        {
            GuestReview guestReview = await _unit.GuestReviewRepository.GetByIdAsync(request.Id, null);
            if (guestReview is null) throw new GuestReviewNotFoundException(request.Id);
            _unit.GuestReviewRepository.Update(guestReview);
            _mapper.Map(request, guestReview);
            //if (request.GuestScore is not null) guestReview.GuestScore = (float)request.GuestScore;
            await _unit.SaveChangesAsync();
            return await GuestReviewHelper.ReturnResponse(guestReview, _unit, _mapper);

        }
    }
}
