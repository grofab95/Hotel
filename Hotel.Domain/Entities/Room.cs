using Hotel.Domain.Entities.Common;
using Hotel.Domain.Validators;

namespace Hotel.Domain.Entities
{
    public class Room : Entity
    {
        public string Name { get; private set; }
        public int PeopleCapacity { get; private set; }
        public virtual Area Area { get; private set; }

        protected Room() { }

        public Room(Area area, string name, int peopleCapcity)
        {
            RoomValidators.ValidIfAreaExist(area);
            RoomValidators.ValidIfNameExist(name);
            RoomValidators.ValidIfPeopleCapacityIsPositive(peopleCapcity);

            Area = area;
            Name = name;
            PeopleCapacity = peopleCapcity;
        }

        public override string ToString()
            => $"{Area?.Name} pokój {Name}";
    }
}
