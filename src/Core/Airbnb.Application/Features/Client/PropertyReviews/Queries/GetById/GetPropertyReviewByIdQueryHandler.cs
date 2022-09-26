using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Exceptions.PropertyReviews;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.PropertyReviews.Queries.GetById
{
    public class GetPropertyReviewByIdQueryHandler : IRequestHandler<GetPropertyReviewByIdQuery, PropertyReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetPropertyReviewByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PropertyReviewResponse> Handle(GetPropertyReviewByIdQuery request, CancellationToken cancellationToken)
        {
            PropertyReview propertyReview = await _unit.PropertyReviewRepository
                .GetByIdAsync(request.Id,request.Expression,false, PropertyReviewHelper.AllPropertyReviewIncludes());
            if (propertyReview is null) throw new PropertyReview_NotFoundException(request.Id);
            PropertyReviewResponse response = _mapper.Map<PropertyReviewResponse>(propertyReview);
            if (response is null) throw new Exception("Internal server error");
            return response;
        }
    }
}
