using AutoMapper;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Views;
using Hotel.Domain.Exceptions;
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

        public async Task<int> SaveReservationAsync(Reservation reservation)
        {
            try
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
                AttachEntry(reservation);

                if (reservation.Id == 0)
                {
                    await context.AddAsync(reservation);
                    await context.SaveChangesAsync();
                }
                else
                {
                    var reservationsRoomsDb = await context.ReservationRooms.Include(x => x.Guests).Where(x => x.ReservationId == reservation.Id).ToListAsync();

                    var toDelete = (from newEntry in reservationsRoomsDb
                                    join noInsert in reservation.ReservationRooms on newEntry.Id equals noInsert.Id
                                    into du
                                    from ud in du.DefaultIfEmpty()
                                    where ud == null
                                    select newEntry).ToList();

                    var toAdd = (from newEntry in reservation.ReservationRooms
                                 join noInsert in reservationsRoomsDb on newEntry.Id equals noInsert.Id
                                 into du
                                 from ud in du.DefaultIfEmpty()
                                 where ud == null
                                 select newEntry).ToList();

                    context.RemoveRange(toDelete);
                    await context.AddRangeAsync(toAdd);

                    var guestsId = reservationsRoomsDb.SelectMany(x => x.Guests).Select(x => x.Id).ToList();
                    var guestsDb = await context.Guests.Where(x => guestsId.Contains(x.Id)).ToListAsync();

                    var guestToDelete = (from newEntry in guestsDb
                                         join noInsert in guests on newEntry.Id equals noInsert.Id
                                         into du
                                         from ud in du.DefaultIfEmpty()
                                         where ud == null
                                         select newEntry).ToList();

                    var guestToAdd = (from newEntry in guests
                                      join noInsert in guestsDb on newEntry.Id equals noInsert.Id
                                      into du
                                      from ud in du.DefaultIfEmpty()
                                      where ud == null
                                      select newEntry).ToList();

                    context.RemoveRange(guestToDelete);
                    await context.AddRangeAsync(guestToAdd);

                    await context.SaveChangesAsync();
                }

                return reservation.Id;
            }
            catch (System.Exception ex)
            {
                if (reservation.Id != 0)
                {
                    reservation = await GetReservationByIdAsync(reservation.Id);
                }

                throw ex;
            }            
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
