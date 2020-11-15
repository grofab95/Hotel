using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities
{
    public class ReservationRoom : Entity
    {
        public virtual Room Room { get; private set; }
        public virtual Reservation Reservation { get; private set; }
        public virtual int ReservationId { get; private set; }
        public virtual List<RoomGuest> RoomGuests { get; set; }

        public int BookingAmount => RoomGuests?.Count ?? 0;

        protected ReservationRoom()
        {
            RoomGuests = new List<RoomGuest>();
        }

        internal static Result<ReservationRoom> CreateReservationRoom(Reservation reservation, Room room)
        {
            try
            {
                if (reservation == null)
                    throw new HotelException($"Rezerwacja jest wymagana.");

                if (room == null)
                    throw new HotelException("Pokój jest wymagany.");
            }
            catch (Exception ex)
            {
                return Result<ReservationRoom>.Fail(ex.Message);
            }

            return Result<ReservationRoom>.Ok(new ReservationRoom
            {
                Room = room,
                Reservation = reservation
            });
        }

        internal Result<ReservationRoom> Update(ReservationRoom updatedReservationRoom)
        {
            try
            {
                if (updatedReservationRoom.Room.Id != Room.Id)
                    throw new HotelException("Nie można zmieniać pokoju.");

                if (updatedReservationRoom.Reservation.Id != Reservation.Id)
                    throw new HotelException("Nie można zmieniać przypisanej rezerwacji.");

                if (!updatedReservationRoom.RoomGuests?.Any() ?? true)
                    throw new HotelException("Należy przypisać gości do pokoju.");

                var joined = (from roomGuest in RoomGuests
                              join updatedRoomGuest in updatedReservationRoom.RoomGuests on roomGuest.Id equals updatedReservationRoom.Id
                              select new { roomGuest, updatedRoomGuest }).ToList();

                joined.ForEach(x => x.roomGuest.Update(x.updatedRoomGuest));

                return Result<ReservationRoom>.Ok(this);
            }
            catch (Exception ex)
            {
                return Result<ReservationRoom>.Fail(ex.Message);
            }            
        }

        internal Result<RoomGuest> AddRoomGuest(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest,
            decimal? priceForStay = null)
        {
            if (BookingAmount == Room.PeopleCapacity)
                return Result<RoomGuest>.Fail("Nie można dodać osoby - pokój jest pełny.");

            if (isChild && isNewlyweds)
                return Result<RoomGuest>.Fail("Nie można dodać osoby - dziecko nie może być nowożeńcem XD");

            var createRoomGuestResult = RoomGuest.Create(name, isChild, isNewlyweds, orderedBreakfest, priceForStay);
            if (createRoomGuestResult.IsSuccess)
                RoomGuests.Add(createRoomGuestResult.Value);

            return createRoomGuestResult;
        }

        internal Result RemoveRoomGuest(RoomGuest roomGuest)
        {
            if (!RoomGuests.Any(x => x.Id == roomGuest.Id))
                return Result.Fail($"Gość {roomGuest} nie jest przypisany do pokoju {Room}.");

            RoomGuests.Remove(roomGuest);
            return Result.Ok();
        }
    }
}
