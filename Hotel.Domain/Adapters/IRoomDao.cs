using Hotel.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IRoomDao
    {
        Task<List<Room>> GetFreeByDateRangeAsync(int peopleAmount, DateRange dateRange);
        Task<int> AddAsync(Room room);
        Task UpdateAsync(Room room);
    }
}
