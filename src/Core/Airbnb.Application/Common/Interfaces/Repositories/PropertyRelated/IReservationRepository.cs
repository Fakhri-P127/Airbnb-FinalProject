using Airbnb.Application.Common.Interfaces.Repositories.Common;
using Airbnb.Application.Contracts.v1.Client.Reservation.Parameters;
using Airbnb.Domain.Entities.PropertyRelated;
using System.Linq.Expressions;

namespace Airbnb.Application.Common.Interfaces.Repositories.PropertyRelated
{
    public interface IReservationRepository:IGenericRepository<Reservation>
    {
        //Task<List<Reservation>> GetAllReservations(ReservationParameters parameters,
        //    Expression<Func<Reservation, bool>> expression, bool tracked = false, params string[] includes);
    }
}
