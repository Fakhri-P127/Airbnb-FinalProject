using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.GuestReviews.Commands.Delete
{
    public class DeleteGuestReviewCommandHandler : IRequestHandler<DeleteGuestReviewCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteGuestReviewCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteGuestReviewCommand request, CancellationToken cancellationToken)
        {
            GuestReview guestReview = await _unit.GuestReviewRepository.GetByIdAsync(request.Id, null,true);
            if (guestReview is null) throw new GuestReviewNotFoundException(request.Id);
            await _unit.GuestReviewRepository.DeleteAsync(guestReview);
            return await Task.FromResult(Unit.Value);
        }
    }
}
