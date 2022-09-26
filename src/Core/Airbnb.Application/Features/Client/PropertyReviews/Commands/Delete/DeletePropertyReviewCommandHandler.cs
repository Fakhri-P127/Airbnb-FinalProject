using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.PropertyReviews;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Delete
{
    public class DeletePropertyReviewCommandHandler : IRequestHandler<DeletePropertyReviewCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeletePropertyReviewCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeletePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            PropertyReview propertyReview = await _unit.PropertyReviewRepository.GetByIdAsync(request.Id,
                null,true);
            if (propertyReview is null) throw new PropertyReview_NotFoundException(request.Id);
            await _unit.PropertyReviewRepository.DeleteAsync(propertyReview);
            return await Task.FromResult(Unit.Value);
        }
    }
}
