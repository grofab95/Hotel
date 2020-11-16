using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;

namespace Hotel.Domain.Entities
{
    public class Area : Entity
    {
        public string Name { get; private set; }

        protected Area() { }
        public Area(string name)
        {
            if (name.IsNotExist())
                throw new HotelException("Nazwa obszaru jest wymagana.");

            Name = name;
        }
    }
}
