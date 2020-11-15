using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            try
            {
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

                var customers = new List<Customer>
                {
                    new Customer("Monika", "Olejnik"),
                    new Customer("Andrzej", "Kowalczyk"),
                    new Customer("Anna", "Nowak")
                };

                var checkIn = new DateTime(2020, 11, 20);
                var checkOut = new DateTime(2020, 11, 27);

                var createReservationResult = Reservation.CreateReservation(customers[0], checkIn, checkOut, true);
                if (createReservationResult.IsError)
                    throw new Exception(createReservationResult.Message);

                var reservation = createReservationResult.Value;

                var res1 = reservation.AddReservationRoom(rooms[0], 4, 0, false);
                var res2 = reservation.AddReservationRoom(rooms[1], 3, 0, false);
                var res3 = reservation.AddReservationRoom(rooms[2], 2, 0, false);
                var res4 = reservation.AddReservationRoom(rooms[3], 2, 0, false);
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
