using Dapper;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Views;
using Hotel.SqlDapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.SqlDapper.Daos
{
    public class ReservationDao : IReservationDao
    {
        public Task<Reservation> AddAsync(Reservation entity)
        {
            throw new NotImplementedException();
        }

        public Task AddGuestToReservationRoomAsync(int reservationId, int guestId)
        {
            throw new NotImplementedException();
        }

        public Task AddRoomToReservationAsync(int reservationId, int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAllReservationsAsync()
        {
            using var db = ConnectionFactory.Build();
            var query = "SELECT Count(*) FROM Reservations";
            return await db.ExecuteScalarAsync<int>(query);
        }

        public async Task<int> CountCustomerReservationsAsync(int customerId)
        {
            using var db = ConnectionFactory.Build();
            var query = "SELECT Count(*) FROM Reservations WHERE CustomerId=@customerId";
            return await db.ExecuteScalarAsync<int>(query, new { customerId });
        }

        public async Task<Reservation> CreateReservation(int customerId, DateRange dateRange)
        {
            using var db = ConnectionFactory.Build();
            var query = @"INSERT INTO Reservations (CustomerId, CheckIn, CheckOut)" +
                         "VALUES (customerId, checkIn, checkOut)";

            var id = await db.ExecuteAsync(query, new { customerId, dateRange.From, dateRange.To });
            return await GetReservationAsync(id);
        }

        public async Task<Reservation> GetReservationAsync(int reservationId)
        {
            using var db = ConnectionFactory.Build();
            var query = @"SELECT * 
                          FROM Reservations r 
                          INNER JOIN Customers c ON C.Id = r.CustomerId
                          INNER JOIN ReservationRooms rr ON rr.ReservationId = r.Id 
                          INNER JOIN Rooms ro ON ro.Id = rr.RoomId
                          INNER JOIN Areas a ON a.Id = ro.AreaId
                          INNER JOIN Guests g ON g.ReservationRoomId = rr.Id
                          WHERE r.Id=@reservationId";

            var test = db.QueryMultiple(query, new { reservationId })
                .Map<Reservation, Customer, int>(
                    r => r.Customer.Id,
                    c => c.Id,
                    (r, c) => {  }
                ).FirstOrDefault();

            var res = await db.QueryFirstOrDefaultAsync<Reservation>(query, new { reservationId });
            return res;
        }

        public Task<List<ReservationInfoView>> GetReservationBasicInfosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetReservationPriceAsync(int reservationId)
        {
            using var db = ConnectionFactory.Build();
            var query = "SELECT TotalPrice FROM Reservations WHERE Id=@id";
            return await db.ExecuteScalarAsync<decimal>(query, new { reservationId });
        }

        public Task<decimal> GetReservationPriceForDay(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reservation>> GetReservationsAsync(int page, int limit, Expression<Func<Reservation, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGuestFromReservationRoomAsync(int reservationId, int guestId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRoomFromReservationAsync(int reservationId, int roomId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ReservationInfoView> SearchReservations()
        {
            throw new NotImplementedException();
        }
    }
}
