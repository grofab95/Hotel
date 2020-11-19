using System.Collections.Generic;
using System.Text;

namespace Hotel.Sql.Tools
{
    public class TriggersGenerator
    {
        private static readonly string schema = @"CREATE TRIGGER [dbo].[trgAfterUpdateXXX]
                               ON [dbo].[XXX]
                               AFTER INSERT, UPDATE
                               AS
                               UPDATE f set UpdatedAt = GETDATE()
                                 FROM
                                 dbo.[XXX] AS f
                                 INNER JOIN inserted
                                 AS i
                                 ON f.Id = i.Id;
                                GO ";

        private static List<string> entitinesNames = new List<string>
        {
            "Customers",
            "Rooms",
            "Areas",
            "Reservations",
            "ReservationRooms",
            "Guests",
            "PriceRules"
        };

        private static string GenerateScript()
        {
            var script = new StringBuilder();

            foreach (var entityName in entitinesNames)
            {
                var trigger = schema.Replace("XXX", entityName);
                script.AppendLine(trigger);
            }

            return script.ToString();
        }

        public static string GetTriggersScript() => GenerateScript();
    }
}
