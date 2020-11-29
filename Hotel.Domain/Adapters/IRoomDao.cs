using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IRoomDao : IDao<Room>
    {
        Task<List<Room>> GetFreeByDateRangeAsync(int peopleAmount, DateRange dateRange);
    }
}
