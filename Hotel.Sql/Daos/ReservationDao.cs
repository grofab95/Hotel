using AutoMapper;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Views;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;
using Hotel.Domain.Validators;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class ReservationDao : BaseDao, IReservationDao
    {
        private IMapper _mapper;

        public ReservationDao(IMapper mapper, IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        {
            _mapper = mapper;
        }

        public async Task<Reservation> AddAsync(Reservation reservation)
        {            
            AttachAggregate(reservation);

            await context.AddAsync(reservation);
            await context.SaveChangesAsync();

            return reservation;
        }

        private void AttachAggregate(Reservation reservation)
        {
            ReservationValidator.Check(reservation);

            var areas = reservation.ReservationRooms.Select(x => x.Room.Area).ToList();
            var rooms = reservation.ReservationRooms.Select(x => x.Room).ToList();
            var reservationRooms = reservation.ReservationRooms.ToList();
            var guests = reservation.ReservationRooms.SelectMany(x => x.Guests).ToList();

            AttachEntries(areas);
            AttachEntries(rooms);
            AttachEntries(reservationRooms);
            AttachEntries(guests);
            AttachEntry(reservation.Customer);
            AttachEntry(reservation);
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
            AttachAggregate(reservation);

            var guests = reservation.ReservationRooms.SelectMany(x => x.Guests).ToList();
            var reservationsRoomsDb = await context.ReservationRooms.Include(x => x.Guests)
                .Where(x => x.ReservationId == reservation.Id)
                .ToListAsync();

            var toDelete = reservationsRoomsDb.GetUnique(reservation.ReservationRooms, x => x.Id, x => x.Id).ToList();
            var toAdd = reservation.ReservationRooms.GetUnique(reservationsRoomsDb, x => x.Id, x => x.Id).ToList();

            context.RemoveRange(toDelete);
            await context.AddRangeAsync(toAdd);

            var guestsId = reservationsRoomsDb.SelectMany(x => x.Guests).Select(x => x.Id).ToList();
            var guestsDb = await context.Guests.Where(x => guestsId.Contains(x.Id)).ToListAsync();

            var guestToDelete = guestsDb.GetUnique(guests, x => x.Id, x => x.Id).ToList();
            var guestToAdd = guests.GetUnique(guestsDb, x => x.Id, x => x.Id).ToList();

            context.RemoveRange(guestToDelete);
            await context.AddRangeAsync(guestToAdd);

            await context.SaveChangesAsync();

            return reservation;
        }

        public async Task<List<ReservationInfoView>> GetReservationBasicInfosAsync()
        {
            return await context.ReservationInfoViews.ToListAsync();
        }

        public IQueryable<ReservationInfoView> SearchReservations()
        {
            return context.ReservationInfoViews;
        }

        public async Task DeleteReservation(int reservationId)
        {
            var reservation = await context.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId)
                ?? throw new HotelException($"Rezerwacja o id {reservationId} nie została odnaleziona.");

            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();
        }

        public Task<List<Reservation>> GetManyAsync(Expression<Func<Reservation, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await context.Reservations.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new HotelException($"Rezerwacja o id {id} nie została odnaleziona.");

            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();
        }

        public async Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return await context.Reservations
                .Include(x => x.Customer)
                .Include(x => x.ReservationRooms).ThenInclude(x => x.Room).ThenInclude(x => x.Area)
                .Include(x => x.ReservationRooms).ThenInclude(x => x.Guests)
                .FirstOrDefaultAsync(predicate)
                    ?? throw new HotelException($"Reserwacja nie istnieje.");
        }

        public async Task<int> GetTotalAsync() => await context.Reservations.CountAsync();

        public async Task<List<Reservation>> GetAllAsync(int page, int limit)
        {
            return await context.Reservations
                .Include(x => x.Customer)
                .Include(x => x.ReservationRooms).ThenInclude(x => x.Room).ThenInclude(x => x.Area)
                .Include(x => x.ReservationRooms).ThenInclude(x => x.Guests)
                .OrderBy(x => x.Id)
                .Pagging(page, limit)
                .ToListAsync();
        }
    }
}
