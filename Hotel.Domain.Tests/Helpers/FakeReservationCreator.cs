﻿using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Tests.Helpers
{
    public class FakeReservationCreator
    {
        public static Reservation Get()
        {
            var customer = new Customer("Marta", "Nowak");
            var checkIn = new DateTime(2020, 11, 20);
            var checkOut = new DateTime(2020, 11, 27);

            var reservation = Reservation.CreateReservation(customer, checkIn, checkOut).Value;

            var areas = new List<Area>
            {
                new Area("Budynek A"),
                new Area("Budynek B")
            };

            var rooms = new List<Room>
            {
                new Room(areas[0], "P1", 4),
                new Room(areas[0], "P2", 2),
                new Room(areas[0], "P3", 3),
                new Room(areas[1], "P4", 2),
                new Room(areas[1], "P5", 2),
                new Room(areas[1], "P6", 5)
            };

            var res01 = reservation.AddReservationRoom(rooms[0]);
            var res02 = reservation.AddReservationRoom(rooms[1]);
            var res03 = reservation.AddReservationRoom(rooms[2]);
            var res04 = reservation.AddReservationRoom(rooms[3]);

            var room1 = reservation.ReservationRooms[0];
            var room2 = reservation.ReservationRooms[1];
            var room3 = reservation.ReservationRooms[2];
            var room4 = reservation.ReservationRooms[3];

            var res1 = reservation.AddGuestToRoom(room1, "Wujek Andrzej", false, false, true);
            var res2 = reservation.AddGuestToRoom(room1, "Ciocia Ania", false, false, true);
            var res3 = reservation.AddGuestToRoom(room1, "Janusz", true, false, false);
            var res4 = reservation.AddGuestToRoom(room1, "Zosia", true, false, false);

            var res5 = reservation.AddGuestToRoom(room2, "Pani młoda", false, true, true);
            var res6 = reservation.AddGuestToRoom(room2, "Pan młody", false, true, true);

            var res7 = reservation.AddGuestToRoom(room3, "Jacek", false, false, true);
            var res8 = reservation.AddGuestToRoom(room3, "Eliza", false, false, true);

            var res9 = reservation.AddGuestToRoom(room4, "Babcia Agnieszka", false, false, false, 10);
            var res0 = reservation.AddGuestToRoom(room4, "Dziadek Józef", false, false, false, 10);

            return reservation;
        }
    }
}