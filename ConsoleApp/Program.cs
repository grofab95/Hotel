using Hotel.Domain.Entities;
using Hotel.Domain.Extensions;
using Hotel.Sql;
using Hotel.Sql.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

var context = new HotelContext();

var customers = new List<Customer>
{
    new Customer("Monika", "Olejnik"),
    new Customer("Andrzej", "Kowalczyk"),
    new Customer("Anna", "Nowak"),
    new Customer("Stanisław", "Wodny"),
    new Customer("Maria", "Kaziewska"),
    new Customer("Stefan", "Gruby"),
    new Customer("Żaneta", "Kuźniar"),
    new Customer("Marian", "Nowy")
};

context.AddRange(customers);
context.SaveChanges();

