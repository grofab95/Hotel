using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Tests.Helpers
{
    public class FakeReservationCreator
    {
        private static readonly decimal _priceForStay = 50;

        private Customer _customer;
        private List<Area> _areas;
        private List<Room> _rooms;

        private readonly DateTime _checkIn;
        private readonly DateTime _checkOut;

        public FakeReservationCreator(DateTime checkIn, DateTime checkOut)
        {
            _checkIn = checkIn;
            _checkOut = checkOut;

            _customer = new Customer("Marta", "Nowak");
            _areas = new List<Area>
            {
                new Area("Budynek A"),
                new Area("Budynek B")
            };

            _rooms = new List<Room>
            {
                new Room(_areas[0], "P1", 4),
                new Room(_areas[0], "P2", 2),
                new Room(_areas[0], "P3", 3),
                new Room(_areas[1], "P4", 2),
                new Room(_areas[1], "P5", 2),
                new Room(_areas[1], "P6", 5)
            };
        }

        public Customer GetCustomer() => _customer;
        public List<Room> GetRooms() => _rooms;
        public List<Area> GetAreas() => _areas;

        public Reservation GetReservation()
        {
            var reservation = Reservation.Create(_customer, _checkIn, _checkOut);

            reservation.AddRoom(_rooms[0]);
            reservation.AddRoom(_rooms[1]);
            reservation.AddRoom(_rooms[2]);
            reservation.AddRoom(_rooms[3]);

            var room1 = reservation.ReservationRooms[0];
            var room2 = reservation.ReservationRooms[1];
            var room3 = reservation.ReservationRooms[2];
            var room4 = reservation.ReservationRooms[3];

            var res1 = reservation.AddGuestToRoom(room1, "Wujek Andrzej", false, false, true, _priceForStay);
            var res2 = reservation.AddGuestToRoom(room1, "Ciocia Ania", false, false, true, _priceForStay);
            var res3 = reservation.AddGuestToRoom(room1, "Janusz", true, false, false, _priceForStay);
            var res4 = reservation.AddGuestToRoom(room1, "Zosia", true, false, false, _priceForStay);

            var res5 = reservation.AddGuestToRoom(room2, "Pani młoda", false, true, true, _priceForStay);
            var res6 = reservation.AddGuestToRoom(room2, "Pan młody", false, true, true, _priceForStay);

            var res7 = reservation.AddGuestToRoom(room3, "Jacek", false, false, true, _priceForStay);
            var res8 = reservation.AddGuestToRoom(room3, "Eliza", false, false, true, _priceForStay);

            var res9 = reservation.AddGuestToRoom(room4, "Babcia Agnieszka", false, false, false, _priceForStay);
            var res0 = reservation.AddGuestToRoom(room4, "Dziadek Józef", false, false, false, _priceForStay);

            return reservation;
        }
    }
}
