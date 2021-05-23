using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
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

        private static List<string> entitiesNames = new List<string>
        {
            nameof(Customer),
            nameof(Room),
            nameof(Area),
            nameof(Reservation),
            nameof(ReservationRoom),
            nameof(Guest),
            nameof(PriceRule),
            nameof(User),
            nameof(Token)
        };

        private static string GenerateScript()
        {
            var script = new StringBuilder();

            foreach (var entityName in entitiesNames)
            {
                var trigger = schema.Replace("XXX", entityName + "s");
                script.AppendLine(trigger);
            }

            return script.ToString();
        }

        public static string GetTriggersScript() => GenerateScript();
    }
}
