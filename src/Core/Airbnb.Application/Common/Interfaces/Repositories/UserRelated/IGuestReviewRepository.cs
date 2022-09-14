using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Domain.Entities.AppUserRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Interfaces.Repositories.UserRelated
{
    public interface IGuestReviewRepository:IGenericRepository<GuestReview>
    {
    }
}
