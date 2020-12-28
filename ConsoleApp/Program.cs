using Hotel.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

var context = new HotelContext();

try
{
    var res = context.Reservations
        .Include(x => x.Customer)
        .Include(x => x.ReservationRooms).ThenInclude(x => x.Room).ThenInclude(x => x.Area)
        .Include(x => x.ReservationRooms).ThenInclude(x => x.Guests)
        .FirstOrDefault();

}
catch (Exception ex)
{

}

//var customers = new List<Customer>
//{
//    new Customer("Monika", "Olejnik"),
//    new Customer("Andrzej", "Kowalczyk"),
//    new Customer("Anna", "Nowak"),
//    new Customer("Stanisław", "Wodny"),
//    new Customer("Maria", "Kaziewska"),
//    new Customer("Stefan", "Gruby"),
//    new Customer("Żaneta", "Kuźniar"),
//    new Customer("Marian", "Nowy")
//};

//context.AddRange(customers);
//context.SaveChanges();

