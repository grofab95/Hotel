using Hotel.Domain.Entities.Common;

namespace Hotel.Sql.Dtos
{
    internal class GuestDto : Entity
    {
        public string Name { get; private set; }
        public bool IsChild { get; private set; }
        public bool IsNewlyweds { get; private set; }
        public bool OrderedBreakfest { get; private set; }
        public decimal BasePrice { get; private set; }
        public decimal PriceForStay { get; private set; }
    }
}
