using Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses;
using MediatR;

namespace Airbnb.Application.Features.Client.PropertyReviews.Commands.Update
{
    public class UpdatePropertyReviewCommand:IRequest<PropertyReviewResponse>
    {
        public string Text { get; set; }
        //public float OverallScore { get; set; }
        public float CleanlinessScore { get; set; }
        public float CommunicationScore { get; set; }
        public float CheckInScore { get; set; }
        public float AccuracyScore { get; set; }
        public float LocationScore { get; set; }
        public float ValueScore { get; set; }
        // check ele ki AppUserId bu property uchun edilen bookingde Id si var :# 
        // AppUser i check ele ki eyni id di ya yo
        public float AverageOverallScore()
        {
            float[] scores = new float[]
            { CheckInScore, CleanlinessScore, LocationScore, ValueScore, AccuracyScore, CommunicationScore };

            return Enumerable.Average(scores);
        }
    }
}
