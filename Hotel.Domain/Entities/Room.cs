using Hotel.Domain.Entities.Common;
using Hotel.Domain.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Domain.Entities
{
    public class Room : Entity
    {
        public string Name { get; private set; }
        public int PeopleCapacity { get; private set; }
        public virtual Area Area { get; private set; }

        [NotMapped]
        public string Note { get; private set; }

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

        public void SetNote(string note) => Note = note;

        public override string ToString()
            => $"{Area?.Name} pokój {Name}";

        public void Update(Area area, string name, int peopleCapcity)
        {
            RoomValidators.ValidIfAreaExist(area);
            RoomValidators.ValidIfNameExist(name);
            RoomValidators.ValidIfPeopleCapacityIsPositive(peopleCapcity);

            Area = area;
            Name = name;
            PeopleCapacity = peopleCapcity;
        }
    }
}
