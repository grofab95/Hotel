using Hotel.Sql;
using Hotel.Sql.Tools;

namespace ConsoleApp
{
    class Program
    {
        static HotelContext context = new HotelContext();

        static void Main()
        {
            var script = TriggersGenerator.GetTriggersScript();
        }
    }
}
