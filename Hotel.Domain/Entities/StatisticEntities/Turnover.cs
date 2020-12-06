namespace Hotel.Domain.Entities.StatisticEntities
{
    public class Turnover
    {
        public decimal Income { get; set; }
        public int PeopleAmount { get; set; }
        public int BreakfestAmount { get; set; }

        public bool AnyInfo => Income > 0;
    }
}
