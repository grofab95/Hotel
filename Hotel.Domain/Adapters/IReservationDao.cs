using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IReservationDao : IDao<Reservation>
    {
        Task<int> GetTotalAsync();
        Task<List<Reservation>> GetAllAsync(int page, int limit);
        Task<List<ReservationInfoView>> GetReservationBasicInfosAsync();
        //Task<List<ReservationInfoView>> SearchReservations(Expression<Func<ReservationInfoView, bool>> query);
        IQueryable<ReservationInfoView> SearchReservations();
    }
}
