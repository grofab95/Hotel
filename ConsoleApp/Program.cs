using Hotel.Application.Infrastructure;
using Hotel.Domain.Entities;
using Hotel.Domain.Environment;
using Hotel.Domain.Utilities.Models;
using Hotel.Sql;
using Hotel.Sql.Tools;
using System;

ILogger logger = new Logger();
var context = new HotelContext(logger);

try
{
    var password = new Password("tajne");
    var user = new User("Fabian2", "Grochla2", "Q@w", password);
    context.Users.Add(user);
    context.SaveChanges();
}
catch (Exception ex)
{

}