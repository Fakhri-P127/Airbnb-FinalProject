using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated.StateRelated;
using Airbnb.Domain.Entities.PropertyRelated.StateRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;

namespace Airbnb.Persistance.Common.Repositories.PropertyRelated.StateRelated
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
