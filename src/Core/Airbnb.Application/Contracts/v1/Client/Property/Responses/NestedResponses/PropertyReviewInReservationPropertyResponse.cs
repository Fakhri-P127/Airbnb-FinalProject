using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses
{
    public class PropertyReviewInReservationPropertyResponse
    {
        public string Text { get; set; }
        public float OverallScore { get; set; }
        public float CleanlinessScore { get; set; }
        public float CommunicationScore { get; set; }
        public float CheckInScore { get; set; }
        public float AccuracyScore { get; set; }
        public float LocationScore { get; set; }
        public float ValueScore { get; set; }
        // check ele ki AppUserId bu property uchun edilen bookingde Id si var :# 
        // bu appUser reserve i edendi
        public string AppUserId { get; set; }
        public Guid ReservationId { get; set; }

    }
}
