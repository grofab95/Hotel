using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Validators;

namespace Hotel.Domain.Entities
{
    public class Guest : Entity
    {
        public string Name { get; private set; }
        public bool IsChild { get; private set; }
        public bool IsNewlyweds { get; private set; }
        public bool OrderedBreakfest { get; private set; }
        public decimal BasePrice { get; private set; }
        public decimal PriceForStay { get; private set; }
        public virtual ReservationRoom ReservationRoom { get; private set; }

        protected Guest() { }

        internal Guest(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest, decimal basePrice, ReservationRoom reservationRoom)
        {
            GuestValidators.ValidIfNameExist(name);
            GuestValidators.ValidPrice(basePrice);

            Name = name;
            IsChild = isChild;
            IsNewlyweds = isNewlyweds;
            OrderedBreakfest = orderedBreakfest;
            BasePrice = basePrice;
            ReservationRoom = reservationRoom;
        }

        public void Update(Guest updatedGuest)
        {
            GuestValidators.ValidIfNameExist(updatedGuest.Name);
            GuestValidators.ValidPrice(updatedGuest.BasePrice);

            if (updatedGuest.Id != Id)
                throw new HotelException("Nie można przypisać gościa do innego pokoju");

            if (updatedGuest.IsChild && updatedGuest.IsNewlyweds)
                throw new HotelException("Nie można dodać osoby - dziecko nie może być nowożeńcem");

            Name = updatedGuest.Name;
            IsChild = updatedGuest.IsChild;
            IsNewlyweds = updatedGuest.IsNewlyweds;
            OrderedBreakfest = updatedGuest.OrderedBreakfest;
            BasePrice = updatedGuest.BasePrice;
        }

        internal void SetPriceForStay(decimal priceForStay)
        {
            GuestValidators.ValidPrice(priceForStay);

            PriceForStay = priceForStay;
        }

        public override string ToString() => Name;
    }
}
