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
    public interface IReservationDao : IAddDao<Reservation>, IModifyDao<Reservation>, IGetDao<Reservation>, ICountDao<Reservation>
    {
        Task<List<Reservation>> GetReservationsAsync(int page, int limit, Expression<Func<Reservation, bool>> predicate);
        Task<List<ReservationInfoView>> GetReservationBasicInfosAsync();
        IQueryable<ReservationInfoView> SearchReservations();

    }
}
