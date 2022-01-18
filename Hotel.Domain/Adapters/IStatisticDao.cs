using Hotel.Domain.Entities.StatisticEntities;
using Hotel.Domain.Entities.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters;

public interface IStatisticDao
{
    Task<List<ReservationInfoView>> GetAllAsync();
}