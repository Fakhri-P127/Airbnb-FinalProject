using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses.NestedResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Create
{
    public class CreatePropertyReviewCommand : IRequest<PropertyReviewResponse>
    {
        public string Text { get; set; }
        //public float OverallScore { get; set; }
        public float CleanlinessScore { get; set; }
        public float CommunicationScore { get; set; }
        public float CheckInScore { get; set; }
        public float AccuracyScore { get; set; }
        public float LocationScore { get; set; }
        public float ValueScore { get; set; }

        //public Guid AppUserId { get; set; }
        public Guid ReservationId { get; set; }

        public float AverageOverallScore()
        {
            float[] scores = new float[]
            { CheckInScore, CleanlinessScore, LocationScore, ValueScore, AccuracyScore, CommunicationScore };

            return Enumerable.Average(scores);
        }

    }
}
