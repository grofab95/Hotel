using Hotel.Sql;
using Hotel.Sql.Tools;
using System;

var context = new HotelContext();

try
{
    var triggers = TriggersGenerator.GetTriggersScript();
}
catch (Exception ex)
{

}