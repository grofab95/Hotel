using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class RoomDao : BaseDao, IRoomDao
    {
        public RoomDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }

        public async Task<List<Room>> GetFreeByDateRangeAsync(int peopleAmount, DateRange dateRange)
        {
            var reservedRoomsInfos = await context.ReservationRooms.Include(z => z.Room).Include(x => x.Reservation)
                .Where(x => x.Reservation.CheckIn >= dateRange.From && x.Reservation.CheckOut <= dateRange.To)
                .Select(x => new { Id = x.Room.Id, ReservedTo = x.Reservation.CheckOut })
                .ToListAsync();

            var reserverdRoomsIds = reservedRoomsInfos.Select(x => x.Id).ToList();
            var freeRooms = await context.Rooms.Include(x => x.Area)
                .Where(x => !reserverdRoomsIds.Contains(x.Id))
                .ToListAsync();

            if (freeRooms.Sum(x => x.PeopleCapacity) >= peopleAmount)
                return freeRooms;

            var reservedRooms = await context.Rooms.Include(x => x.Area)
                .Where(x => reserverdRoomsIds.Contains(x.Id))
                .ToListAsync();

            var joined = (from reservedRoom in reservedRooms
                          join info in reservedRoomsInfos on reservedRoom.Id equals info.Id
                          select new { reservedRoom, info }).ToList();

            joined.ForEach(x => x.reservedRoom.SetNote($"Wolny od {x.info.ReservedTo.AddHours(15):dd.MM.yyyy yy:mm}"));

            return freeRooms.Concat(reservedRooms).ToList();
        }

        public async Task<List<Room>> GetAllAsync() => await context.Rooms.ToListAsync();

        public async Task<int> AddAsync(Room room)
        {
            await context.Rooms.AddAsync(room);
            await context.SaveChangesAsync();

            return room.Id;
        }

        public async Task UpdateAsync(Room room) => await UpdateEntry(room);
    }
}
