using Hotel.Domain.Entities.Common;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Utilities;
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

        public static Result<Reservation> CreateReservation(Customer customer, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                if (customer == null)
                    throw new HotelException("Klient nie został określony.");

                if (checkIn < DateTime.Now)
                    throw new HotelException("Nie można stworzyć rezerwacji w przeszłości.");

                if (checkIn > checkOut)
                    throw new HotelException("Date zameldowania nie może byc późniejsza od daty wymeldowania.");
            }
            catch (Exception ex)
            {
                return Result<Reservation>.Fail(ex.Message);
            }

            return Result<Reservation>.Ok(new Reservation 
            {
                Customer = customer,
                CheckIn = checkIn,
                CheckOut = checkOut
            });
        }

        public Result AddReservationRoom(Room room)
        {
            //if (ReservationRooms.Any(x => x.Room.Id == room.Id))
            //    return Result.Fail($"Pokój {room} już istnieje w tej rezerwacji.");

            var createReservationRoomResult = ReservationRoom
                .CreateReservationRoom(this, room);

            if (createReservationRoomResult.IsError)
                return Result.Fail(createReservationRoomResult.Message);

            ReservationRooms.Add(createReservationRoomResult.Value);
            return Result.Ok();
        }

        public Result DeleteReservationRoom(ReservationRoom reservationRoom)
        {
            if (!ReservationRooms.Contains(reservationRoom))
                return Result.Fail($"{reservationRoom} nie należy do tej rezerwacji.");

            ReservationRooms.Remove(reservationRoom);
            return Result.Ok();
        }

        public Result<ReservationRoom> UpdateReservationRoom(ReservationRoom updatedReservationRoom)
        {
            try
            {
                ReservationValidators.ValidIfReservationRoomExistInReservation(this, updatedReservationRoom);
            }
            catch (Exception ex)
            {
                return Result<ReservationRoom>.Fail(ex.Message);
            }
           
            var reservationRoom = ReservationRooms.FirstOrDefault(x => x.Id == updatedReservationRoom.Id);

            return reservationRoom.Update(updatedReservationRoom);
        }

        public Result<RoomGuest> AddGuestToRoom(ReservationRoom reservationRoom, string name, bool isChild,
            bool isNewlyweds, bool orderedBreakfest, decimal? priceForStay = null)
        {
            try
            {
                ReservationValidators.ValidIfReservationRoomExistInReservation(this, reservationRoom);
            }
            catch (Exception ex)
            {
                return Result<RoomGuest>.Fail(ex.Message);
            }

            return reservationRoom.AddRoomGuest(name, isChild, isNewlyweds, orderedBreakfest, priceForStay);
        }

        public Result RemoveGuestFromRoom(ReservationRoom reservationRoom, RoomGuest roomGuest)
        {
            try
            {
                ReservationValidators.ValidIfReservationRoomExistInReservation(this, reservationRoom);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

            return reservationRoom.RemoveRoomGuest(roomGuest);
        }

        public decimal GetCalculatedPrice(PriceCalculator priceCalculator)
            => priceCalculator.CalculateReservationPrice(this);
    }
}
