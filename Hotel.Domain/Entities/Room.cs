using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Entities
{
    public class Room : Entity
    {
        public string Name { get; private set; }
        public int PeopleCapacity { get; private set; }
        public virtual Area Area { get; private set; }

        protected Room() { }
        public Room(Area area, string name, int peopleAmount)
        {
            if (area == null)
                throw new HotelException("Obszar jest wymagany.");

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new HotelException("Nazwa pokoju jest wymagana.");

            if (peopleAmount <= 0)
                throw new HotelException("Ilość musi być większa od zera.");

            Area = area;
            Name = name;
            PeopleCapacity = peopleAmount;
        }

        public override string ToString()
            => $"{Area?.Name} pokój {Name}";
    }
}
