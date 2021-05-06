using Hotel.Sql;
using Hotel.Sql.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

var context = new HotelContext();

try
{
    var triggers = TriggersGenerator.GetTriggersScript();
}
catch (Exception ex)
{

}