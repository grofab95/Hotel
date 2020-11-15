using Hotel.Domain.Entities.Common;
using Hotel.Domain.Excetions;
using System.Collections.Generic;

namespace Hotel.Domain.Entities
{
    public class Customer : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public virtual List<Reservation> Reservations { get; private set; }

        protected Customer() { }
        public Customer(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName))
                throw new HotelException("Imię jest wymagane.");

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName))
                throw new HotelException("Nazwisko jest wymagane.");

            if (firstName.ToLower().Trim() == lastName.ToLower().Trim())
                throw new HotelException("Imię i nazwisko nie mogą byc takie same.");

            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
