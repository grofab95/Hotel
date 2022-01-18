using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters;

public interface IRoomDao : IAddDao<Room>, IModifyDao<Room>, IGetDao<Room>
{
    Task<List<Room>> GetFreeByDateRangeAsync(int peopleAmount, DateRange dateRange);
}