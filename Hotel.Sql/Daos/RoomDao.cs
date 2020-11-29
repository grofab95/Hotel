using Hotel.Domain.Adapters;
using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;
using Hotel.Domain.Environment;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                .Where(x => dateRange.From >= x.Reservation.CheckIn && dateRange.From <= x.Reservation.CheckOut)
                .Select(x => new { Id = x.Room.Id, ReservedTo = x.Reservation.CheckOut })
                .ToListAsync();

            var resRooms = await context.ReservationRooms.Include(z => z.Room).Include(x => x.Reservation).ToListAsync();

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

            joined.ForEach(x =>
            {
                var date = x.info.ReservedTo;
                x.reservedRoom
                    .SetNote($"Wolny od {new DateTime(date.Year, date.Month, date.Day, Config.Get.FreeRoomHour, 0, 0):dd.MM.yyyy HH:mm}");
            });

            return freeRooms.Concat(reservedRooms).ToList();
        }

        public async Task<Room> AddAsync(Room room)
        {
            await context.Rooms.AddAsync(room);
            await context.SaveChangesAsync();

            return room;
        }

        public async Task UpdateAsync(Room room) => await UpdateEntry(room);

        public Task<List<Room>> GetManyAsync(Expression<Func<Room, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetAsync(Expression<Func<Room, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task<Room> IDao<Room>.UpdateAsync(Room entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
