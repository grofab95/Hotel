using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities;

public class ReservationRoom : Entity
{
    public Room Room { get; private set; }
    public Reservation Reservation { get; private set; }
    public int ReservationId { get; private set; }
    public List<Guest> Guests { get; private set; }

    public int BookingAmount => Guests?.Count ?? 0;

    protected ReservationRoom() { }

    internal ReservationRoom(Reservation reservation, Room room)
    {
        if (reservation == null)
            throw new MissingValueException($"Rezerwacja jest wymagana.");

        if (room == null)
            throw new HotelException("Pokój jest wymagany.");

        Room = room;
        Reservation = reservation;
        Guests = new List<Guest>();
    }

    internal ReservationRoom Update(ReservationRoom updatedReservationRoom)
    {
        if (updatedReservationRoom.Room != Room)
            throw new HotelException("Nie można zmieniać pokoju.");

        if (updatedReservationRoom.Reservation != Reservation)
            throw new HotelException("Nie można zmieniać przypisanej rezerwacji.");

        if (!updatedReservationRoom.Guests?.Any() ?? true)
            throw new HotelException("Należy przypisać gości do pokoju.");

        var joined = (from guest in Guests
            join updatedGuest in updatedReservationRoom.Guests on guest.Id equals updatedReservationRoom.Id
            select new { guest, updatedGuest }).ToList();

        joined.ForEach(x => x.guest.Update(x.updatedGuest));

        return this;            
    }

    internal Guest AddGuest(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest, decimal priceForStay)
    {
        if (BookingAmount == Room.PeopleCapacity)
            throw new HotelException("Nie można dodać osoby - pokój jest pełny.");

        if (isChild && isNewlyweds)
            throw new HotelException("Nie można dodać osoby - dziecko nie może być nowożeńcem XD");

        Guests.Add(new Guest(name, isChild, isNewlyweds, orderedBreakfest, priceForStay, this));

        return Guests.Last();
    }

    internal void RemoveGuest(Guest guest)
    {
        if (!Guests.Contains(guest))
            throw new HotelException($"Gość {guest} nie jest przypisany do pokoju {Room}.");

        Guests.Remove(guest);
    }
}