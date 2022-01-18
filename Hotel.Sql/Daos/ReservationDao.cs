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

namespace Hotel.Sql.Daos;

public class ReservationDao : BaseDao<Reservation>, IReservationDao
{
    private IMapper _mapper;

    public ReservationDao(IMapper mapper, IContextFactory<HotelContext> contextFactory) : base(contextFactory)
    {
        _mapper = mapper;
    }

    public override async Task<Reservation> AddAsync(Reservation reservation)
    {            
        AttachAggregate(reservation);
        await base.AddAsync(reservation);
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

    public override async Task<Reservation> UpdateAsync(Reservation reservation)
    {
        AttachAggregate(reservation);

        var guests = reservation.ReservationRooms.SelectMany(x => x.Guests).ToList();
        var reservationsRoomsDb = await context.ReservationRooms.Include(x => x.Guests)
            .Where(x => x.ReservationId == reservation.Id)
            .ToListAsync();

        var toDelete = reservationsRoomsDb.GetUniques(reservation.ReservationRooms, x => x.Id, x => x.Id).ToList();
        var toAdd = reservation.ReservationRooms.GetUniques(reservationsRoomsDb, x => x.Id, x => x.Id).ToList();
        context.RemoveRange(toDelete);
        await context.AddRangeAsync(toAdd);
        var guestsId = reservationsRoomsDb.SelectMany(x => x.Guests).Select(x => x.Id).ToList();
        var guestsDb = await context.Guests.Where(x => guestsId.Contains(x.Id)).ToListAsync();
        var guestToDelete = guestsDb.GetUniques(guests, x => x.Id, x => x.Id).ToList();
        var guestToAdd = guests.GetUniques(guestsDb, x => x.Id, x => x.Id).ToList();
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

    public override async Task DeleteAsync(int reservationId)
    {
        var reservation = await context.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId)
                          ?? throw new HotelException($"Rezerwacja o id {reservationId} nie została odnaleziona.");

        await base.DeleteAsync(reservationId);
    }

    public override async Task<Reservation> GetAsync(Expression<Func<Reservation, bool>> predicate)
    {
        return await context.Reservations
                   .Include(x => x.Customer)
                   .Include(x => x.ReservationRooms).ThenInclude(x => x.Room).ThenInclude(x => x.Area)
                   .Include(x => x.ReservationRooms).ThenInclude(x => x.Guests)
                   .FirstOrDefaultAsync(predicate)
               ?? throw new HotelException($"Rezerwacja nie istnieje.");
    }

    public async Task<List<Reservation>> GetReservationsAsync(int page, int limit, Expression<Func<Reservation, bool>> predicate)
    {
        return await context.Reservations
            .Where(predicate)
            .Include(x => x.Customer)
            .Include(x => x.ReservationRooms).ThenInclude(x => x.Room).ThenInclude(x => x.Area)
            .Include(x => x.ReservationRooms).ThenInclude(x => x.Guests)
            .OrderBy(x => x.Id)
            .Pagging(page, limit)
            .ToListAsync();
    }

    public async Task<Reservation> CreateReservation(int customerId, DateRange dateRange)
    {
        var customer = await context.Customers.FindAsync(customerId)
                       ?? throw new HotelException("Klient nie został odnaleziony.");

        AttachEntry(customer);
        var reservation = Reservation.Create(customer, dateRange.From, dateRange.To);
        await context.Reservations.AddAsync(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public Task AddRoomToReservationAsync(int reservationId, int roomId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRoomFromReservationAsync(int reservationId, int roomId)
    {
        throw new NotImplementedException();
    }

    public Task AddGuestToReservationRoomAsync(int reservationId, int guestId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveGuestFromReservationRoomAsync(int reservationId, int guestId)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetReservationPriceAsync(int reservationId)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetReservationPriceForDay(int reservationId)
    {
        throw new NotImplementedException();
    }
}