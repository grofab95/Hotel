﻿using AutoMapper;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Common;
using Hotel.Domain.Entities.Views;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;
using Hotel.Domain.Validators;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> AddReservationAsync(Reservation reservation)
        {            
            AttachAggregate(reservation);

            await context.AddAsync(reservation);
            await context.SaveChangesAsync();

            return reservation.Id;
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

        public async Task<bool> UpdateReservationAsync(Reservation reservation)
        {
            AttachAggregate(reservation);

            var guests = reservation.ReservationRooms.SelectMany(x => x.Guests).ToList();
            var reservationsRoomsDb = await context.ReservationRooms.Include(x => x.Guests)
                .Where(x => x.ReservationId == reservation.Id)
                .ToListAsync();

            var toDelete = reservationsRoomsDb.GetUnique(reservation.ReservationRooms, x => x.Id, x => x.Id).ToList();
            var toAdd = reservation.ReservationRooms.GetUnique(reservationsRoomsDb, x => x.Id, x => x.Id).ToList();

            var tsdft = context
                   .Set<Area>().Local
                   //.Where(x => x.GetType() == typeof(T) && x.Id.Equals(entry.Id))
                   .ToList();

            var trackedEntries = context.ChangeTracker.Entries<Entity>()
                   //.GroupBy(x => new { x.Entity.GetType().Name, x.Entity.Id })
                   //.Select(x => x.First())
                   .ToList();

            var tracke56346ies = context.ChangeTracker.Entries<Entity>()
                    //.GroupBy(x => new { x.Entity.GetType().Name, x.Entity.Id })
                    //.Select(x => x.First())
                    .ToList();



            context.RemoveRange(toDelete);
            await context.AddRangeAsync(toAdd);

            var guestsId = reservationsRoomsDb.SelectMany(x => x.Guests).Select(x => x.Id).ToList();
            var guestsDb = await context.Guests.Where(x => guestsId.Contains(x.Id)).ToListAsync();

            var guestToDelete = guestsDb.GetUnique(guests, x => x.Id, x => x.Id).ToList();
            var guestToAdd = guests.GetUnique(guestsDb, x => x.Id, x => x.Id).ToList();

            context.RemoveRange(guestToDelete);
            await context.AddRangeAsync(guestToAdd);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ReservationInfoView>> GetReservationBasicInfosAsync()
        {
            return await context.ReservationInfoViews.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            //var reservationDto = await context.Reservations
            //    .ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync()
            //        ?? throw new HotelException($"Reserwacja o id {reservationId} nie istnieje.");

            //return _mapper.Map<Reservation>(reservationDto);

            return await context.Reservations
                .Include(x => x.Customer)
                .Include(x => x.ReservationRooms).ThenInclude(x => x.Room).ThenInclude(x => x.Area)
                .Include(x => x.ReservationRooms).ThenInclude(x => x.Guests)
                .FirstOrDefaultAsync(x => x.Id == reservationId)
                    ?? throw new HotelException($"Reserwacja o id {reservationId} nie istnieje.");
        }

        public IQueryable<ReservationInfoView> SearchReservations()
        {
            return context.ReservationInfoViews;
        }
    }
}
