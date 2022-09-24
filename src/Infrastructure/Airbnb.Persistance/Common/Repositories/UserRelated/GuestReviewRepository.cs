using Airbnb.Application.Common.Interfaces.Repositories.UserRelated;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common.Repositories.UserRelated
{
    public class GuestReviewRepository : GenericRepository<GuestReview>, IGuestReviewRepository
    {
        public GuestReviewRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
