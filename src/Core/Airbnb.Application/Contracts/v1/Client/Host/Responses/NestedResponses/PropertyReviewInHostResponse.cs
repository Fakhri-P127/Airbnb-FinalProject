using Airbnb.Domain.Entities.AppUserRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Host.Responses.NestedResponses
{
    public class PropertyReviewInHostResponse
    {
        public string Text { get; set; }
        public float OverallScore { get; set; }
        public Guid AppUserId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
