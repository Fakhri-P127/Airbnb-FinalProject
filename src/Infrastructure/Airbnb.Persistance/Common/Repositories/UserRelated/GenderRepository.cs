using Airbnb.Application.Common.Interfaces.Repositories.UserRelated;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Common.Repositories.UserRelated
{
    public class GenderRepository : GenericRepository<Gender>, IGenderRepository
    {
        public GenderRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
