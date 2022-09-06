using Airbnb.Application.Common.Interfaces.Repositories;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Persistance.Context;
using Airbnb.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Common.Repositories
{
    public class AirCoverRepository : GenericRepository<AirCover>, IAirCoverRepository
    {
        public AirCoverRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
