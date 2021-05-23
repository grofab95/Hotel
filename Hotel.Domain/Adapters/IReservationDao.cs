using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IReservationDao : IDao<Reservation>
    {
        Task<int> GetTotalAsync(Expression<Func<Reservation, bool>> predicate);
        Task<List<Reservation>> GetAllAsync(int page, int limit, Expression<Func<Reservation, bool>> predicate = null);
        Task<List<ReservationInfoView>> GetReservationBasicInfosAsync();
        IQueryable<ReservationInfoView> SearchReservations();

    }
}
