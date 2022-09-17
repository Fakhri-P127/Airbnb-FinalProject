using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Queries.GetAll
{
    public class GetAllPropertyReviewsQueryHandler : IRequestHandler<GetAllPropertyReviewsQuery, List<PropertyReviewResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllPropertyReviewsQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<PropertyReviewResponse>> Handle(GetAllPropertyReviewsQuery request, CancellationToken cancellationToken)
        {
            List<PropertyReview> propertyReviews = await _unit.PropertyReviewRepository
                .GetAllAsync(request.Expression, PropertyReviewHelper.AllPropertyReviewIncludes());
            List<PropertyReviewResponse> responses = _mapper.Map<List<PropertyReviewResponse>>(propertyReviews);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
