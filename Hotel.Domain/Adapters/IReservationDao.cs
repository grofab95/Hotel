using Hotel.Domain.Entities;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IReservationDao
    {
        Task<int> CreateReservationAsync(Reservation reservation);
    }
}
