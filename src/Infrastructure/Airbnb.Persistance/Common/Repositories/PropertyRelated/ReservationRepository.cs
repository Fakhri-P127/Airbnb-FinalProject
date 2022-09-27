using Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated;
using Airbnb.Application.Contracts.v1.Client.Reservation.Parameters;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Persistance.Common.Repositories.Common;
using Airbnb.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Persistance.Common.Repositories.PropertyRelated
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AirbnbDbContext context) : base(context)
        {
        }
       

        //public Task<List<Reservation>> GetAllReservations(ReservationParameters parameters, Expression<Func<Reservation, bool>> expression, bool tracked = false, params string[] includes)
        //{
            
        //    IQueryable<T> query = expression is not null ?
        //        _dbSet.Where(expression) : _dbSet.AsQueryable();
        //    query = SetIncludes(query, includes);

        //    return tracked is false ?
        //         await query.AsNoTrackingWithIdentityResolution().ToListAsync() : await query.ToListAsync();
        //}
    }
}
