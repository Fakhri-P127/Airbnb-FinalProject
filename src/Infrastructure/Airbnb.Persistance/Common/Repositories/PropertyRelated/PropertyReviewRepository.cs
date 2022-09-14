using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Common.Repositories.PropertyRelated
{
    public class PropertyReviewRepository : GenericRepository<PropertyReview>, IPropertyReviewRepository
    {
        public PropertyReviewRepository(AirbnbDbContext context) : base(context)
        {
        }
    }
}
