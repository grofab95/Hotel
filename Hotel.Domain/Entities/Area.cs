using Hotel.Domain.Entities.Common;
using Hotel.Domain.Validators;

namespace Hotel.Domain.Entities
{
    public class Area : Entity
    {
        public string Name { get; private set; }

        protected Area() { }
        public Area(string name)
        {
            AreaValidators.ValidIfNameExist(name);

            Name = name;
        }
    }
}
