using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.PropertyReviews.Responses.NestedResponses
{
    public class AppUserInPropertyReviewResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
