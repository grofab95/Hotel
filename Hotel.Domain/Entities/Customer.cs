using Hotel.Domain.Entities.Common;
using Hotel.Domain.Validators;
using System.Collections.Generic;

namespace Hotel.Domain.Entities;

public class Customer : Entity
{
    public string Name { get; private set; }
    public List<Reservation> Reservations { get; private set; }

    protected Customer() { }
    public Customer(string name)
    {
        CustomerValidators.ValidIftNameExist(name);

        Name = name;
    }

    public override string ToString() => $"{Name}";
}