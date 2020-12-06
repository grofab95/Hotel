using Hotel.Domain.Entities.StatisticEntities;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IStatisticDao
    {
        Task<Turnover> GetTurnoverAsync(int year, int? month);
        Task<int> GetFirstReservationYearAsync();
    }
}
