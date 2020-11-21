using Hotel.Domain.Entities.Common;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities
{
    public class Reservation : Entity
    {
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }
        public virtual Customer Customer { get; private set; }
        public virtual List<ReservationRoom> ReservationRooms { get; private set; }

        protected Reservation() 
        {
            ReservationRooms = new List<ReservationRoom>();
        }

        public static Reservation Create(Customer customer, DateTime checkIn, DateTime checkOut)
        {
            if (customer == null)
                throw new MissingValueException("Klient nie został określony.");

            if (checkIn < DateTime.Now)
                throw new HotelException("Nie można stworzyć rezerwacji w przeszłości.");

            if (checkIn > checkOut)
                throw new HotelException("Date zameldowania nie może byc późniejsza od daty wymeldowania.");

            return new Reservation 
            {
                Customer = customer,
                CheckIn = checkIn,
                CheckOut = checkOut
            };
        }

        public void AddRoom(Room room)
        {
            if (ReservationRooms.Any(x => x.Room == room))
                throw new Exception($"Pokój {room} już istnieje w tej rezerwacji.");

            var reservationRoom = new ReservationRoom(this, room);

            ReservationRooms.Add(reservationRoom);
        }

        public void DeleteRoom(Room room)
        {
            if (!ReservationRooms.Select(x => x.Room).Contains(room))
                throw new HotelException($"{room} nie należy do tej rezerwacji.");

            var reservationRoom = ReservationRooms.FirstOrDefault(x => x.Room.Id == room.Id)
                ?? throw new HotelException($"{room} nie należy do tej rezerwacji.");

            ReservationRooms.Remove(reservationRoom);
        }

        public ReservationRoom UpdateRoom(ReservationRoom updatedReservationRoom)
        {
            ReservationValidators.ValidIfReservationRoomExistInReservation(this, updatedReservationRoom);

            var reservationRoom = ReservationRooms.FirstOrDefault(x => x.Id == updatedReservationRoom.Id);

            return reservationRoom.Update(updatedReservationRoom);
        }

        public Guest AddGuestToRoom(ReservationRoom reservationRoom, string name, bool isChild,
            bool isNewlyweds, bool orderedBreakfest, decimal priceForStay)
        {
            ReservationValidators.ValidIfReservationRoomExistInReservation(this, reservationRoom);

            return reservationRoom.AddGuest(name, isChild, isNewlyweds, orderedBreakfest, priceForStay);
        }

        public void RemoveGuestFromRoom(ReservationRoom reservationRoom, Guest guest)
        {
            ReservationValidators.ValidIfReservationRoomExistInReservation(this, reservationRoom);

            reservationRoom.RemoveGuest(guest);
        }

        public bool IsRoomInReservation(Room room) => ReservationRooms.Any(x => x.Room.Id == room.Id);

        public decimal GetCalculatedPrice(PriceCalculator priceCalculator)
        {
            var guest = ReservationRooms.SelectMany(x => x.Guests).ToList();
            guest.ForEach(x => x.SetPriceForStay(priceCalculator.CalculateGuestPrice(x)));

            return priceCalculator.CalculateReservationPrice(this);
        }

        public List<Room> GetRooms() => ReservationRooms.Select(x => x.Room).ToList();
        public List<Guest> GetGuests() => ReservationRooms.SelectMany(x => x.Guests).ToList();
        public int GetGuestsAmount() => ReservationRooms.Sum(x => x.Guests.Count);
        public int GetRoomsAmount() => ReservationRooms.Count;
    }
}
