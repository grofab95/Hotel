﻿using Hotel.Domain.Entities.Common;
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

        public int BookingAmount => ReservationRooms?.Sum(x => x.BookingAmount) ?? 0;

        protected Reservation() 
        {
            ReservationRooms = new List<ReservationRoom>();
        }

        public static Reservation Create(Customer customer, DateTime checkIn, DateTime checkOut)
        {
            if (customer == null)
                throw new MissingValueException("Klient nie został określony.");

            //if (checkIn < DateTime.Now)
            //    throw new HotelException("Nie można stworzyć rezerwacji w przeszłości.");

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
            //if (ReservationRooms.Any(x => x.Room == room))
                //return Result.Fail($"Pokój {room} już istnieje w tej rezerwacji.");

            var reservationRoom = new ReservationRoom(this, room);

            ReservationRooms.Add(reservationRoom);
        }

        public void DeleteRoom(Room room)
        {
            //if (!ReservationRooms.Contains(reservationRoom))
            //    throw new HotelException($"{reservationRoom} nie należy do tej rezerwacji.");

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
            bool isNewlyweds, bool orderedBreakfest, decimal? priceForStay = null)
        {
            ReservationValidators.ValidIfReservationRoomExistInReservation(this, reservationRoom);

            return reservationRoom.AddRoomGuest(name, isChild, isNewlyweds, orderedBreakfest, priceForStay);
        }

        public void RemoveGuestFromRoom(ReservationRoom reservationRoom, Guest roomGuest)
        {
            ReservationValidators.ValidIfReservationRoomExistInReservation(this, reservationRoom);

            reservationRoom.RemoveRoomGuest(roomGuest);
        }

        public bool IsRoomInReservation(Room room) => ReservationRooms.Any(x => x.Room.Id == room.Id);

        public decimal GetCalculatedPrice(PriceCalculator priceCalculator)
            => priceCalculator.CalculateReservationPrice(this);

        public int GetGuestsAmount() => ReservationRooms.Sum(x => x.RoomGuests.Count());
    }
}
