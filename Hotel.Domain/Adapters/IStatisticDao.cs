using Hotel.Domain.Entities.StatisticEntities;
using Hotel.Domain.Entities.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IStatisticDao
    {
        Task<Turnover> GetTurnoverAsync(int year, int? month);
        Task<int> GetFirstReservationYearAsync();
        Task<Dictionary<int, List<int>>> GetReservationsMonthsAsync();
        Task<List<ReservationInfoView>> GetAllAsync();
    }
}
