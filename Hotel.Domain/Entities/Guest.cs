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
        public decimal PriceForStay { get; private set; }

        protected Guest() { }

        internal Guest(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest, decimal priceForStay)
        {
            RoomGuestValidators.ValidIfNameExist(name);
            RoomGuestValidators.ValidPriceForStay(priceForStay);

            Name = name;
            IsChild = isChild;
            IsNewlyweds = isNewlyweds;
            OrderedBreakfest = orderedBreakfest;
            PriceForStay = priceForStay;
        }

        public void Update(Guest updatedGuest)
        {
            RoomGuestValidators.ValidIfNameExist(updatedGuest.Name);
            RoomGuestValidators.ValidPriceForStay(updatedGuest.PriceForStay);

            if (updatedGuest.Id != Id)
                throw new HotelException("Nie można przypisać gościa do innego pokoju");

            if (updatedGuest.IsChild && updatedGuest.IsNewlyweds)
                throw new HotelException("Nie można dodać osoby - dziecko nie może być nowożeńcem XD");

            Name = updatedGuest.Name;
            IsChild = updatedGuest.IsChild;
            IsNewlyweds = updatedGuest.IsNewlyweds;
            OrderedBreakfest = updatedGuest.OrderedBreakfest;
            PriceForStay = updatedGuest.PriceForStay;
        }

        public override string ToString() => Name;
    }
}
