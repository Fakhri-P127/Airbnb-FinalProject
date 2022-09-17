using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Exceptions.PropertyReviews;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Update
{
    public class UpdatePropertyReviewCommandHandler : IRequestHandler<UpdatePropertyReviewCommand, PropertyReviewResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UpdatePropertyReviewCommandHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<PropertyReviewResponse> Handle(UpdatePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            PropertyReview propertyReview = await _unit.PropertyReviewRepository.GetByIdAsync(request.Id, null);
            if (propertyReview is null) throw new PropertyReview_NotFoundException(request.Id);
            _unit.PropertyReviewRepository.Update(propertyReview);
            _mapper.Map(request,propertyReview);
            await _unit.SaveChangesAsync();
            return await PropertyReviewHelper.ReturnResponse(propertyReview, _unit, _mapper);
        }
    }
}
