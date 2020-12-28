using Hotel.Domain.Entities.Common;
using Hotel.Domain.Validators;
using System.Collections.Generic;

namespace Hotel.Domain.Entities
{
    public class Customer : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<Reservation> Reservations { get; private set; }

        protected Customer() { }
        public Customer(string firstName, string lastName)
        {
            CustomerValidators.ValidIfFirstNameExist(firstName);
            CustomerValidators.ValidIfLastNameExist(lastName);
            CustomerValidators.ValidIfFirstAndLastNameAreTheSame(firstName, lastName);

            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
