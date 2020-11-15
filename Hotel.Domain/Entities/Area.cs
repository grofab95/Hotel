using Hotel.Domain.Entities.Common;
using Hotel.Domain.Excetions;
using System;

namespace Hotel.Domain.Entities
{
    public class Area : Entity
    {
        public string Name { get; private set; }

        protected Area() { }
        public Area(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new HotelException("Nazwa obszaru jest wymagana.");

            Name = name;
        }
    }
}
