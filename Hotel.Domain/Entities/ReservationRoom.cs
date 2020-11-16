using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;
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

        protected ReservationRoom() { }

        internal ReservationRoom(Reservation reservation, Room room)
        {
            if (reservation == null)
                throw new MissingValueException($"Rezerwacja jest wymagana.");

            if (room == null)
                throw new HotelException("Pokój jest wymagany.");

            Room = room;
            Reservation = reservation;
            RoomGuests = new List<RoomGuest>();
        }

        //internal static ReservationRoom CreateReservationRoom(Reservation reservation, Room room)
        //{
        //    if (reservation == null)
        //        throw new MissingValueException($"Rezerwacja jest wymagana.");

        //    if (room == null)
        //        throw new HotelException("Pokój jest wymagany.");

        //    return new ReservationRoom
        //    {
        //        Room = room,
        //        Reservation = reservation
        //    };
        //}

        internal ReservationRoom Update(ReservationRoom updatedReservationRoom)
        {
            if (updatedReservationRoom.Room != Room)
                throw new HotelException("Nie można zmieniać pokoju.");

            if (updatedReservationRoom.Reservation != Reservation)
                throw new HotelException("Nie można zmieniać przypisanej rezerwacji.");

            if (!updatedReservationRoom.RoomGuests?.Any() ?? true)
                throw new HotelException("Należy przypisać gości do pokoju.");

            var joined = (from roomGuest in RoomGuests
                            join updatedRoomGuest in updatedReservationRoom.RoomGuests on roomGuest.Id equals updatedReservationRoom.Id
                            select new { roomGuest, updatedRoomGuest }).ToList();

            joined.ForEach(x => x.roomGuest.Update(x.updatedRoomGuest));

            return this;            
        }

        internal RoomGuest AddRoomGuest(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest,
            decimal? priceForStay = null)
        {
            if (BookingAmount == Room.PeopleCapacity)
                throw new HotelException("Nie można dodać osoby - pokój jest pełny.");

            if (isChild && isNewlyweds)
                throw new HotelException("Nie można dodać osoby - dziecko nie może być nowożeńcem XD");

            var roomGuest = new RoomGuest(name, isChild, isNewlyweds, orderedBreakfest, priceForStay);
            RoomGuests.Add(roomGuest);

            return roomGuest;
        }

        internal void RemoveRoomGuest(RoomGuest roomGuest)
        {
            //if (!RoomGuests.Any(x => x.Id == roomGuest.Id))
            if (!RoomGuests.Contains(roomGuest))
                throw new HotelException($"Gość {roomGuest} nie jest przypisany do pokoju {Room}.");

            RoomGuests.Remove(roomGuest);
        }
    }
}
