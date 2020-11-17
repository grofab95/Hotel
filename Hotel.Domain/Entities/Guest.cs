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
        public decimal? PriceForStay { get; private set; }

        protected Guest() { }

        internal Guest(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest,
            decimal? priceForStay = null)
        {
            RoomGuestValidators.ValidIfNameExist(name);
            RoomGuestValidators.ValidPriceForStay(priceForStay);

            Name = name;
            IsChild = isChild;
            IsNewlyweds = isNewlyweds;
            OrderedBreakfest = orderedBreakfest;
            PriceForStay = priceForStay;
        }

        internal Guest Update(Guest updatedGuest)
        {
            RoomGuestValidators.ValidIfNameExist(updatedGuest.Name);
            RoomGuestValidators.ValidPriceForStay(updatedGuest.PriceForStay);

            if (updatedGuest.Id != Id)
                throw new HotelException("Nie można przypisać gościa do innego pokoju");

            Name = updatedGuest.Name;
            IsChild = updatedGuest.IsChild;
            IsNewlyweds = updatedGuest.IsNewlyweds;
            OrderedBreakfest = updatedGuest.OrderedBreakfest;
            PriceForStay = updatedGuest.PriceForStay;

            return this;
        }

        public override string ToString() => Name;
    }
}
