using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Sql.ContextFactory;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class ReservationDao : BaseDao, IReservationDao
    {
        public ReservationDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        {  }

        public async Task<int> CreateReservationAsync(Reservation reservation)
        {
            var areas = reservation.ReservationRooms.Select(x => x.Room.Area).ToList();
            var rooms = reservation.ReservationRooms.Select(x => x.Room).ToList();

            AttachEntries(areas);
            AttachEntries(rooms);

            await AddEntry(reservation);

            return reservation.Id;
        }
    }
}
