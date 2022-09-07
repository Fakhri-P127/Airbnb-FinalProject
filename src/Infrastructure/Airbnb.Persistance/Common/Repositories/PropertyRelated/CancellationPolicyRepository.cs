using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common.Repositories.PropertyRelated
{
    public class CancellationPolicyRepository : GenericRepository<CancellationPolicy>, ICancellationPolicyRepository
    {
        public CancellationPolicyRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
